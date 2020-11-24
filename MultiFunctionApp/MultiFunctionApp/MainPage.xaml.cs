using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MultiFunctionApp
{
    public partial class MainPage : ContentPage
    {
        /// <summary>
        /// MainPage load
        /// </summary>

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_NavWeatherPage_OnClicked(object sender, EventArgs e)
        {
            //Attempt to instantiate Weatherscreen instance
            try
            {
                WeatherPage WeatherScreen = new WeatherPage();
                await Navigation.PushModalAsync(WeatherScreen);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private async void Button_ToBeAdded_OnClicked(object sender, EventArgs e)
        {
            //Connected to pages that are not made yet
            await DisplayAlert("Additional Feature",
                "Unfortunately this feature is not added yet. Will be added in the near future", "Ok!");
        }

        private async void button_Settings_Clicked(object sender, EventArgs e)
        {
            //Push an instance of the Settings page
            try
            {
                Settings SettingsScreen = new Settings();
                await Navigation.PushModalAsync(SettingsScreen);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private async void button_AppInfo_Clicked(object sender, EventArgs e)
        {
            try
            {
                InfoPage InfoScreen = new InfoPage();
                await Navigation.PushModalAsync(InfoScreen);
            }
            catch (Exception exception)
            {

                Console.WriteLine(exception.Message);
            }
        }
    }
}