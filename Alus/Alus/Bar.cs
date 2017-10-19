using System;

namespace Alus
{
    public class Bar
    {
        public Bar()
        {
        }

        public Bar(string name, string coordinates, double onlineRating, string address, string placeId, string evaluation, double percentage, int beersBought)
        {
            Name = name;
            Coordinates = coordinates;
            OnlineRating = onlineRating;
            Address = address;
            PlaceId = placeId;
            Evaluation = evaluation;
            Percentage = percentage;
            BeersBought = beersBought;
        }

        public string Name { get; private set; }
        public string Coordinates { get; private set; }
        public double OnlineRating { get; private set; }
        public string Address { get; private set; }
        public string PlaceId { get; private set; }
        public string Evaluation { get; set; }
        public double Percentage { get; set; }
        public int BeersBought { get; set; }
    }
}
