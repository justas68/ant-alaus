using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alus
{
    static class ListExtensions
    {
        public static bool InRange<T>(this List<T> list, int x)
        {
            return Enumerable.Range(0, list.Count()).Contains(x);
        }
    }
}
