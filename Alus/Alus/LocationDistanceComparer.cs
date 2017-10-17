using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alus
{
    public class LocationDistanceComparer : IComparer<Location>
    {
        public Location Location { get; private set; }

        public LocationDistanceComparer(Location location)
        {
            Location = location;
        }

        public int Compare(Location x, Location y)
        {
            return x.DistanceTo(Location).CompareTo(y.DistanceTo(Location));
        }
    }
}
