﻿using ddfplus;
using System;
using System.Collections.Generic;

namespace Quotes
{
    internal class DdfPlusQuotesProvider : BaseQuotesProvider
    {
        private Client _client;
        private const string USER = "ajain";
        private const string PASS = "devtest";
        private Dictionary<string, double> _quotes;
        private object _quotesLock;

        public DdfPlusQuotesProvider(){
            _quotes = new Dictionary<string, double>();
            _quotesLock = new object();

            // The streaming version must be set prior to creating any clients expected to work for that version
            Connection.Properties["streamingversion"] = "3";

            _client = new Client();
            _client.NewQuote += new Client.NewQuoteEventHandler(OnNewQuote);
            //_client.NewBookQuote += new Client.NewBookQuoteEventHandler(OnNewBookQuote);
            //_client.NewTimestamp += new Client.NewTimestampEventHandler(OnNewTimestamp);
            //_client.NewOHLCQuote += new Client.NewOHLCQuoteEventHandler(OnNewOHLCQuote);
            //_client.NewMessage += new Client.NewMessageEventHandler(OnNewMessage);

            UpdateConnectionSettings();
        }

        public override void ExtraSymbolRegistration(string symbol)
        {
            string symbolList = "";
            foreach(string s in Symbols)
            {
                symbolList += s + "=Ss,";
            }
            symbolList = symbolList.Substring(0, symbolList.Length - 1);
            _client.Symbols = symbolList;
        }

        protected override void GetQuotes()
        {
            lock (_quotesLock)
            {
                ExtraSymbolRegistration("");
                OnQuotesUpdate(_quotes);
            }
        }

        private void OnNewQuote(object sender, Client.NewQuoteEventArgs e)
        {
            lock (_quotesLock)
            {
                Quote quote = e.Quote;
                Session session = quote.Sessions["combined"];
                _quotes[quote.Symbol] = session.Last;
            }
        }

        private void UpdateConnectionSettings()
        {
            Connection.Username = USER;
            Connection.Password = PASS;
            Connection.Mode = ConnectionMode.TCPClient;
            //Let ddf figure out Server and Port
            Connection.Server = "";
            Connection.Port = 0;
        }
    }
}
