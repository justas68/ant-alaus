using System;
using System.Collections.Generic;
using System.Text;

namespace Alus.Core
{
    public interface ILocationFinder
    {
        Location FindLocation(int tries = 3, Location defaultLocation = default(Location));
    }
}
