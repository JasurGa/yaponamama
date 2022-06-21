using System;
namespace Atlas.Application.Common.Helpers
{
    public static class GeoDistance
    {
        public const int EARTH_RADIUS = 6372795;

        public static double GetDistance(double fA, double lA, double fB, double lB)
        {
            var lat1  = fA * Math.PI / 180;
            var lat2  = fB * Math.PI / 180;
            var long1 = lA * Math.PI / 180;
            var long2 = lB * Math.PI / 180;

            var cl1 = Math.Cos(lat1);
            var cl2 = Math.Cos(lat2);
            var sl1 = Math.Sin(lat1);
            var sl2 = Math.Sin(lat2);

            var delta  = long1 - long2;
            var cdelta = Math.Cos(delta);
            var sdelta = Math.Sin(delta);

            var y = Math.Sqrt(Math.Pow(cl2 * sdelta, 2) + Math.Pow(cl1 * sl2 - sl1 * cl2 * cdelta, 2));
            var x = sl1 * sl2 + cl1 * cl2 * cdelta;

            var ad   = Math.Atan2(y, x);
            var dist = ad * EARTH_RADIUS;

            return dist;
        }
    }
}
