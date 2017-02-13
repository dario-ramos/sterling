using System;
using System.Collections.Generic;

namespace QuotesProvider
{
    public class MockProvider : BaseQuotesProvider
    {
        private Dictionary<string, Quote> _quotes;
        private double _mean, _stdDev;
        private Random _random;

        public MockProvider()
        {
            _random = new Random();
            _mean = 100;
            _stdDev = 4.0;
            _quotes = new Dictionary<string, Quote>();
        }

        public override string ProviderName
        {
            get
            {
                return "Mock";
            }
        }

        protected override void GetQuotes()
        {
            foreach (string symbol in Symbols)
            {
                _quotes[symbol] = new Quote
                {
                    LastPrice = NextGaussian(),
                    Timestamp = DateTime.Now.ToString()
                };
            }
            OnQuotesUpdate(_quotes);
        }

        private double NextGaussian()
        {
            double u1 = 1.0 - _random.NextDouble(); //uniform(0,1] random doubles
            double u2 = 1.0 - _random.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1) ) * Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            return _mean + _stdDev * randStdNormal; //random normal(mean,stdDev^2)
        }
    }
}
