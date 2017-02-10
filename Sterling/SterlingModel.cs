using Quotes;
using System;
using System.Collections.Generic;

namespace Sterling
{

    internal class SterlingModel
    {
        public event Action<Dictionary<string, Quote>> QuotesUpdate;
        public event Action<Strategy> TradeStopped;
        public event Action<Strategy, double, int, double> UpdateTradeStats;
        public event Action<string> LogMessage;
        public event Action<string, bool> ErrorMessage;

        private Dictionary<Strategy, StrategyExecutor> _strategies;
        private IQuotesProvider _quotesProvider;

        public SterlingModel()
        {
            _strategies = new Dictionary<Strategy, StrategyExecutor>();
            _quotesProvider = new DdfPlusQuotesProvider();
            _quotesProvider.QuotesUpdate += OnQuotesUpdate;
        }

        public bool AddStrategy(Strategy strategy, string exchange, string account,
                                double dpr, int n, double price,
                                double r, int s)
        {
            if (_strategies.ContainsKey(strategy))
            {
                OnErrorMessage("Strategy " + strategy.Name + " already registered to symbol " + strategy.Symbol, false);
                return false;
            }
            _strategies[strategy] = new StrategyExecutor
            (
                strategy, account, exchange,
                price, n, dpr,
                r, s
            );
            _strategies[strategy].ErrorMessage += OnErrorMessage;
            _strategies[strategy].LogMessage += OnLogMessage;
            _strategies[strategy].TradeStopped += OnTradeStopped;
            _strategies[strategy].UpdateTradeStats += OnUpdateTradeStats;
            return true;
        }

        public double CalculatePl(Strategy strategy, double lastPrice)
        {
            return _strategies[strategy].CalcPnL(lastPrice);
        }

        public void CancelAllOrders(Strategy strategy)
        {
            _strategies[strategy].Running = false;
            _strategies[strategy].CancelAllOrders();
        }

        public void StartStrategy(Strategy strategy)
        {
            _strategies[strategy].StartTrade();
        }

        public void StopStrategy(Strategy strategy)
        {
            _strategies[strategy].ErrorMessage -= OnErrorMessage;
            _strategies[strategy].LogMessage -= OnLogMessage;
            _strategies[strategy].TradeStopped -= OnTradeStopped;
            _strategies[strategy].UpdateTradeStats -= OnUpdateTradeStats;
            _strategies[strategy].Running = false;
        }

        public void StopTrade(Strategy strategy)
        {
            _strategies[strategy].StopTrade();
        }

        private void OnErrorMessage(string msg, bool fatal)
        {
            ErrorMessage?.Invoke(msg, fatal);
        }

        private void OnLogMessage(string msg)
        {
            LogMessage?.Invoke(msg);
        }

        private void OnQuotesUpdate(Dictionary<string, Quote> quotes)
        {
            //Update all executors whose symbols were updated
            foreach(KeyValuePair<string,Quote> quote in quotes)
            {
                foreach(KeyValuePair<Strategy, StrategyExecutor> strategy in _strategies)
                {
                    if(strategy.Key.Symbol == quote.Key)
                    {
                        strategy.Value.SetLastPrice(quote.Value.LastPrice);
                    }
                }
            }
            QuotesUpdate?.Invoke(quotes);
        }

        private void OnTradeStopped(Strategy strategy)
        {
            TradeStopped?.Invoke(strategy);
        }

        private void OnUpdateTradeStats(Strategy strategy, double buyPrice, int quantity,
                                        double sellPrice)
        {
            UpdateTradeStats?.Invoke(strategy, buyPrice, quantity, sellPrice);
        }
    }
}
