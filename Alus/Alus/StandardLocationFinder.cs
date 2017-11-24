using System;
using System.Device.Location;

using Alus.Core;

namespace Alus
{
    public class StandardLocationFinder : ILocationFinder
    {
        private GeoCoordinateWatcher _watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.Default);

        public Location FindLocation(int tries = 3, Location defaultLocation = default(Location))
        {
            for (int i = 0; i < tries; i++)
            {
                if (_watcher.TryStart(false, TimeSpan.FromSeconds(3)))
                {
                    if (_watcher.Position.Location.IsUnknown)
                    {
                        continue;
                    }
                    return new Location(
                        _watcher.Position.Location.Latitude,
                        _watcher.Position.Location.Longitude
                    );
                }
            }

            return defaultLocation;
        }
    }
}
