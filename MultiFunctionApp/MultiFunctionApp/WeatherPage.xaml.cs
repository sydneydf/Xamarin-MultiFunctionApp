using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace MultiFunctionApp
{
    /// <summary>
    /// Handle most of our API returns
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeatherPage : ContentPage
    {
        //TODO: Change notes section to display alerts

        /// <summary>
        /// Instantiate APIobj to handle JSOn returns
        /// </summary>
        private APIHandler APIobj = new APIHandler();

        DateTime CurrentDateTime;//hoisted variable to store DateTime type

        int InitialDayInt; //Store the initial day
        int EnumDayInt; //Keep track of DayNumber in reference to a 7 day week for reference with DateTime.DayOfWeek Enum

        private int sliderVal = 0;

        public WeatherPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// App OnAppearing method is overrwritten, So when Weatherpage instance is loaded we initiate an API connection
        /// We grab currenttime 
        /// And rezero DayButton labels
        /// </summary>
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                await APIobj.connectToAPI();
                CurrentDateTime = DateTime.Now;
                reZeroDayLabels();
                updateUI(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                await DisplayAlert("No Internet", $"Please reconnect and then pull down to refresh", "Reconnect!");
            }
        }

        /// <summary>
        /// Push Mainpage instance again
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btn_HomePage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Async method to initiate App json connection from APIobj method
        /// </summary>
        //private async void InitializeConnection()
        //{
        //    await APIobj.connectToAPI();
        //}


        /// <summary>
        /// SUPER UI method to update every needed weather display item and call APIobj methods to retrieve data for display
        /// 
        /// TODO: Fix Icon image updating
        /// NOTE: Alot of these string format returns should be moved to APIobj class
        /// </summary>
        private void updateUI(bool iconUpdate)
        {

            string measurementChoice;
            bool imperialChoice = Preferences.Get("setting_Imperial", false);


            if (imperialChoice == false)
            {
                measurementChoice = "C";
            }
            else
            {
                measurementChoice = "F";
            }


            //TODO: Many of these display string will need to be formatted or parsed differently for the user

            List<string> sliderList_Actual = APIobj.Actuals_SliderParser();
            List<string> sliderList_Feels = APIobj.Feels_SliderParser();

            List<string> sunTimes = APIobj.createTimes();
            Weather weatherInfo = APIobj.WeatherInfo();

            DayHandler CurrentDayObject = APIobj.DayInfo();

            if (iconUpdate == true)
            {
                weatherIcon.Source = APIobj.returnIcon();
            }

            display_rainChance.Text = $"{CurrentDayObject.pop}%";

            display_tempMax.Text = $"{Convert.ToInt32(CurrentDayObject.temp.max)}\x00B0{measurementChoice}"; //Remember temperatures change to implement preference choice
            display_tempMin.Text = $"{Convert.ToInt32(CurrentDayObject.temp.min)}\x00B0{measurementChoice}"; //
            display_ActualTemp.Text = $"{sliderList_Actual[sliderVal]}\x00B0{measurementChoice}"; //as noted above
            display_FeelsLike.Text = $"{sliderList_Feels[sliderVal]}\x00B0{measurementChoice}"; //

            display_WeatherMain.Text = $"{weatherInfo.main},";
            display_WeatherDescript.Text = weatherInfo.description;
            
            display_WindSpeed.Text = CurrentDayObject.wind_speed.ToString();
            display_WindDirection.Text = CurrentDayObject.wind_deg.ToString();
            display_Humidity.Text = CurrentDayObject.humidity.ToString();
            display_Pressure.Text = CurrentDayObject.pressure.ToString();

            display_Sunrise.Text = sunTimes[0];
            display_Sunset.Text = sunTimes[1];

            string currentText;
            if (EnumDayInt == InitialDayInt)
            {
                currentText = "Currently\n";
            }
            else
            {
                currentText = "";
            }
            selected_day.Text = $"{currentText}{fetchDay("")}";
            btn_minusDay.Text = fetchDay("minus").Substring(0, 3);
            btn_plusDay.Text = fetchDay("plus").Substring(0, 3);
        }

        /// <summary>
        /// reset Date ints for reference to reset Enum reference 
        /// </summary>
        private void reZeroDayLabels()
        {
            int _tempNum = (int)CurrentDateTime.DayOfWeek;
            EnumDayInt = _tempNum;
            InitialDayInt = _tempNum;
        }

        /// <summary>
        /// We have only 7 days in a week
        /// This method limits counters so that they only iterate over min and max values of Day Ints.
        /// Because we dont want to refrence an 8th day when that dosent exist
        /// </summary>
        /// <param name="value"></param>
        /// <returns>legal day int references</returns>
        public static int limitToRange(int value)
        {
            int min = 0;
            int max = 6;
            if (value < min) { return max; }
            if (value > max) { return min; }
            return value;
        }

        /// <summary>
        /// fetch the needed day for a button by passing in what button it is
        /// </summary>
        /// <param name="_command"></param>
        /// <returns>string of type day for button display</returns>
        private string fetchDay(string _command)
        {
            int IntRefEnumDay = EnumDayInt;
            if (_command == "minus")
            {
                IntRefEnumDay--;
                IntRefEnumDay = limitToRange(IntRefEnumDay);
            }
            else if (_command == "plus")
            {
                IntRefEnumDay++;
                IntRefEnumDay = limitToRange(IntRefEnumDay);
            }
            else
            {
                IntRefEnumDay = EnumDayInt;
            }
            DayOfWeek CurrentEnum = (DayOfWeek)IntRefEnumDay;
            return CurrentEnum.ToString();
        }

        /// <summary>
        /// Increase APIobj.CurrentDay and DayOfWeek reference to change what information of APIobj is displayed
        /// </summary>
        private void incrementUp()
        {
            APIobj.CurrentDay++;
            APIobj.CurrentDay = limitToRange(APIobj.CurrentDay);


            EnumDayInt++;
            EnumDayInt = limitToRange(EnumDayInt);
            sliderVal = 0;
        }

        /// <summary>
        /// Decrease APIobj.CurrentDay and DayOfWeek reference to change what information of APIobj is displayed
        /// </summary>
        private void incrementDown()
        {
            APIobj.CurrentDay--;
            APIobj.CurrentDay = limitToRange(APIobj.CurrentDay);

            EnumDayInt--;
            EnumDayInt = limitToRange(EnumDayInt);
            sliderVal = 0;
        }

        /// <summary>
        /// detect sliderbox changes and update information referenced accordingly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Slider_SliderBox_OnValueChanged(object sender, ValueChangedEventArgs e)
        {
            sliderVal = (int)slider_SliderBox.Value;
            updateUI(false);
        }


        /// <summary>
        /// call methods to decrease reference point and updateUI accordingly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_minusDay_OnClicked(object sender, EventArgs e)
        {
            incrementDown();
            updateUI(true);
        }

        /// <summary>
        /// call methods to increase reference point and updateUI accordingly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_plusDay_OnClicked(object sender, EventArgs e)
        {
            incrementUp();
            updateUI(true);
        }

        /// <summary>
        /// method to detect refreshview refreshchange, If a user pulls down to refresh,recall api connection method and update from zero refrence accordingly and display
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void refreshPage_Refreshing(object sender, EventArgs e)
        {

            try
            {
                await APIobj.connectToAPI();
                APIobj.CurrentDay = 0;
                CurrentDateTime = DateTime.Now;
                reZeroDayLabels();
                updateUI(true);
                refreshPage.IsRefreshing = false;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                await DisplayAlert("No Internet", $"Please reconnect and then pull down to refresh", "Reconnect!");
                refreshPage.IsRefreshing = false;
            }
        }
    }
}