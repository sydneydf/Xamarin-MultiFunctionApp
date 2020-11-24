using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Essentials;

namespace MultiFunctionApp
{
    internal class APIHandler
    {
        //TODO: Fix APIHandler methods using parameter _dayChoice constantly
        //TODO: ASk Aaron how we handle Api Optionals that dont always get reported?
        //TODO: Add AlertHandler (Alerts Property of Json Return), NOTE: If Alert pops up, JsonConvert.Deserialize operation will crash, Because it cant cast to a base API object
        //Maybe ask Aaron about dynamic objects

        private string
            APIstring; // Location Syntax lowercase = {cityname,country code} &units=""  To Add a new API parameter DO: &<paramterName>=

        public string ResponseString { get; set; }
        public List<DayHandler> Days { get; set; } // OUR MAIN LIST WHICH IS OUR 8 DAY FORECAST INCLUDING CURRENT DAY, Only use 0-6 index (7 Days)
        public int CurrentDay { get; set; } = 0;

        public APIHandler()
        {
            //Grab location from preferencces and if none exists grab Perth Information
            string latlong = Preferences.Get("setting_Locality", "?lat=-31.93&lon=115.83"); 


            //Grab Measurementchoice from preferences with a default for metric system
            string UnitPreference; 
            bool _ImperialChoice = Preferences.Get("setting_Imperial", false);

            if (_ImperialChoice)
            {
                UnitPreference = "imperial";
            }
            else
            {
                UnitPreference = "metric";
            }
            string ApiKey = "69f2a2e9df97b7dee23d01e848e7f7b3"; //Currently registered API key from openweather.org

            //Full formatted string with above variables thats parsed to API connection
            APIstring =
                $"https://api.openweathermap.org/data/2.5/onecall{latlong}&units={UnitPreference}&exclude=current,minutely,hourly&appid={ApiKey}";
            //https://api.openweathermap.org/data/2.5/onecall?lat=-31.93&lon=115.83&units=metric&exclude=current,minutely,hourly&appid=69f2a2e9df97b7dee23d01e848e7f7b3



            ///ExampleQueryofApi
            ///{"lat":-31.93,"lon":115.83,"timezone":"Australia/Perth","timezone_offset":28800,"daily":[{"dt":1603857600,"sunrise":1603833766,"sunset":1603881476,"temp":{"day":20.48,"min":17.29,"max":22.06,"night":17.29,"eve":20.78,"morn":17.56},"feels_like":{"day":17.66,"night":14.83,"eve":15.81,"morn":16.45},"pressure":1019,"humidity":59,"dew_point":12.23,"wind_speed":5,"wind_deg":209,"weather":[{"id":803,"main":"Clouds","description":"broken clouds","icon":"04d"}],"clouds":78,"pop":0,"uvi":10.38},{"dt":1603944000,"sunrise":1603920109,"sunset":1603967926,"temp":{"day":24.27,"min":15.03,"max":27.46,"night":23.07,"eve":25.95,"morn":15.03},"feels_like":{"day":21.18,"night":17.62,"eve":22.15,"morn":10.43},"pressure":1020,"humidity":42,"dew_point":10.57,"wind_speed":4.69,"wind_deg":80,"weather":[{"id":800,"main":"Clear","description":"clear sky","icon":"01d"}],"clouds":0,"pop":0,"uvi":10.64},{"dt":1604030400,"sunrise":1604006453,"sunset":1604054377,"temp":{"day":28.63,"min":20.83,"max":30.95,"night":24.82,"eve":29.73,"morn":20.83},"feels_like":{"day":23.8,"night":22.84,"eve":28.18,"morn":14.33},"pressure":1014,"humidity":26,"dew_point":7.57,"wind_speed":5.97,"wind_deg":50,"weather":[{"id":802,"main":"Clouds","description":"scattered clouds","icon":"03d"}],"clouds":25,"pop":0,"uvi":10.21},{"dt":1604116800,"sunrise":1604092798,"sunset":1604140828,"temp":{"day":21.1,"min":16.49,"max":24.42,"night":16.49,"eve":18.61,"morn":22.12},"feels_like":{"day":17.9,"night":10.93,"eve":13.46,"morn":18.81},"pressure":1004,"humidity":67,"dew_point":14.83,"wind_speed":6.74,"wind_deg":257,"weather":[{"id":500,"main":"Rain","description":"light rain","icon":"10d"}],"clouds":72,"pop":0.89,"rain":1.36,"uvi":10.54},{"dt":1604203200,"sunrise":1604179145,"sunset":1604227279,"temp":{"day":17.64,"min":15.17,"max":18.04,"night":16.19,"eve":17.94,"morn":15.17},"feels_like":{"day":10.51,"night":12.66,"eve":12.96,"morn":9.24},"pressure":1008,"humidity":48,"dew_point":6.71,"wind_speed":9.03,"wind_deg":288,"weather":[{"id":501,"main":"Rain","description":"moderate rain","icon":"10d"}],"clouds":21,"pop":0.94,"rain":4.3,"uvi":9.28},{"dt":1604289600,"sunrise":1604265493,"sunset":1604313731,"temp":{"day":19.37,"min":13.56,"max":19.46,"night":15.56,"eve":18.33,"morn":13.56},"feels_like":{"day":16.26,"night":12.96,"eve":14.27,"morn":10.79},"pressure":1020,"humidity":52,"dew_point":9.34,"wind_speed":4.22,"wind_deg":214,"weather":[{"id":801,"main":"Clouds","description":"few clouds","icon":"02d"}],"clouds":17,"pop":0.24,"uvi":9.43},{"dt":1604376000,"sunrise":1604351842,"sunset":1604400183,"temp":{"day":24.68,"min":14.57,"max":26.11,"night":19.76,"eve":23.33,"morn":14.57},"feels_like":{"day":22.39,"night":17.52,"eve":19.36,"morn":12.36},"pressure":1023,"humidity":39,"dew_point":9.93,"wind_speed":3.25,"wind_deg":84,"weather":[{"id":800,"main":"Clear","description":"clear sky","icon":"01d"}],"clouds":2,"pop":0,"uvi":9.4},{"dt":1604462400,"sunrise":1604438192,"sunset":1604486635,"temp":{"day":30.66,"min":20.49,"max":32.77,"night":22.94,"eve":29.96,"morn":20.49},"feels_like":{"day":26.86,"night":21.25,"eve":26.97,"morn":14.81},"pressure":1018,"humidity":27,"dew_point":9.59,"wind_speed":5.3,"wind_deg":34,"weather":[{"id":802,"main":"Clouds","description":"scattered clouds","icon":"03d"}],"clouds":31,"pop":0,"uvi":9.69}]}
        }



