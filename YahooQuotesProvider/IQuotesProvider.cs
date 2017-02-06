using System;
using System.Collections.Generic;

namespace Quotes
{
    internal interface IQuotesProvider
    {
        event Action<Dictionary<string, double>> QuotesUpdate;

        bool RegisterSymbol(string symbol);

        bool UnregisterSymbol(string symbol);

        void StartGettingQuotes();

        void StopGettingQuotes();

        void UnregisterAllSymbols();
    }
}
