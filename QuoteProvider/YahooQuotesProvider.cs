using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace QuotesProvider
{
    public class YahooQuotesProvider : BaseQuotesProvider
    {
        private const int REQUEST_TIMEOUT = 20000;
        private const string CONTENT_TYPE = "application/json;charset=utf-8";
        private const string SYMBOLS_PLACEHOLDER = "###SYMBOLS_PLACEHOLDER###";
        private const string SYMBOLS_REQUEST_SEPARATOR = ",";
        private const string REQUEST_URI = "https://query.yahooapis.com/v1/public/yql?q=select%20symbol%2CLastTradePriceOnly%20from%20yahoo.finance.quote%20where%20symbol%20in%20(" + SYMBOLS_PLACEHOLDER + ")&format=json&diagnostics=false&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys&callback=";

        public YahooQuotesProvider()
        {
        }

        public override string ProviderName
        {
            get
            {
                return "Yahoo";
            }
        }

        private void HandleResponse(WebResponse response)
        {
            using (Stream responseStream = response.GetResponseStream())
            using (StreamReader sr = new StreamReader(responseStream))
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                dynamic obj = js.Deserialize<dynamic>(sr.ReadToEnd());
                var quotes = new Dictionary<string, Quote>();
                if( obj["query"]["results"]["quote"] is object[] )
                {
                    foreach (dynamic quote in obj["query"]["results"]["quote"])
                    {
                        quotes.Add(quote["symbol"], new QuotesProvider.Quote { LastPrice = double.Parse(quote["LastTradePriceOnly"]) });
                    }
                }else
                {
                    quotes.Add(obj["query"]["results"]["quote"]["symbol"], double.Parse(obj["query"]["results"]["quote"]["LastTradePriceOnly"]));
                }
                OnQuotesUpdate(quotes);
            }
        }

        private Task MakeAsyncHttpRequest(string url, string contentType, Action<WebResponse> responseHandler)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = contentType;
            request.Method = WebRequestMethods.Http.Get;
            request.Timeout = REQUEST_TIMEOUT;
            request.Proxy = null;

            Task<WebResponse> task = Task.Factory.FromAsync(
                request.BeginGetResponse,
                asyncResult => request.EndGetResponse(asyncResult),
                (object)null);

            return task.ContinueWith(t => responseHandler(t.Result));
        }

        protected override void GetQuotes()
        {
            string symbolString = "";
            foreach (string symbol in Symbols)
            {
                symbolString += "%22" + symbol + "%22" + "%2C";
            }
            symbolString = symbolString.Substring(0, symbolString.Length - 3); //Remove last comma
            MakeAsyncHttpRequest(REQUEST_URI.Replace(SYMBOLS_PLACEHOLDER, symbolString), CONTENT_TYPE, HandleResponse);
        }

    }
}
