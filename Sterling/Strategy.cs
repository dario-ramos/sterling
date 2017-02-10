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

        public bool Equals(Strategy value)
        {
            if (ReferenceEquals(null, value))
            {
                return false;
            }
            if (ReferenceEquals(this, value))
            {
                return true;
            }
            return IsEqual(value);
        }

        public override bool Equals(object value)
        {
            if(ReferenceEquals(null, value))
            {
                return false;
            }
            if (ReferenceEquals(this, value))
            {
                return true;
            }
            if(GetType() != value.GetType())
            {
                return false;
            }
            Strategy strat = value as Strategy;
            return IsEqual(strat);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                // Choose large primes to avoid hashing collisions
                const int HashingBase = (int)2166136261;
                const int HashingMultiplier = 16777619;

                int hash = HashingBase;
                hash = (hash * HashingMultiplier) ^ (!ReferenceEquals(null, Name) ? Name.GetHashCode() : 0);
                hash = (hash * HashingMultiplier) ^ (!ReferenceEquals(null, Side) ? Side.GetHashCode() : 0);
                hash = (hash * HashingMultiplier) ^ (!ReferenceEquals(null, Symbol) ? Symbol.GetHashCode() : 0);
                return hash;
            }
        }

        public static bool operator ==(Strategy stratA, Strategy stratB)
        {
            if (ReferenceEquals(stratA, stratB))
            {
                return true;
            }
            if (ReferenceEquals(null, stratA))
            {
                return false;
            }
            return (stratA.Equals(stratB));
        }

        public static bool operator !=(Strategy stratA, Strategy stratB)
        {
            return !(stratA == stratB);
        }

        private bool IsEqual(Strategy value)
        {
            return (Side == value.Side) &&
                   string.Equals(Name, value.Name) &&
                   string.Equals(Symbol, value.Symbol);
        }
    }

}
