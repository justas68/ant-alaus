using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alus
{
    public static class LocationExtensions
    {
        public static double DistanceTo(this Location baseLocation, Location targetLocation, UnitOfLength unitOfLength = null)
        {
            if (unitOfLength == null)
            {
                unitOfLength = UnitOfLength.Kilometers;
            }

            var baseRad = Math.PI * baseLocation.Latitude / 180;
            var targetRad = Math.PI * targetLocation.Latitude / 180;
            var theta = baseLocation.Longtitude - targetLocation.Longtitude;
            var thetaRad = Math.PI * theta / 180;

            double dist =
                Math.Sin(baseRad) * Math.Sin(targetRad) + Math.Cos(baseRad) *
                Math.Cos(targetRad) * Math.Cos(thetaRad);
            dist = Math.Acos(dist);

            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;

            return unitOfLength.ConvertFromMiles(dist);
        }
    }
}
