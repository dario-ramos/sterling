using System;
using System.Collections.Generic;

namespace Quotes
{
    internal class QuotesPresenter
    {
        private IQuotesView _view = null;
        private QuotesModel _model = null;

        public QuotesPresenter(IQuotesView view)
        {
            _view = view;
            _model = new QuotesModel();
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

        private void OnQuotesUpdate(Dictionary<string,double> quotes)
        {
            _view.UpdateQuotes(quotes);
        }

    }
}
