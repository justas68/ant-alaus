using System;

namespace Alus
{
    class Bar
    {
        public Bar()
        {
        }

        public Bar(string name, string coordinates, double rating, string address, GoogleApi.OpeningHours hour)
        {
            Name = name;
            Coordinates = coordinates;
            Rating = rating;
            Address = address;
            Hour = hour;
        }

        public string Name { get; private set; }
        public string Coordinates { get; private set; }
        public double Rating { get; private set; }
        public string Address { get; private set; }
        public GoogleApi.OpeningHours Hour { get; private set; }
    }
}
