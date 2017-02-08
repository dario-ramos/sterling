using System;
using System.Collections.Generic;

namespace Quotes
{
    internal class QuotesModel
    {
        public event Action<Dictionary<string, double>> QuotesUpdate;

        private IQuotesProvider _quotesProvider;

        public QuotesModel()
        {
            _quotesProvider = new DdfPlusQuotesProvider();
        }

        public bool RegisterSymbol(string symbol)
        {
            return _quotesProvider.RegisterSymbol(symbol);
        }

        public void StartGettingQuotes()
        {
            _quotesProvider.QuotesUpdate += OnQuotesUpdate;
            _quotesProvider.StartGettingQuotes();
        }

        public void StopGettingQuotes()
        {
            _quotesProvider.QuotesUpdate -= OnQuotesUpdate;
            _quotesProvider.StopGettingQuotes();
        }

        public bool UnregisterSymbol(string symbol)
        {
            return _quotesProvider.UnregisterSymbol(symbol);
        }

        public void UnregisterAllSymbols()
        {
            _quotesProvider.UnregisterAllSymbols();
        }

        private void OnQuotesUpdate(Dictionary<string, double> quotes)
        {
            QuotesUpdate?.Invoke(quotes);
        }

    }
}
