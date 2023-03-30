using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BisimulationRelationObtainer.Models
{
    public class Pair<U> where U : notnull
    {
        public U Left { get; }
        public U Right { get; }
        public Pair(U left, U right)
        {
            Left = left;
            Right = right;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Left, Right);
        }

        public override bool Equals(object? obj)
        {
            if (obj is Pair<U> pair)
            {
                return pair.Left.Equals(Left) && pair.Right.Equals(Right);
            }
            return false;
        }

        public override string? ToString()
        {
            return $"[{Left}] [{Right}]";
        }
    }
}
