using System;
using System.Collections.Generic;

namespace Quotes
{
    internal class QuotesPresenter : IDisposable
    {
        private bool disposedValue = false; // To detect redundant calls
        private IQuotesView _view = null;
        private QuotesModel _model = null;

        public QuotesPresenter(IQuotesView view)
        {
            _view = view;
            _model = new QuotesModel();
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }

        public void RegisterNewSymbol(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol))
            {
                _view.DisplayErrorMessage("Symbol name cannot be empty");
            }
            else
            {
                bool success = _model.RegisterSymbol(symbol);
                if (success)
                {
                    _view.RegisterSymbol(symbol);
                    _view.DisplayMessage("Symbol " + symbol + " registered");
                }else
                {
                    _view.DisplayErrorMessage("Symbol already registered");
                }
            }
        }

        public void StartGettingQuotes()
        {
            _model.QuotesUpdate += OnQuotesUpdate;
            _model.StartGettingQuotes();
        }

        public void StopGettingQuotes()
        {
            _model.QuotesUpdate -= OnQuotesUpdate;
            _model.StopGettingQuotes();
        }

        public void UnregisterAllSymbols()
        {
            _model.UnregisterAllSymbols();
            _view.UnregisterAllSymbols();
        }

        public void UnregisterSymbol()
        {
            string selectedSymbol = _view.SelectedSymbol;
            if (string.IsNullOrWhiteSpace(selectedSymbol))
            {
                _view.DisplayErrorMessage("No symbol selected");
            }else
            {
                bool success = _model.UnregisterSymbol(selectedSymbol);
                if (success)
                {
                    _view.UnregisterSymbol(selectedSymbol);
                    _view.DisplayMessage("Symbol " + selectedSymbol + " unregisted");
                }
                
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    _model.Dispose();
                }
                disposedValue = true;
            }
        }

        private void OnQuotesUpdate(Dictionary<string, Quote> quotes)
        {
            _view.UpdateQuotes(quotes);
        }

    }
}
