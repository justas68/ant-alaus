using Alus;
using Alus.Core;

namespace BeerApplication
{
    class AndroidLocationFinder : ILocationFinder
    {
        public Location FindLocation(int tries = 3, Location defaultLocation = default(Location))
        {
            if (NearestBars.currentLocation != null)
            {
                return new Alus.Location(NearestBars.currentLocation.Latitude, NearestBars.currentLocation.Longitude);
            }
            else
            {
                return new Location(54.73157, 25.26187);
            }
            
        }
    }
}