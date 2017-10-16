using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alus
{
    public static class ListExtensions
    {
        public static bool InRange<T>(this List<T> list, int x)
        {
            return (x >= 0 && x < list.Count());
        }
    }
}
