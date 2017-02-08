using System.Collections.Generic;

namespace Sterling
{
    internal class SterlingModel
    {
        private Dictionary<string, TradeExecutor> _strategies;

        public SterlingModel()
        {
            _strategies = new Dictionary<string, TradeExecutor>();
        }

        public void CancelAllOrders(string symbol)
        {
            _strategies[symbol].Running = false;
            _strategies[symbol].CancelAllOrders();
        }

        public void StopStrategy(string symbol)
        {
            _strategies[symbol].Running = false;
        }
    }
}
