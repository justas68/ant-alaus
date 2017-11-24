using System;
using Newtonsoft.Json;

namespace Alus.Core.Models
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

        public Bar(DatabaseBar bar)
            : this(bar.Name, bar.Coordinates, bar.OnlineRating, bar.Address, bar.PlaceId, bar.Evaluation, bar.Percentage, bar.BeersBought)
        {
        }

        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("coordinates")]
        public string Coordinates { get; set; }
        [JsonProperty("onlineRating")]
        public double OnlineRating { get; set; }
        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonProperty("placeId")]
        public string PlaceId { get; set; }
        [JsonProperty("evaluation")]
        public string Evaluation { get; set; }
        [JsonProperty("percentage")]
        public double Percentage { get; set; }
        [JsonProperty("beersBought")]
        public int BeersBought { get; set; }
    }

    public class DatabaseBar : Bar
    {
        public DatabaseBar()
        {
        }

        public DatabaseBar(Bar bar)
            : base(bar.Name, bar.Coordinates, bar.OnlineRating, bar.Address, bar.PlaceId, bar.Evaluation, bar.Percentage, bar.BeersBought)
        {
        }

        public long Id { get; set; }
        public DateTime Created { get; set; }
    }
}
