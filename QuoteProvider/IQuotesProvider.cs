﻿using System;
using System.Collections.Generic;

namespace QuotesProvider
{
    public struct Quote
    {
        public double LastPrice;
        public string Timestamp;
    }

    public interface IQuotesProvider : IDisposable
    {
        event Action<Dictionary<string, Quote>> QuotesUpdate;

        bool RegisterSymbol(string symbol);

        bool UnregisterSymbol(string symbol);

        string Password { set; }

        string ProviderName { get; }

        string User { set; }

        void StartGettingQuotes();

        void StopGettingQuotes();

        void UnregisterAllSymbols();
    }
}
