using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace QuotesProvider
{
    public abstract class BaseQuotesProvider : IQuotesProvider
    {
        public event Action<Dictionary<string, Quote>> QuotesUpdate;

        private bool _gettingQuotes;
        private const int QUOTE_THREAD_SLEEP = 500;
        private HashSet<string> _symbols;
        private object _gettingQuotesLock;
        private object _symbolsLock;

        public BaseQuotesProvider()
        {
            _gettingQuotesLock = new object();
            GettingQuotes = false;
            _symbolsLock = new object();
            _symbols = new HashSet<string>();
        }

        public abstract string ProviderName{ get; }

        public string Password { get; set; }

        public string User { get; set; }

        public bool RegisterSymbol(string symbol)
        {
            lock (_symbolsLock)
            {
                if (_symbols.Contains(symbol))
                {
                    return false;
                }
                _symbols.Add(symbol);
                CustomSymbolRegistration(symbol);
            }
            return true;
        }

        public bool UnregisterSymbol(string symbol)
        {
            lock (_symbolsLock)
            {
                if (!_symbols.Contains(symbol))
                {
                    return false;
                }
                _symbols.Remove(symbol);
                CustomSymbolUnregistration(symbol);
            }
            return true;
        }

        public virtual void CustomSymbolRegistration(string symbol){}

        public virtual void CustomSymbolUnregistration(string symbol){}

        public virtual void CustomUnregisterAllSymbols(){}

        public void StartGettingQuotes()
        {
            if (GettingQuotes)
            {
                return;
            }
            Task.Factory.StartNew(() => { QuotesThread(); });
        }

        public void StopGettingQuotes()
        {
            GettingQuotes = false;
        }

        public void UnregisterAllSymbols()
        {
            lock (_symbolsLock)
            {
                _symbols.Clear();
                CustomUnregisterAllSymbols();
            }
        }

        protected abstract void GetQuotes();

        protected HashSet<string> Symbols
        {
            get
            {
                return _symbols;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        protected void OnQuotesUpdate(Dictionary<string, Quote> quotes)
        {
            QuotesUpdate?.Invoke(quotes);
        }

        private bool GettingQuotes
        {
            get
            {
                lock (_gettingQuotesLock)
                {
                    return _gettingQuotes;
                }
            }
            set
            {
                lock (_gettingQuotesLock)
                {
                    _gettingQuotes = value;
                }
            }
        }

        private void QuotesThread()
        {
            GettingQuotes = true;
            while (GettingQuotes)
            {
                lock (_symbolsLock)
                {
                    if (_symbols.Count > 0)
                    {
                        GetQuotes();
                    }
                }
                Thread.Sleep(QUOTE_THREAD_SLEEP);
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
