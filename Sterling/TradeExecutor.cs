using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Sterling
{
    //<CHG> Moved this class to a separate file (it was inside Form1.cs)
    public class TradeExecutor
    {
        public event Action<string> tradeStopped; //<CHG> Added this event to notify that trade stopped

        private string account;
        private string exchange;
        private string symbol;
        private string strategy;
        private double price;
        private int quantity;
        private double dpr;
        private double r;
        private int s;
        private double[] buyPrices;
        private double[] sellPrices;
        private int[] buyQuantityTracker;
        private int[] sellQuantityTracker;
        private int n = 1000;
        private SterlingLib.STIOrder stiLimitOrder;
        private SterlingLib.STIOrder stiStopOrder;
        private SterlingLib.STIOrder stiClosingOrder;
        private SterlingLib.STIQuote stiQuote;
        private SterlingLib.STIApp stiApp;

        private double lastPrice;
        private string stopOrderID;
        private int stratPosTrack;
        //private int runningRow;
        private DataGridViewRow runningRow;
        private Form1 mForm;
        private string side;
        private int formPos;
        private bool stopFailed = false;

        public TradeExecutor(string acct, string sym, string ex, string strat, double p, int quant, double DPR, double R, int S, Form1 mf)
        {
            try
            {
                this.account = acct;
                this.symbol = sym;
                this.exchange = ex;
                this.strategy = strat;
                this.price = p;
                this.quantity = quant;
                this.dpr = DPR;
                this.r = R;
                this.s = S;
                this.buyPrices = new double[n];
                this.sellPrices = new double[n];
                this.buyQuantityTracker = new int[n];
                this.sellQuantityTracker = new int[n];
                this.stiLimitOrder = new SterlingLib.STIOrder();
                this.stiStopOrder = new SterlingLib.STIOrder();
                this.stiClosingOrder = new SterlingLib.STIOrder();
                this.stiQuote = new SterlingLib.STIQuote();
                this.stiApp = new SterlingLib.STIApp();
                this.stiApp.SetModeXML(true);
                this.stiQuote.DeRegisterAllQuotes();
                this.stiQuote.RegisterQuote(symbol, "");

                this.stiLimitOrder.Tif = "D";
                this.stiLimitOrder.Destination = "NSDQ";
                this.stiLimitOrder.Account = account;
                this.stiLimitOrder.Symbol = symbol;
                this.stiLimitOrder.PriceType = SterlingLib.STIPriceTypes.ptSTILmt;
                this.stiStopOrder.Tif = "D";
                this.stiStopOrder.Destination = "NSDQ";
                this.stiStopOrder.Account = account;
                this.stiStopOrder.Symbol = symbol;
                this.stiClosingOrder.Tif = "D";
                this.stiClosingOrder.Destination = "NSDQ";
                this.stiClosingOrder.Account = account;
                this.stiClosingOrder.Symbol = symbol;
                this.stiClosingOrder.PriceType = SterlingLib.STIPriceTypes.ptSTIMkt;
                //this.stiStopOrder.PriceType = SterlingLib.STIPriceTypes.ptSTISvrStp;
                this.stopOrderID = null;
                this.stratPosTrack = 0;
                this.mForm = mf;
            }
            catch
            {
                MessageBox.Show("Error occurred in the trade executor constructor.");
                System.Windows.Forms.Application.ExitThread();
            }

        }

        public string Symbol
        {
            get
            {
                return symbol;
            }
            private set { }
        }

        public void runStrategy(ref DataGridViewRow row, string s, int fPos)
        {
            this.runningRow = row;
            this.formPos = fPos;
            this.side = s;
            justUpdate();
            if (s == "B")
            {
                this.lastPrice = 0;
                runBuyStrategy();
            }
            else
            {
                this.lastPrice = 100000;
                runSellStrategy();
            }
        }

        public void runBuyStrategy()
        {

            double weightedSum;
            int netQuantity;

            if (strategy.Equals("IQ_RSL"))
            {
                weightedSum = 0;
                netQuantity = 0;

                for (int j = 0; j < s; j++)
                {
                    buyPrices[j] = price + j * dpr;
                    buyQuantityTracker[j] = quantity;
                    weightedSum += buyPrices[j] * quantity;
                    netQuantity += quantity;
                    double weightedAvg = weightedSum / netQuantity;
                    sellPrices[j] = weightedAvg - (r / (quantity * (j + 1)));
                    sellQuantityTracker[j] = netQuantity;
                }

                for (int k = s; k < n; k++)
                {
                    buyPrices[k] = price + k * dpr;
                    buyQuantityTracker[k] = quantity;
                    weightedSum += buyPrices[k] * quantity;
                    netQuantity += quantity;
                    double weightedAvg = weightedSum / netQuantity;
                    sellPrices[k] = weightedAvg;
                    sellQuantityTracker[k] = netQuantity;
                }

            }
            else if (strategy.Equals("IQ_Increment_Qty"))
            {
                weightedSum = 0;
                netQuantity = 0;
                //int tempQuant = quantity;
                for (int j = 0; j < s; j++)
                {
                    buyPrices[j] = price + j * dpr;
                    //tempQuant += j * quantity;
                    int tempQuant = quantity + j * quantity;
                    buyQuantityTracker[j] = tempQuant;
                    //buyQuantityTracker[j] = tempQuant;
                    weightedSum += buyPrices[j] * buyQuantityTracker[j];
                    netQuantity += tempQuant;
                    sellQuantityTracker[j] = netQuantity;
                    double weightedAvg = weightedSum / netQuantity;
                    sellPrices[j] = weightedAvg - (r / (netQuantity * (j + 1)));
                }

                for (int k = s; k < n; k++)
                {
                    buyPrices[k] = price + k * dpr;
                    int tempQuant = quantity + k * quantity;
                    buyQuantityTracker[k] = tempQuant;
                    weightedSum += buyPrices[k] * buyQuantityTracker[k];
                    netQuantity += tempQuant;
                    sellQuantityTracker[k] = netQuantity;
                    double weightedAvg = weightedSum / netQuantity;
                    sellPrices[k] = weightedAvg;
                }

            }
            else
            {
                weightedSum = r * s;
                netQuantity = s;

                for (int j = 0; j < n; j++)
                {
                    buyPrices[j] = price + j * dpr;
                    buyQuantityTracker[j] = quantity;
                    weightedSum += buyPrices[j] * buyQuantityTracker[j];
                    netQuantity += quantity;
                    sellQuantityTracker[j] = netQuantity;
                    double weightedAvg = weightedSum / netQuantity;
                    sellPrices[j] = weightedAvg;
                }

            }

            try
            {
                stiQuote.OnSTIQuoteUpdateXML += new SterlingLib._ISTIQuoteEvents_OnSTIQuoteUpdateXMLEventHandler(OnSTIQuoteUpdateXML_Buy);

                int counter = 0;
                stiLimitOrder.Side = "B";
                stiStopOrder.Side = "S";

                while (true)
                {
                    if ((bool)mForm.checkRun[formPos])
                    {
                        if (lastPrice >= buyPrices[counter])
                        {
                            stiLimitOrder.Quantity = buyQuantityTracker[counter];
                            stiLimitOrder.LmtPrice = buyPrices[counter];
                            int ord1;
                            try
                            {
                                ord1 = stiLimitOrder.SubmitOrder();
                                mForm.AppendText("Limit @ " + buyPrices[counter].ToString() + ", Quantity- " + buyQuantityTracker[counter].ToString() +
                                ", Symbol- " + symbol + ", Error code- " + ord1.ToString() + "\n");
                            }
                            catch
                            {
                                MessageBox.Show("Error while placing limit buy order.");
                                System.Windows.Forms.Application.ExitThread();
                            }
                            /*if (ord1 != 0)
                            {
                                mForm.AppendText("Error placing the order- Limit @ " + buyPrices[counter].ToString() +
                                    ", Quantity- " + buyQuantityTracker[counter].ToString() + ", Symbol- " + symbol +
                                    ", Error code- " + ord1.ToString() + "\n");
                            }*/


                            runningRow.Cells[1].Value = buyPrices[counter].ToString();
                            runningRow.Cells[2].Value = sellQuantityTracker[counter].ToString();
                            runningRow.Cells[4].Value = sellPrices[counter].ToString();

                            stiStopOrder.PriceType = SterlingLib.STIPriceTypes.ptSTISvrStp;
                            stiStopOrder.Quantity = sellQuantityTracker[counter];
                            stiStopOrder.StpPrice = sellPrices[counter];

                            if (stopOrderID == null)
                            {
                                SYSTEMTIME st = new SYSTEMTIME();
                                LibWrap.GetSystemTime(st);
                                stopOrderID = "STSDEMO09: " + st.wYear + st.wMonth + st.wDay + st.wHour + st.wMinute + st.wSecond + st.wMilliseconds;
                                stiStopOrder.ClOrderID = stopOrderID;
                                int ord2;
                                try
                                {
                                    ord2 = stiStopOrder.SubmitOrder();
                                    mForm.AppendText("Stop @ " + sellPrices[counter].ToString() +
                                            ", Quantity- " + sellQuantityTracker[counter].ToString() + ", Symbol- " + symbol +
                                            ", Error code- " + ord2.ToString() + "\n");

                                }
                                catch
                                {
                                    MessageBox.Show("Error while placing stop sell order.");
                                    System.Windows.Forms.Application.ExitThread();
                                }
                            }
                            else
                            {
                                int ord3;
                                try
                                {
                                    ord3 = stiStopOrder.ReplaceOrder(0, stopOrderID);
                                    mForm.AppendText("Stop @ " + sellPrices[counter].ToString() +
                                        ", Quantity- " + sellQuantityTracker[counter].ToString() + ", Symbol- " + symbol +
                                        ", Error code- " + ord3.ToString() + "\n");
                                    if (ord3 != 0) stopFailed = true;
                                    else stopFailed = false;
                                }
                                catch
                                {
                                    MessageBox.Show("Error while placing stop sell order.");
                                    System.Windows.Forms.Application.ExitThread();
                                }
                                /*if (ord3 != 0)
                                {
                                    mForm.AppendText("Error placing the order- Stop @ " + sellPrices[counter].ToString() +
                                        ", Quantity- " + sellQuantityTracker[counter].ToString() + ", Symbol- " + symbol +
                                        ", Error code- " + ord3.ToString() + "\n");
                                }*/

                            }

                            if (counter < n - 1) counter++;
                            stratPosTrack = counter;
                        }
                        else
                        {
                            while (stopFailed)
                            {
                                stiStopOrder.PriceType = SterlingLib.STIPriceTypes.ptSTISvrStp;
                                stiStopOrder.Quantity = sellQuantityTracker[counter - 1];
                                stiStopOrder.StpPrice = sellPrices[counter - 1];
                                int ord4;
                                try
                                {
                                    ord4 = stiStopOrder.ReplaceOrder(0, stopOrderID);
                                    mForm.AppendText("Stop @ " + sellPrices[counter - 1].ToString() +
                                        ", Quantity- " + sellQuantityTracker[counter - 1].ToString() + ", Symbol- " + symbol +
                                        ", Error code- " + ord4.ToString() + "\n");
                                    if (ord4 != 0) //<CHG> Added call to fire trade stopped event
                                    {
                                        stopFailed = true;
                                    }
                                    else
                                    {
                                        stopFailed = false;
                                        OnTradeStopped(symbol);
                                    }
                                }
                                catch
                                {
                                    MessageBox.Show("Error while placing stop sell order.");
                                    System.Windows.Forms.Application.ExitThread();
                                }
                            }

                            if (lastPrice != 0)
                            {
                                if (counter > 0)
                                {
                                    if (lastPrice <= sellPrices[counter - 1])
                                    {
                                        mForm.checkRun[formPos] = false;
                                        MessageBox.Show("The buy price for this strategy fell below the stop price. No more orders will be placed for this strategy.");
                                    }
                                }
                                else
                                {
                                    if (lastPrice <= sellPrices[counter])
                                    {
                                        mForm.checkRun[formPos] = false;
                                        MessageBox.Show("The buy price for this strategy fell below the stop price. No more orders will be placed for this strategy.");
                                    }
                                }
                            }

                        }

                    }
                    else
                    {
                        if (stopFailed)
                        {
                            stiStopOrder.PriceType = SterlingLib.STIPriceTypes.ptSTISvrStp;
                            stiStopOrder.Quantity = sellQuantityTracker[counter - 1];
                            stiStopOrder.StpPrice = sellPrices[counter - 1];
                            int ord4;
                            try
                            {
                                ord4 = stiStopOrder.ReplaceOrder(0, stopOrderID);
                                mForm.AppendText("Stop @ " + sellPrices[counter - 1].ToString() +
                                    ", Quantity- " + sellQuantityTracker[counter - 1].ToString() + ", Symbol- " + symbol +
                                    ", Error code- " + ord4.ToString() + "\n");
                                if (ord4 != 0) stopFailed = true;
                                else stopFailed = false;
                            }
                            catch
                            {
                                MessageBox.Show("Error while placing stop sell order.");
                                System.Windows.Forms.Application.ExitThread();
                            }
                            break;
                        }

                    }

                }
            }
            catch
            {
                MessageBox.Show("Error occurred while trying to run buy strategy.");
                System.Windows.Forms.Application.ExitThread();
            }


        }

        public void justUpdate()
        {
            try
            {
                if (this.side == "B") stiQuote.OnSTIQuoteUpdateXML += new SterlingLib._ISTIQuoteEvents_OnSTIQuoteUpdateXMLEventHandler(OnSTIQuoteUpdateXML_Buy);
                else stiQuote.OnSTIQuoteUpdateXML += new SterlingLib._ISTIQuoteEvents_OnSTIQuoteUpdateXMLEventHandler(OnSTIQuoteUpdateXML_Sell);
            }
            catch
            {
                MessageBox.Show("Error occurred while updating LTP and PnL.");
                System.Windows.Forms.Application.ExitThread();
            }

        }

        public void OnSTIQuoteUpdateXML_Buy(ref string strQuote)
        {
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(SterlingLib.structSTIQuoteUpdate));
                SterlingLib.structSTIQuoteUpdate structQuote = (SterlingLib.structSTIQuoteUpdate)xs.Deserialize(new StringReader(strQuote));

                if (structQuote.bLastPrice == 1)
                {
                    runningRow.Cells[3].Value = structQuote.fLastPrice.ToString();
                    runningRow.Cells[5].Value = calcPnL(structQuote.fLastPrice, "B");
                    lastPrice = structQuote.fLastPrice;
                }


            }
            catch
            {
                MessageBox.Show("Error occurred inside OnSTIQuoteUpdateXML_Buy");
                System.Windows.Forms.Application.ExitThread();
            }

        }

        public string calcPnL(double currPrice, string side)
        {
            double sellAmt = 0;
            double buyAmt = 0;
            if (stratPosTrack > 0)
            {
                if (side == "B")
                {
                    sellAmt = currPrice * sellQuantityTracker[stratPosTrack - 1];
                    buyAmt = 0;
                    for (int i = 0; i < stratPosTrack; i++)
                    {
                        buyAmt += buyPrices[i] * buyQuantityTracker[i];
                    }

                }
                else
                {
                    buyAmt = currPrice * buyQuantityTracker[stratPosTrack - 1];
                    sellAmt = 0;
                    for (int i = 0; i < stratPosTrack; i++)
                    {
                        sellAmt += sellPrices[i] * sellQuantityTracker[i];
                    }
                }
            }
            double pnl = sellAmt - buyAmt;
            pnl = Math.Round(pnl, 2); //<CHG> Was not rounding because argument is passed as a copy
            return pnl.ToString();

        }

        public void runSellStrategy()
        {

            double weightedSum;
            int netQuantity;

            if (strategy.Equals("IQ_RSL"))
            {
                weightedSum = 0;
                netQuantity = 0;

                for (int j = 0; j < s; j++)
                {
                    sellPrices[j] = price - j * dpr;
                    sellQuantityTracker[j] = quantity;
                    weightedSum += sellPrices[j] * quantity;
                    netQuantity += quantity;
                    double weightedAvg = weightedSum / netQuantity;
                    buyPrices[j] = weightedAvg + (r / (quantity * (j + 1)));
                    buyQuantityTracker[j] = netQuantity;
                }

                for (int k = s; k < n; k++)
                {
                    sellPrices[k] = price - k * dpr;
                    sellQuantityTracker[k] = quantity;
                    weightedSum += sellPrices[k] * quantity;
                    netQuantity += quantity;
                    double weightedAvg = weightedSum / netQuantity;
                    buyPrices[k] = weightedAvg;
                    buyQuantityTracker[k] = netQuantity;
                }

            }
            else if (strategy.Equals("IQ_Increment_Qty"))
            {
                weightedSum = 0;
                netQuantity = 0;
                //int tempQuant = quantity;
                for (int j = 0; j < s; j++)
                {
                    sellPrices[j] = price - j * dpr;
                    int tempQuant = quantity + j * quantity;
                    sellQuantityTracker[j] = tempQuant;
                    weightedSum += sellPrices[j] * sellQuantityTracker[j];
                    netQuantity += tempQuant;
                    buyQuantityTracker[j] = netQuantity;
                    double weightedAvg = weightedSum / netQuantity;
                    buyPrices[j] = weightedAvg + (r / (netQuantity * (j + 1)));
                }

                for (int k = s; k < n; k++)
                {
                    sellPrices[k] = price - k * dpr;
                    int tempQuant = quantity + k * quantity;
                    sellQuantityTracker[k] = tempQuant;
                    weightedSum += sellPrices[k] * sellQuantityTracker[k];
                    netQuantity += tempQuant;
                    buyQuantityTracker[k] = netQuantity;
                    double weightedAvg = weightedSum / netQuantity;
                    buyPrices[k] = weightedAvg;
                }

            }
            else
            {
                weightedSum = r * s;
                netQuantity = s;

                for (int j = 0; j < n; j++)
                {
                    sellPrices[j] = price - j * dpr;
                    sellQuantityTracker[j] = quantity;
                    weightedSum += sellPrices[j] * sellQuantityTracker[j];
                    netQuantity += quantity;
                    buyQuantityTracker[j] = netQuantity;
                    double weightedAvg = weightedSum / netQuantity;
                    buyPrices[j] = weightedAvg;
                }

            }

            try
            {
                stiQuote.OnSTIQuoteUpdateXML += new SterlingLib._ISTIQuoteEvents_OnSTIQuoteUpdateXMLEventHandler(OnSTIQuoteUpdateXML_Sell);

                int counter = 0;
                stiLimitOrder.Side = "S";
                stiStopOrder.Side = "B";

                while (true)
                {
                    if ((bool)mForm.checkRun[formPos])
                    {
                        if (lastPrice <= sellPrices[counter])
                        {
                            stiLimitOrder.Quantity = sellQuantityTracker[counter];
                            stiLimitOrder.LmtPrice = sellPrices[counter];
                            int ord1;
                            try
                            {
                                ord1 = stiLimitOrder.SubmitOrder();
                                mForm.AppendText("Limit @ " + sellPrices[counter].ToString() +
                                    ", Quantity- " + sellQuantityTracker[counter].ToString() + ", Symbol- " + symbol +
                                    ", Error code- " + ord1.ToString() + "\n");
                            }
                            catch
                            {
                                MessageBox.Show("Error occurred while placing limit sell order.");
                                System.Windows.Forms.Application.ExitThread();
                            }

                            /*
                            if (ord1 != 0)
                            {
                                mForm.AppendText("Error placing the order- Limit @ " + sellPrices[counter].ToString() +
                                    ", Quantity- " + sellQuantityTracker[counter].ToString() + ", Symbol- " + symbol +
                                    ", Error code- " + ord1.ToString() + "\n");
                            }*/


                            runningRow.Cells[1].Value = sellPrices[counter].ToString();
                            runningRow.Cells[2].Value = buyQuantityTracker[counter].ToString();
                            runningRow.Cells[4].Value = buyPrices[counter].ToString();

                            stiStopOrder.PriceType = SterlingLib.STIPriceTypes.ptSTISvrStp;
                            stiStopOrder.Quantity = buyQuantityTracker[counter];
                            stiStopOrder.StpPrice = buyPrices[counter];

                            if (stopOrderID == null)
                            {
                                SYSTEMTIME st = new SYSTEMTIME();
                                LibWrap.GetSystemTime(st);
                                stopOrderID = "STSDEMO09: " + st.wYear + st.wMonth + st.wDay + st.wHour + st.wMinute + st.wSecond + st.wMilliseconds;
                                stiStopOrder.ClOrderID = stopOrderID;
                                int ord2;
                                try
                                {
                                    ord2 = stiStopOrder.SubmitOrder();
                                    mForm.AppendText("Stop @ " + buyPrices[counter].ToString() +
                                        ", Quantity- " + buyQuantityTracker[counter].ToString() + ", Symbol- " + symbol +
                                        ", Error code- " + ord2.ToString() + "\n");
                                }
                                catch
                                {
                                    MessageBox.Show("Error occurred while placing stop buy order.");
                                    System.Windows.Forms.Application.ExitThread();
                                }

                                /*
                                if (ord2 != 0)
                                {
                                    mForm.AppendText("Error placing the order- Stop @ " + buyPrices[counter].ToString() +
                                        ", Quantity- " + buyQuantityTracker[counter].ToString() + ", Symbol- " + symbol +
                                        ", Error code- " + ord2.ToString() + "\n");
                                }*/


                            }
                            else
                            {
                                int ord3;
                                try
                                {
                                    ord3 = stiStopOrder.ReplaceOrder(0, stopOrderID);
                                    mForm.AppendText("Stop @ " + buyPrices[counter].ToString() +
                                        ", Quantity- " + buyQuantityTracker[counter].ToString() + ", Symbol- " + symbol +
                                        ", Error code- " + ord3.ToString() + "\n");
                                    if (ord3 != 0) stopFailed = true;
                                    else stopFailed = false;

                                }
                                catch
                                {
                                    MessageBox.Show("Error occurred while placing stop buy order.");
                                    System.Windows.Forms.Application.ExitThread();
                                }

                                /*if(ord3 != 0)
                                {
                                    mForm.AppendText("Error placing the order- Stop @ " + buyPrices[counter].ToString() +
                                        ", Quantity- " + buyQuantityTracker[counter].ToString() + ", Symbol- " + symbol +
                                        ", Error code- " + ord3.ToString() + "\n");
                                }*/

                            }

                            if (counter < n - 1) counter++;
                            stratPosTrack = counter;
                        }
                        else
                        {
                            while (stopFailed)
                            {
                                stiStopOrder.PriceType = SterlingLib.STIPriceTypes.ptSTISvrStp;
                                stiStopOrder.Quantity = buyQuantityTracker[counter - 1];
                                stiStopOrder.StpPrice = buyPrices[counter - 1];
                                int ord4;
                                try
                                {
                                    ord4 = stiStopOrder.ReplaceOrder(0, stopOrderID);
                                    mForm.AppendText("Stop @ " + buyPrices[counter - 1].ToString() +
                                        ", Quantity- " + buyQuantityTracker[counter - 1].ToString() + ", Symbol- " + symbol +
                                        ", Error code- " + ord4.ToString() + "\n");
                                    if (ord4 != 0) //<CHG> Fire tradeStopped event
                                    {
                                        stopFailed = true;
                                    }
                                    else
                                    {
                                        OnTradeStopped(symbol);
                                        stopFailed = false;
                                    }
                                }
                                catch
                                {
                                    MessageBox.Show("Error while placing stop buy order.");
                                    System.Windows.Forms.Application.ExitThread();
                                }
                            }

                            if (lastPrice != 100000)
                            {
                                if (counter > 0)
                                {
                                    if (lastPrice >= buyPrices[counter - 1])
                                    {
                                        mForm.checkRun[formPos] = false;
                                        MessageBox.Show("The sell price for this strategy is above the stop price. No more orders will be placed for this strategy.");
                                    }
                                }
                                else
                                {
                                    if (lastPrice >= buyPrices[counter])
                                    {
                                        mForm.checkRun[formPos] = false;
                                        MessageBox.Show("The sell price for this strategy is above the stop price. No more orders will be placed for this strategy.");
                                    }
                                }
                            }


                        }


                    }
                    else
                    {
                        if (stopFailed)
                        {
                            stiStopOrder.PriceType = SterlingLib.STIPriceTypes.ptSTISvrStp;
                            stiStopOrder.Quantity = buyQuantityTracker[counter - 1];
                            stiStopOrder.StpPrice = buyPrices[counter - 1];
                            int ord4;
                            try
                            {
                                ord4 = stiStopOrder.ReplaceOrder(0, stopOrderID);
                                mForm.AppendText("Stop @ " + buyPrices[counter - 1].ToString() +
                                    ", Quantity- " + buyQuantityTracker[counter - 1].ToString() + ", Symbol- " + symbol +
                                    ", Error code- " + ord4.ToString() + "\n");
                                if (ord4 != 0) stopFailed = true;
                                else stopFailed = false;
                            }
                            catch
                            {
                                MessageBox.Show("Error while placing stop buy order.");
                                System.Windows.Forms.Application.ExitThread();
                            }
                        }
                    }

                }
            }
            catch
            {
                MessageBox.Show("Error occurred while running sell strategy.");
                System.Windows.Forms.Application.ExitThread();
            }


        }

        public void OnSTIQuoteUpdateXML_Sell(ref string strQuote)
        {
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(SterlingLib.structSTIQuoteUpdate));
                SterlingLib.structSTIQuoteUpdate structQuote = (SterlingLib.structSTIQuoteUpdate)xs.Deserialize(new StringReader(strQuote));

                if (structQuote.bLastPrice == 1)
                {
                    runningRow.Cells[3].Value = structQuote.fLastPrice.ToString();
                    runningRow.Cells[5].Value = calcPnL(structQuote.fLastPrice, "S");
                    lastPrice = structQuote.fLastPrice;
                }


            }
            catch
            {
                MessageBox.Show("Error occurred inside OnSTIQuoteUpdateXML_Buy");
                System.Windows.Forms.Application.ExitThread();
            }

        }

        public void stopTrade()
        {
            if (side == "B")
            {
                this.stiClosingOrder.Side = "S";
                if (stratPosTrack > 0)
                {
                    this.stiClosingOrder.Quantity = sellQuantityTracker[stratPosTrack - 1];
                    int ord = stiClosingOrder.SubmitOrder();
                    if (ord != 0)
                    {
                        MessageBox.Show("Error occurred while trying to Stop the strategy. Order error code- " + ord.ToString());
                    }
                }


            }
            else
            {
                this.stiClosingOrder.Side = "B";
                if (stratPosTrack > 0)
                {
                    this.stiClosingOrder.Quantity = buyQuantityTracker[stratPosTrack - 1];
                    int ord = stiClosingOrder.SubmitOrder();
                    if (ord != 0)
                    {
                        MessageBox.Show("Error occurred while trying to Stop the strategy. Order error code- " + ord.ToString());
                    }
                }

            }
        }

        public void cancelAllOrders() //<CHG> new method
        {
            //Retrieve all open orders for this symbol and account
            SterlingLib.STIOrderMaint oMaint = new SterlingLib.STIOrderMaint();
            Array orders = null;
            var filter = new SterlingLib.structSTIOrderFilter();
            filter.bOpenOnly = 1;
            filter.bstrAccount = account;
            filter.bstrSymbol = symbol;
            oMaint.GetOrderListEx(ref filter, ref orders);
            //Cancel all those orders
            foreach(SterlingLib.structSTIOrderUpdate pendingOrder in orders)
            {
                oMaint.CancelOrder(account, pendingOrder.nOrderRecordId, "", "");
            }
        }

        private void OnTradeStopped(string symbol) //<CHG> Added this method to fire the event
        {
            tradeStopped?.Invoke(symbol);
        }

    }
}
