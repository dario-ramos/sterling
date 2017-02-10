using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sterling
{
    public enum Side
    {
        Buy,
        Sell
    }

    public class Strategy
    {
        public Strategy(Side side, string name, string symbol)
        {
            Side = side;
            Name = name;
            Symbol = symbol;
        }

        public Side Side { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
    }

}
