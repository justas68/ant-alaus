using System;

namespace Alus
{
    class Bar
    {
        public Bar()
        {
        }

        public Bar(string name, string coordinates, double onlineRating, string address, string placeId, string evaluation)
        {
            Name = name;
            Coordinates = coordinates;
            OnlineRating = onlineRating;
            Address = address;
            PlaceId = placeId;
            Evaluation = evaluation;
        }

        public string Name { get; private set; }
        public string Coordinates { get; private set; }
        public double OnlineRating { get; private set; }
        public string Address { get; private set; }
        public string PlaceId { get; private set; }
        public string Evaluation { get; set; }
    }
}
