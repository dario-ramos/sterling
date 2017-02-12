using QuotesProvider;
using System.Collections.Generic;

namespace Sterling
{
    public interface ISterlingView
    {
        void LogMessage(string msg);

        void ShowMessage(string msg, bool fatalError);

        void StopStrategy(Strategy strategy);

        void UpdateQuotes(Dictionary<string, Quote> quotes);

        void UpdateTradeStats(Strategy strategy, double buyPrice, int quantity,
                              double sellPrice);
    }
}
