using System.Collections.Generic;

namespace Quotes
{
    internal interface IQuotesView
    {

        string SelectedSymbol { get; }

        void DisplayErrorMessage(string msg);

        void DisplayMessage(string msg);

        void RegisterSymbol(string symbol);

        void UnregisterAllSymbols();

        void UnregisterSymbol(string symbol);

        void UpdateQuotes(Dictionary<string, Quote> quotes);

    }
}
