using System;
using System.Collections.Generic;

namespace Quotes
{
    public struct Quote
    {
        public double LastPrice;
        public string Timestamp;
    }

    internal interface IQuotesProvider : IDisposable
    {
        event Action<Dictionary<string, Quote>> QuotesUpdate;

        bool RegisterSymbol(string symbol);

        bool UnregisterSymbol(string symbol);

        string ProviderName { get; }

        void StartGettingQuotes();

        void StopGettingQuotes();

        void UnregisterAllSymbols();
    }
}