        /// <summary>
        /// Initiates HTTP connection to grab weather JSON Data
        /// </summary>
        /// <returns>Stores List of dayhandler objects to a hoisted class variable</returns>
        public async Task connectToAPI()
        {
                var client = new HttpClient();
                var response = await client.GetAsync(APIstring);
                ResponseString = await response.Content.ReadAsStringAsync();
                BaseApi BaseApiObj = JsonConvert.DeserializeObject<BaseApi>(ResponseString);
                Days = BaseApiObj.daily.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Specific DayHandler object based on current day selected by user</returns>
        public DayHandler DayInfo()
        {
            return Days[CurrentDay];
        }


        /// <summary>
        /// Parses Selected DayHandler Object
        /// </summary>
        /// <returns>Returns ordered Actual Temp List</returns>
        public List<string> Actuals_SliderParser()
        {
            DayHandler currentDayObj = DayInfo();
            Temp _actualTemp = currentDayObj.temp;

            List<string> _iterableActuals = new List<string>();
            _iterableActuals.Add(Convert.ToInt32(_actualTemp.morn).ToString());
            _iterableActuals.Add(Convert.ToInt32(_actualTemp.day).ToString());
            _iterableActuals.Add(Convert.ToInt32(_actualTemp.eve).ToString());
            _iterableActuals.Add(Convert.ToInt32(_actualTemp.night).ToString());
            return _iterableActuals;
        }


        /// <summary>
        /// Parses Selected DayHandler Object
        /// </summary>
        /// <returns>Returns ordered Feels_Like Temp List</returns>
        public List<string> Feels_SliderParser()
        {
            DayHandler currentDayObj = DayInfo();
            FeelsLike _feelsLikeObj = currentDayObj.feels_like;

            List<string> _iterableFeelsLike = new List<string>();
            _iterableFeelsLike.Add(Convert.ToInt32(_feelsLikeObj.morn).ToString());
            _iterableFeelsLike.Add(Convert.ToInt32(_feelsLikeObj.day).ToString());
            _iterableFeelsLike.Add(Convert.ToInt32(_feelsLikeObj.eve).ToString());
            _iterableFeelsLike.Add(Convert.ToInt32(_feelsLikeObj.night).ToString());
            return _iterableFeelsLike;
        }

        /// <summary>
        /// Grab parsed _sunTime, converts to unix seconds from epoch, epoch is time in seconds from 1/1/1970
        /// convert epochtime to localtime and convert that to a custom string
        /// </summary>
        /// <param name="_sunTime"></param>
        /// <returns>string of localtimestamp</returns>
        public string sunTimesParser(int _sunTime)
        {
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(_sunTime); //Unix time in seconds (FROM API) to UTC
            string localTime = dateTimeOffset.LocalDateTime.ToString("HH:mm");
            return localTime;
        }

        /// <summary>
        /// Use sunTimesParser methods to grab appropriate times from our DayHandler object
        /// </summary>
        /// <returns>List of 2 datetime strings</returns>
        public List<string> createTimes()
        {
            List<string> timesList = new List<string>();
            DayHandler currentDayObj = DayInfo();
            timesList.Add($"{sunTimesParser(currentDayObj.sunrise)}A.M.");
            timesList.Add($"{sunTimesParser(currentDayObj.sunset)}P.M.");

            return timesList;
        }

        /// <summary>
        /// Grab weatherinfo from DayHandler
        /// </summary>
        /// <returns>return said weatherinfo</returns>
        public Weather WeatherInfo()
        {

            DayHandler currentDayObj = Days[CurrentDay];
            List<Weather> _actualWeather = currentDayObj.weather.ToList();

            Weather DayWeather = _actualWeather[0];
            return DayWeather;

        }

        /// <summary>
        /// Grab weather icon from weatherinfo method
        /// </summary>
        /// <returns>web address of needed icon</returns>
        public string returnIcon()
        {
            Weather WeatherInvoke = WeatherInfo();
            string icon = WeatherInvoke.icon;
            string iconURL = $"https://openweathermap.org/img/wn/{icon}@2x.png";
            return iconURL;
        }
    }
}
