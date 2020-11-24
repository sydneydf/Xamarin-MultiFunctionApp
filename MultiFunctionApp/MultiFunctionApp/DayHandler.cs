using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MultiFunctionApp
{
    //Our Main object we are looking that we reference and pass. 8? of these exist from the API return
    internal class DayHandler
    {
        [JsonProperty("dt")]
        public int dt { get; set; }

        [JsonProperty("sunrise")]
        public int sunrise { get; set; }

        [JsonProperty("sunset")]
        public int sunset { get; set; }

        //Cast to type of temp
        [JsonProperty("temp")]
        public Temp temp { get; set; }

        //Cast to type of feels_like
        [JsonProperty("feels_like")]
        public FeelsLike feels_like { get; set; }

        [JsonProperty("pressure")]
        public int pressure { get; set; }

        [JsonProperty("humidity")]
        public int humidity { get; set; }

        [JsonProperty("dew_point")]
        public double dew_point { get; set; }

        [JsonProperty("wind_speed")]
        public double wind_speed { get; set; }

        [JsonProperty("wind_deg")]
        public int wind_deg { get; set; }

        //Cast to an IList of type weather
        [JsonProperty("weather")]
        public IList<Weather> weather { get; set; }

        [JsonProperty("clouds")]
        public int clouds { get; set; }

        [JsonProperty("pop")]
        public double pop { get; set; }

        [JsonProperty("uvi")]
        public double uvi { get; set; }

        //optional parameter TODO: use in future
        [JsonProperty("rain")]
        public double? rain { get; set; }
    }
}