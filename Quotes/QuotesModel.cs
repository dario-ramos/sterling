using System;
using System.Collections.Generic;

namespace Quotes
{
    internal class QuotesModel : IDisposable
    {
        public event Action<Dictionary<string, Quote>> QuotesUpdate;

        private bool _disposedValue; // To detect redundant calls
        private IQuotesProvider _quotesProvider;

        public QuotesModel()
        {
            _disposedValue = false;
            _quotesProvider = new DdfPlusQuotesProvider();
        }

        public string ProviderName
        {
            get
            {
                return _quotesProvider.ProviderName;
            }
        }

        public bool RegisterSymbol(string symbol)
        {
            return _quotesProvider.RegisterSymbol(symbol);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
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

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    _quotesProvider.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.
                _disposedValue = true;
            }
        }

        private void OnQuotesUpdate(Dictionary<string, Quote> quotes)
        {
            QuotesUpdate?.Invoke(quotes);
        }


    }
}
