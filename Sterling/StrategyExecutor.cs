using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sterling
{
    public class StrategyExecutor
    {
        public event Action<string, bool> ErrorMessage;
        public event Action<string> LogMessage;
        public event Action<Strategy> TradeStopped;
        public event Action<Strategy, double, int, double> UpdateTradeStats;

        private bool _running;
        private double _dpr;
        private double _price;
        private double _r;
        private double[] _buyPrices;
        private double[] _sellPrices;
        private double _lastPrice;
        private int _quantity;
        private int _n = 1000;
        private int _s;
        private int _stratPosTrack;
        private int[] _buyQuantityTracker;
        private int[] _sellQuantityTracker;
        private object _lastPriceLock;
        private object _runningLock;
        private SterlingLib.STIApp _stiApp;
        private SterlingLib.STIOrder _stiClosingOrder;
        private SterlingLib.STIOrder _stiLimitOrder;
        private SterlingLib.STIOrder _stiStopOrder;
        private SterlingLib.STIPosition _stiPosition;
        private Strategy _strategy;
        private string _account;
        private string _exchange;
        private string _stopOrderID;
        private bool _stopFailed;

        public StrategyExecutor(Strategy strat, string acct, string ex,
                                double p, int quant, double DPR,
                                double R, int S)
        {
            try
            {
                _strategy = strat;
                _account = acct;
                _exchange = ex;
                _price = p;
                _quantity = quant;
                _dpr = DPR;
                _r = R;
                _s = S;
                _buyPrices = new double[_n];
                _sellPrices = new double[_n];
                _buyQuantityTracker = new int[_n];
                _sellQuantityTracker = new int[_n];
                _stiLimitOrder = new SterlingLib.STIOrder();
                _stiStopOrder = new SterlingLib.STIOrder();
                _stiClosingOrder = new SterlingLib.STIOrder();
                _stiPosition = new SterlingLib.STIPosition();
                _stiApp = new SterlingLib.STIApp();
                _stiApp.SetModeXML(true);

                _stiLimitOrder.Tif = "D";
                _stiLimitOrder.Destination = _exchange;
                _stiLimitOrder.Account = _account;
                _stiLimitOrder.Symbol = _strategy.Symbol;
                _stiLimitOrder.PriceType = SterlingLib.STIPriceTypes.ptSTILmt;
                _stiStopOrder.Tif = "D";
                _stiStopOrder.Destination = _exchange;
                _stiStopOrder.Account = _account;
                _stiStopOrder.Symbol = _strategy.Symbol;
                _stiClosingOrder.Tif = "D";
                _stiClosingOrder.Destination = _exchange;
                _stiClosingOrder.Account = _account;
                _stiClosingOrder.Symbol = _strategy.Symbol;
                _stiClosingOrder.PriceType = SterlingLib.STIPriceTypes.ptSTIMkt;
                //this.stiStopOrder.PriceType = SterlingLib.STIPriceTypes.ptSTISvrStp;
                _stopOrderID = null;
                _stratPosTrack = 0;
                _stopFailed = false;
                _runningLock = new object();
                _lastPriceLock = new object();
                Running = false;
            }
            catch
            {
                OnErrorMessage("Error occurred in the trade executor constructor.", true);
            }

        }

        public bool Running
        {
            get
            {
                lock (_runningLock)
                {
                    return _running;
                }
            }
            set
            {
                lock (_runningLock)
                {
                    _running = value;
                }
            }
        }

        public double LastPrice
        {
            get{
                lock (_lastPriceLock)
                {
                    return _lastPrice;
                }
            }
            set
            {
                lock (_lastPriceLock)
                {
                    _lastPrice = value;
                }
            }
        }

        public string Symbol
        {
            get
            {
                return _strategy.Symbol;
            }
            private set { }
        }

        public double CalcPnL(double currPrice)
        {
            double sellAmt = 0;
            double buyAmt = 0;
            if (_stratPosTrack > 0)
            {
                if (_strategy.Side == Side.Buy)
                {
                    sellAmt = currPrice * _sellQuantityTracker[_stratPosTrack - 1];
                    buyAmt = 0;
                    for (int i = 0; i < _stratPosTrack; i++)
                    {
                        buyAmt += _buyPrices[i] * _buyQuantityTracker[i];
                    }

                }
                else
                {
                    buyAmt = currPrice * _buyQuantityTracker[_stratPosTrack - 1];
                    sellAmt = 0;
                    for (int i = 0; i < _stratPosTrack; i++)
                    {
                        sellAmt += _sellPrices[i] * _sellQuantityTracker[i];
                    }
                }
            }
            double pnl = sellAmt - buyAmt;
            return Math.Round(pnl, 2);
        }

        public void CancelAllOrders()
        {
            //Retrieve all open orders for this symbol and account
            SterlingLib.STIOrderMaint oMaint = new SterlingLib.STIOrderMaint();
            Array orders = null;
            var filter = new SterlingLib.structSTIOrderFilter();
            filter.bOpenOnly = 1;
            filter.bstrAccount = _account;
            filter.bstrSymbol = _strategy.Symbol;
            oMaint.GetOrderListEx(ref filter, ref orders);
            //Cancel all those orders
            foreach (SterlingLib.structSTIOrderUpdate pendingOrder in orders)
            {
                oMaint.CancelOrder(_account, pendingOrder.nOrderRecordId, "", "");
            }
        }

        public void RunStrategy()
        {
            Running = true;
            if (_strategy.Side == Side.Buy)
            {
                LastPrice = 0;
                RunBuyStrategy();
            }
            else
            {
                LastPrice = 100000;
                RunSellStrategy();
            }
        }

        public void StartTrade()
        {
            Task.Factory.StartNew(() => { RunStrategy(); });
        }

        public void StopTrade()
        {
            if (_strategy.Side == Side.Buy)
            {
                _stiClosingOrder.Side = "S";
            }
            else
            {
                _stiClosingOrder.Side = "B";
            }
            if (_stratPosTrack > 0)
            {
                SterlingLib.structSTIPositionUpdate positionUpdate = _stiPosition.GetPositionInfoStruct(_strategy.Symbol, "", _account);
                _stiClosingOrder.Quantity = Math.Abs(positionUpdate.nOpeningPosition + positionUpdate.nSharesBot - positionUpdate.nSharesSld);
                int ord = _stiClosingOrder.SubmitOrder();
                if (ord != 0)
                {
                    OnErrorMessage("Error occurred while trying to Stop the strategy. Order error code- " + ord.ToString(), false);
                }
            }
        }

        private void CalculateDSPBuyPrices()
        {
            double weightedSum = _r * _s;
            int netQuantity = _s;

            for (int j = 0; j < _n; j++)
            {
                _buyPrices[j] = _price + j * _dpr;
                _buyQuantityTracker[j] = _quantity;
                weightedSum += _buyPrices[j] * _buyQuantityTracker[j];
                netQuantity += _quantity;
                _sellQuantityTracker[j] = netQuantity;
                double weightedAvg = weightedSum / netQuantity;
                _sellPrices[j] = weightedAvg;
            }
        }

        private void CalculateDSPSellPrices()
        {
            double weightedSum = _r * _s;
            int netQuantity = _s;

            for (int j = 0; j < _n; j++)
            {
                _sellPrices[j] = _price - j * _dpr;
                _sellQuantityTracker[j] = _quantity;
                weightedSum += _sellPrices[j] * _sellQuantityTracker[j];
                netQuantity += _quantity;
                _buyQuantityTracker[j] = netQuantity;
                double weightedAvg = weightedSum / netQuantity;
                _buyPrices[j] = weightedAvg;
            }
        }

        private void CalculateIQ_IncrementBuyPrices()
        {
            double weightedSum = 0;
            int netQuantity = 0;
            //int tempQuant = quantity;
            for (int j = 0; j < _s; j++)
            {
                _buyPrices[j] = _price + j * _dpr;
                //tempQuant += j * quantity;
                int tempQuant = _quantity + j * _quantity;
                _buyQuantityTracker[j] = tempQuant;
                //_buyQuantityTracker[j] = tempQuant;
                weightedSum += _buyPrices[j] * _buyQuantityTracker[j];
                netQuantity += tempQuant;
                _sellQuantityTracker[j] = netQuantity;
                double weightedAvg = weightedSum / netQuantity;
                _sellPrices[j] = weightedAvg - (_r / (netQuantity * (j + 1)));
            }

            for (int k = _s; k < _n; k++)
            {
                _buyPrices[k] = _price + k * _dpr;
                int tempQuant = _quantity + k * _quantity;
                _buyQuantityTracker[k] = tempQuant;
                weightedSum += _buyPrices[k] * _buyQuantityTracker[k];
                netQuantity += tempQuant;
                _sellQuantityTracker[k] = netQuantity;
                double weightedAvg = weightedSum / netQuantity;
                _sellPrices[k] = weightedAvg;
            }
        }

        private void CalculateIQ_IncrementSellPrices()
        {
            double weightedSum = 0;
            int netQuantity = 0;
            //int tempQuant = quantity;
            for (int j = 0; j < _s; j++)
            {
                _sellPrices[j] = _price - j * _dpr;
                int tempQuant = _quantity + j * _quantity;
                _sellQuantityTracker[j] = tempQuant;
                weightedSum += _sellPrices[j] * _sellQuantityTracker[j];
                netQuantity += tempQuant;
                _buyQuantityTracker[j] = netQuantity;
                double weightedAvg = weightedSum / netQuantity;
                _buyPrices[j] = weightedAvg + (_r / (netQuantity * (j + 1)));
            }

            for (int k = _s; k < _n; k++)
            {
                _sellPrices[k] = _price - k * _dpr;
                int tempQuant = _quantity + k * _quantity;
                _sellQuantityTracker[k] = tempQuant;
                weightedSum += _sellPrices[k] * _sellQuantityTracker[k];
                netQuantity += tempQuant;
                _buyQuantityTracker[k] = netQuantity;
                double weightedAvg = weightedSum / netQuantity;
                _buyPrices[k] = weightedAvg;
            }
        }

        private void CalculateIQ_RSLBuyPrices()
        {
            double weightedSum = 0;
            int netQuantity = 0;

            for (int j = 0; j < _s; j++)
            {
                _buyPrices[j] = _price + j * _dpr;
                _buyQuantityTracker[j] = _quantity;
                weightedSum += _buyPrices[j] * _quantity;
                netQuantity += _quantity;
                double weightedAvg = weightedSum / netQuantity;
                _sellPrices[j] = weightedAvg - (_r / (_quantity * (j + 1)));
                _sellQuantityTracker[j] = netQuantity;
            }

            for (int k = _s; k < _n; k++)
            {
                _buyPrices[k] = _price + k * _dpr;
                _buyQuantityTracker[k] = _quantity;
                weightedSum += _buyPrices[k] * _quantity;
                netQuantity += _quantity;
                double weightedAvg = weightedSum / netQuantity;
                _sellPrices[k] = weightedAvg;
                _sellQuantityTracker[k] = netQuantity;
            }
        }

        private void CalculateIQ_RSLSellPrices()
        {
            double weightedSum = 0;
            int netQuantity = 0;

            for (int j = 0; j < _s; j++)
            {
                _sellPrices[j] = _price - j * _dpr;
                _sellQuantityTracker[j] = _quantity;
                weightedSum += _sellPrices[j] * _quantity;
                netQuantity += _quantity;
                double weightedAvg = weightedSum / netQuantity;
                _buyPrices[j] = weightedAvg + (_r / (_quantity * (j + 1)));
                _buyQuantityTracker[j] = netQuantity;
            }

            for (int k = _s; k < _n; k++)
            {
                _sellPrices[k] = _price - k * _dpr;
                _sellQuantityTracker[k] = _quantity;
                weightedSum += _sellPrices[k] * _quantity;
                netQuantity += _quantity;
                double weightedAvg = weightedSum / netQuantity;
                _buyPrices[k] = weightedAvg;
                _buyQuantityTracker[k] = netQuantity;
            }
        }

        private void CalculateBuyStratPrices()
        {
            if (_strategy.Name.Equals("IQ_RSL"))
            {
                CalculateIQ_RSLBuyPrices();
            }
            else if (_strategy.Name.Equals("IQ_Increment_Qty"))
            {
                CalculateIQ_IncrementBuyPrices();
            }
            else
            {
                CalculateDSPBuyPrices();
            }
        }

        private void CalculateSellStratPrices()
        {
            if (_strategy.Name.Equals("IQ_RSL"))
            {
                CalculateIQ_RSLSellPrices();
            }
            else if (_strategy.Name.Equals("IQ_Increment_Qty"))
            {
                CalculateIQ_IncrementSellPrices();
            }
            else
            {
                CalculateDSPSellPrices();
            }
        }

        private void OnErrorMessage(string msg, bool fatal)
        {
            ErrorMessage?.Invoke(msg, fatal);
        }

        private void OnLogMessage(string msg)
        {
            LogMessage?.Invoke(msg);
        }

        private void OnTradeStopped(Strategy strategy)
        {
            TradeStopped?.Invoke(_strategy);
        }

        private void OnUpdateTradeStats(Strategy strategy, double buyPrice, int quantity,
                                        double sellPrice)
        {
            UpdateTradeStats?.Invoke(strategy, buyPrice, quantity, sellPrice);
        }

        private void RunBuyStrategy()
        {
            CalculateBuyStratPrices();

            try
            {
                int counter = 0;
                _stiLimitOrder.Side = "B";
                _stiStopOrder.Side = "S";

                while (true)
                {
                    if (Running)
                    {
                        if (LastPrice >= _buyPrices[counter])
                        {
                            _stiLimitOrder.Quantity = _buyQuantityTracker[counter];
                            _stiLimitOrder.LmtPrice = _buyPrices[counter];
                            int ord1;
                            try
                            {
                                ord1 = _stiLimitOrder.SubmitOrder();
                                OnLogMessage("Limit @ " + _buyPrices[counter].ToString() + ", Quantity- " + _buyQuantityTracker[counter].ToString() +
                                ", Symbol- " + _strategy.Symbol + ", Error code- " + ord1.ToString() + "\n");
                            }
                            catch
                            {
                                OnErrorMessage("Error while placing limit buy order.", false);
                                Running = false;
                            }

                            OnUpdateTradeStats(_strategy, _buyPrices[counter], _sellQuantityTracker[counter], _sellPrices[counter]);

                            _stiStopOrder.PriceType = SterlingLib.STIPriceTypes.ptSTISvrStp;
                            _stiStopOrder.Quantity = _sellQuantityTracker[counter];
                            _stiStopOrder.StpPrice = _sellPrices[counter];

                            if (_stopOrderID == null)
                            {
                                SYSTEMTIME st = new SYSTEMTIME();
                                LibWrap.GetSystemTime(st);
                                _stopOrderID = "STSDEMO09: " + st.wYear + st.wMonth + st.wDay + st.wHour + st.wMinute + st.wSecond + st.wMilliseconds;
                                _stiStopOrder.ClOrderID = _stopOrderID;
                                int ord2;
                                try
                                {
                                    ord2 = _stiStopOrder.SubmitOrder();
                                    OnLogMessage("Stop @ " + _sellPrices[counter].ToString() +
                                            ", Quantity- " + _sellQuantityTracker[counter].ToString() + ", Symbol- " + _strategy.Symbol +
                                            ", Error code- " + ord2.ToString() + "\n");

                                }
                                catch
                                {
                                    OnErrorMessage("Error while placing stop sell order.", false);
                                    Running = false;
                                }
                            }
                            else
                            {
                                int ord3;
                                try
                                {
                                    ord3 = _stiStopOrder.ReplaceOrder(0, _stopOrderID);
                                    OnLogMessage("Stop @ " + _sellPrices[counter].ToString() +
                                        ", Quantity- " + _sellQuantityTracker[counter].ToString() + ", Symbol- " + _strategy.Symbol +
                                        ", Error code- " + ord3.ToString() + "\n");
                                    if (ord3 != 0) _stopFailed = true;
                                    else _stopFailed = false;
                                }
                                catch
                                {
                                    OnErrorMessage("Error while placing stop sell order.", false);
                                    Running = false;
                                }

                            }

                            if (counter < _n - 1) counter++;
                            _stratPosTrack = counter;
                        }
                        else if(counter > 0) //Don't try to stop until at least one order was placed
                        {
                            while (_stopFailed)
                            {
                                _stiStopOrder.PriceType = SterlingLib.STIPriceTypes.ptSTISvrStp;
                                _stiStopOrder.Quantity = _sellQuantityTracker[counter - 1];
                                _stiStopOrder.StpPrice = _sellPrices[counter - 1];
                                int ord4;
                                try
                                {
                                    ord4 = _stiStopOrder.ReplaceOrder(0, _stopOrderID);
                                    OnLogMessage("Stop @ " + _sellPrices[counter - 1].ToString() +
                                        ", Quantity- " + _sellQuantityTracker[counter - 1].ToString() + ", Symbol- " + _strategy.Symbol +
                                        ", Error code- " + ord4.ToString() + "\n");
                                    _stopFailed = (ord4 != 0);
                                }
                                catch
                                {
                                    OnErrorMessage("Error while placing stop sell order.", false);
                                    Running = false;
                                }
                            }

                            if (LastPrice != 0)
                            {
                                if (counter > 0)
                                {
                                    if (LastPrice <= _sellPrices[counter - 1])
                                    {
                                        Running = false;
                                        OnLogMessage("The buy price for this strategy fell below the stop price. No more orders will be placed for this strategy.");
                                        OnTradeStopped(_strategy);
                                    }
                                }
                                else
                                {
                                    if (LastPrice <= _sellPrices[counter])
                                    {
                                        Running = false;
                                        OnLogMessage("The buy price for this strategy fell below the stop price. No more orders will be placed for this strategy.");
                                        OnTradeStopped(_strategy);
                                    }
                                }
                            }

                        }

                    }
                    else if(counter > 0)  //Don't try to stop until at least one order was placed
                    {
                        if (_stopFailed)
                        {
                            _stiStopOrder.PriceType = SterlingLib.STIPriceTypes.ptSTISvrStp;
                            _stiStopOrder.Quantity = _sellQuantityTracker[counter - 1];
                            _stiStopOrder.StpPrice = _sellPrices[counter - 1];
                            int ord4;
                            try
                            {
                                ord4 = _stiStopOrder.ReplaceOrder(0, _stopOrderID);
                                OnLogMessage("Stop @ " + _sellPrices[counter - 1].ToString() +
                                    ", Quantity- " + _sellQuantityTracker[counter - 1].ToString() + ", Symbol- " + _strategy.Symbol +
                                    ", Error code- " + ord4.ToString() + "\n");
                                _stopFailed = (ord4 != 0);
                            }
                            catch
                            {
                                OnErrorMessage("Error while placing stop sell order.", false);
                                Running = false;
                            }
                            break;
                        }

                    }

                }
            }
            catch
            {
                OnErrorMessage("Error occurred while trying to run buy strategy.", false);
                _running = false;
            }


        }

        private void RunSellStrategy()
        {
            CalculateSellStratPrices();

            try
            {
                int counter = 0;
                _stiLimitOrder.Side = "S";
                _stiStopOrder.Side = "B";

                while (true)
                {
                    if (Running)
                    {
                        if (LastPrice <= _sellPrices[counter])
                        {
                            _stiLimitOrder.Quantity = _sellQuantityTracker[counter];
                            _stiLimitOrder.LmtPrice = _sellPrices[counter];
                            int ord1;
                            try
                            {
                                ord1 = _stiLimitOrder.SubmitOrder();
                                OnLogMessage("Limit @ " + _sellPrices[counter].ToString() +
                                    ", Quantity- " + _sellQuantityTracker[counter].ToString() + ", Symbol- " + _strategy.Symbol +
                                    ", Error code- " + ord1.ToString() + "\n");
                            }
                            catch
                            {
                                OnErrorMessage("Error occurred while placing limit sell order.", false);
                                Running = false;
                            }

                            OnUpdateTradeStats(_strategy, _sellPrices[counter], _buyQuantityTracker[counter],
                                               _buyPrices[counter]);
                            _stiStopOrder.PriceType = SterlingLib.STIPriceTypes.ptSTISvrStp;
                            _stiStopOrder.Quantity = _buyQuantityTracker[counter];
                            _stiStopOrder.StpPrice = _buyPrices[counter];

                            if (_stopOrderID == null)
                            {
                                SYSTEMTIME st = new SYSTEMTIME();
                                LibWrap.GetSystemTime(st);
                                _stopOrderID = "STSDEMO09: " + st.wYear + st.wMonth + st.wDay + st.wHour + st.wMinute + st.wSecond + st.wMilliseconds;
                                _stiStopOrder.ClOrderID = _stopOrderID;
                                int ord2;
                                try
                                {
                                    ord2 = _stiStopOrder.SubmitOrder();
                                    OnLogMessage("Stop @ " + _buyPrices[counter].ToString() +
                                        ", Quantity- " + _buyQuantityTracker[counter].ToString() + ", Symbol- " + _strategy.Symbol +
                                        ", Error code- " + ord2.ToString() + "\n");
                                }
                                catch
                                {
                                    OnErrorMessage("Error occurred while placing stop buy order.", false);
                                    Running = false;
                                }

                            }
                            else
                            {
                                int ord3;
                                try
                                {
                                    ord3 = _stiStopOrder.ReplaceOrder(0, _stopOrderID);
                                    OnLogMessage("Stop @ " + _buyPrices[counter].ToString() +
                                        ", Quantity- " + _buyQuantityTracker[counter].ToString() + ", Symbol- " + _strategy.Symbol +
                                        ", Error code- " + ord3.ToString() + "\n");
                                    _stopFailed = (ord3 != 0);

                                }
                                catch
                                {
                                    OnErrorMessage("Error occurred while placing stop buy order.", false);
                                    Running = false;
                                }

                            }

                            if (counter < _n - 1) counter++;
                            _stratPosTrack = counter;
                        }
                        else if(counter > 0)
                        {
                            while (_stopFailed)
                            {
                                _stiStopOrder.PriceType = SterlingLib.STIPriceTypes.ptSTISvrStp;
                                _stiStopOrder.Quantity = _buyQuantityTracker[counter - 1];
                                _stiStopOrder.StpPrice = _buyPrices[counter - 1];
                                int ord4;
                                try
                                {
                                    ord4 = _stiStopOrder.ReplaceOrder(0, _stopOrderID);
                                    OnLogMessage("Stop @ " + _buyPrices[counter - 1].ToString() +
                                        ", Quantity- " + _buyQuantityTracker[counter - 1].ToString() + ", Symbol- " + _strategy.Symbol +
                                        ", Error code- " + ord4.ToString() + "\n");
                                    _stopFailed = (ord4 != 0);
                                }
                                catch
                                {
                                    OnErrorMessage("Error while placing stop buy order.", false);
                                    Running = false;
                                }
                            }

                            if (LastPrice != 100000)
                            {
                                if (counter > 0)
                                {
                                    if (LastPrice >= _buyPrices[counter - 1])
                                    {
                                        Running = false;
                                        OnLogMessage("The sell price for this strategy is above the stop price. No more orders will be placed for this strategy. Symbol: " + _strategy.Symbol);
                                        OnTradeStopped(_strategy);
                                    }
                                }
                                else
                                {
                                    if (LastPrice >= _buyPrices[counter])
                                    {
                                        Running = false;
                                        OnLogMessage("The sell price for this strategy is above the stop price. No more orders will be placed for this strategy. Symbol: " + _strategy.Symbol);
                                        OnTradeStopped(_strategy);
                                    }
                                }
                            }
                        }
                    }
                    else if(counter > 0)
                    {
                        if (_stopFailed)
                        {
                            _stiStopOrder.PriceType = SterlingLib.STIPriceTypes.ptSTISvrStp;
                            _stiStopOrder.Quantity = _buyQuantityTracker[counter - 1];
                            _stiStopOrder.StpPrice = _buyPrices[counter - 1];
                            int ord4;
                            try
                            {
                                ord4 = _stiStopOrder.ReplaceOrder(0, _stopOrderID);
                                OnLogMessage("Stop @ " + _buyPrices[counter - 1].ToString() +
                                    ", Quantity- " + _buyQuantityTracker[counter - 1].ToString() + ", Symbol- " + _strategy.Symbol +
                                    ", Error code- " + ord4.ToString() + "\n");
                                if (ord4 != 0) _stopFailed = true;
                                else _stopFailed = false;
                            }
                            catch
                            {
                                OnErrorMessage("Error while placing stop buy order.", false);
                                Running = false;
                            }
                        }
                    }

                }
            }
            catch
            {
                OnErrorMessage("Error occurred while running sell strategy.", false);
                Running = false;
            }
        }

    }
}
