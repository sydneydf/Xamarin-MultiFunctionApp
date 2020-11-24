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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {

        //Very important dictionary sourced from Student Nathan Chaimongkhan!!!
        public Dictionary<string, string> locationDict = new Dictionary<string, string>()
        {
            {"Perth - Australia","?lat=-31.9505&lon=115.8605"},{"Adelaide - Australia","?lat=-34.9285&lon=138.6007"},{"Algiers - Algeria","?lat=36.73225&lon=3.08746"},{"Amsterdam - NL","?lat=52.3676&lon=4.9041"},{"Ankara - Turkey","?lat=39.9334&lon=32.8597"},
            {"Athens - Greece","?lat=37.9838&lon=23.7275"},{"Auckland - NZ","?lat=-36.8483&lon=174.7625"},{"Bangkok - Thailand","?lat=13.7563&lon=100.5018"},{"Barcelona - Spain","?lat=41.3851&lon=2.1734"},{"Beijing - China","?lat=39.9042&lon=116.4074"},
            {"Belém - Brazil","?lat=1.4557&lon=48.4902"},{"Belfast - NIR","?lat=54.5973&lon=5.9301"},{"Belgrade - Serbia","?lat=44.7866&lon=20.4489"},{"Berlin - Germany","?lat=52.5200&lon=13.4050"},{"Birmingham - England","?lat=52.4862&lon=1.8904"},
            {"Bogotá - Colombia","?lat=4.7110lon=74.0721"},{"Bombay - India","?lat=19.0760&lon=72.8777"},{"Bordeaux - France","?lat=44.8378&lon=0.5792"},{"Bremen - Germany","?lat=53.0793&lon=8.8017"},{"Brisbane - Australia","?lat=27.4698&lon=153.0251"},
            {"Bristol - England","?lat=51.4545&lon=2.5879"},{"Brussels - Belgium","?lat=50.8503&lon=4.3517"},{"Bucharest - Romania","?lat=44.4268&lon=26.1025"},{"Budapest - Hungary","?lat=47.4979&lon=19.0402"},{"Buenos Aires - AR","?lat=34.6037&lon=58.3816"},
            {"Cairo - Egypt","?lat=30.0444&lon=31.2357"},{"Calcutta - India","?lat=22.5726&lon=88.3639"},{"Canton - China","?lat=23.1291&lon=113.2644"},{"Cape Town - ZA","?lat=-33.9249&lon=18.4241"},{"Caracas - Venezuela","?lat=10.4806&lon=66.9036"},

            {"Edinburgh - Scotland","?lat=55.9533&lon=3.1883"},{"Frankfurt - Germany","?lat=50.1109&lon=8.6821"},{"Georgetown - Guyana","?lat=6.8013&lon=58.1551"},{"Glasgow - Scotland","?lat=55.8642&lon=4.2518"},{"Guatemala City - GT","?lat=14.6349&lon=90.5069"},
            {"Guayaquil - Ecuador","?lat=2.1894&lon=79.8891"},{"Hamburg - Germany","?lat=53.5511&lon=9.9937"},{"Hammerfest - Norway","?lat=70.5308&lon=23.5717"},{"Havana - Cuba","?lat=23.1136&lon=82.3666"},{"Helsinki - Finland","?lat=60.1699&lon=24.9384"},
            {"Hobart - Tasmania","?lat=42.8821&lon=147.3272"},{"Hong Kong - China","?lat=22.3193&lon=114.1694"},{"Iquique - Chile","?lat=20.2307&lon=70.1357"},{"Irkutsk - Russia","?lat=52.2855&lon=104.2890"},{"Jakarta - Indonesia","?lat=6.2088&lon=106.8456"},
            {"Johannesburg - ZA","?lat=26.2041&lon=28.0473"},{"Kingston - Jamaica","?lat=18.0179&lon=76.8099"},{"Kinshasa - Congo","?lat=4.4419&lon=15.2663"},{"Kuala Lumpur - Malaysia","?lat=3.1390&lon=101.6869"},{"La Paz - Bolivia","?lat=-16.4897&lon=-68.1193"},
            {"Leeds - England","?lat=53.8008&lon=1.5491"},{"Lima - Peru","?lat=12.0464&lon=77.0428"},{"Lisbon - Portugal","?lat=38.7223&lon=9.1393"},{"Liverpool - England","?lat=53.4084&lon=2.9916"},{"London - England","?lat=51.5074&lon=0.1278"},
            {"Lyons - France","?lat=45.7640&lon=4.8357"},{"Madrid - Spain","?lat=40.4168&lon=3.7038"},{"Manchester - England","?lat=53.4808&lon=2.2426"},{"Manila - Philippines","?lat=14.5995&lon=120.9842"},{"Marseilles - France","?lat=43.2965&lon=5.3698"},

            {"Mazatlán - Mexico","?lat=23.2494&lon=106.4111"},{"Mecca - Saudi Arabia","?lat=21.3891&lon=39.8579"},{"Melbourne - Australia","?lat=-37.8136&lon=144.9631"},{"Mexico City - Mexico","?lat=-19.4326&lon=99.1332"},{"Milan - Italy","?lat=45.4642&lon=9.1900"},
            {"Montevideo - Uruguay","?lat=34.9011&lon=56.1645"},{"Moscow - Russia","?lat=55.7558&lon=37.6173"},{"Munich - Germany","?lat=48.1351&lon=11.5820"},{"Nagasaki - Japan","?lat=32.7503&lon=129.8779"},{"Nagoya - Japan","?lat=35.1815&lon=136.9066"},
            {"Nairobi - Kenya","?lat=1.2921&lon=36.8219"},{"Nanjing - China","?lat=32.0603&lon=118.7969"},{"Naples - Italy","?lat=40.8518&lon=14.2681"},{"New Delhi - India","?lat=28.6139&lon=77.2090"},{"NCLE - England","?lat=54.9783&lon=1.6178"},
            {"Odessa - Ukraine","?lat=46.4825&lon=30.7233"},{"Osaka - Japan","?lat=34.6937&lon=135.5023"},{"Oslo - Norway","?lat=59.9139&lon=10.7522"},{"Panama City - Panama","?lat=8.9824&lon=79.5199"},{"Paramaribo - Suriname","?lat=5.8520&lon=55.2038"},
            {"Paris - France","?lat=48.8566&lon=2.3522"},{"Aberdeen - Scotland","?lat=57.1497&lon=2.0943"},{"Plymouth - England","?lat=50.3755&lon=4.1427"},{"Port Moresby - PNG","?lat=9.4438&lon=147.1803"},{"Prague - Czech Republic","?lat=50.0755&lon=14.4378"},
            {"Rangoon - Myanmar","?lat=16.8409&lon=96.1735"},{"Reykjavík - Iceland","?lat=64.1466&lon=-21.9426"},{"Rio de Janeiro - Brazil","?lat=22.9068&lon=43.1729"},{"Rome - Italy","?lat=41.9028&lon=12.4964"},{"Salvador - Brazil","?lat=12.9777&lon=38.5016"},

            {"Santiago - Chile","?lat=33.4489&lon=70.6693"},{"St. Petersburg - Russia","?lat=59.9311&lon=30.3609"},{"São Paulo - Brazil","?lat=23.55051&lon=46.6333"},{"Shanghai - China","?lat=31.2304&lon=121.4737"},{"Singapore - Singapore","?lat=1.3521&lon=103.8198"},
            {"Sofia - Bulgaria","?lat=42.6977&lon=23.3219"},{"Stockholm - Sweden","?lat=59.3293&lon=18.0686"},{"Sydney - Australia","?lat=-33.8688&lon=151.2093"},{"Tananarive - Madagascar","?lat=18.8792&lon=47.5079"},{"Teheran - Iran","?lat=35.6892&lon=51.3890"},
            {"Tokyo - Japan","?lat=35.6762&lon=139.6503"},{"Tripoli - Libya","?lat=32.8872&lon=13.1913"},{"Venice - Italy","?lat=45.4408&lon=12.3155"},{"Veracruz - Mexico","?lat=19.1738&lon=-96.1342"},{"Vienna - Austria","?lat=48.2082&lon=16.3738"},
            {"Vladivostok - Russia","?lat=43.1332&lon=131.9113"},{"Warsaw - Poland","?lat=52.2297&lon=21.0122"},{"Wellington - NZ","?lat=-41.2769&lon=174.7731"},{"Zürich - Switzerland","?lat=47.3769&lon=8.5417"},{"New York - America", "?lat=40.7128&lon=74.0060"},
            {"Cayenne - French Guiana","?lat=4.9224&lon=52.3135"},{"Chihuahua - Mexico","?lat=28.6330&lon=-106.0691"},{"Chongqing - China","?lat=29.4316&lon=106.9123"},{"Copenhagen - Denmark","?lat=55.6761&lon=12.5683"},{"Córdoba - AR","?lat=31.4201&lon=64.1888"},
            {"Dakar - Senegal","?lat=14.7167&lon=17.4677"},{"Darwin - Australia","?lat=-12.4634&lon=130.8456"},{"Djibouti - Djibouti","?lat=11.8251&lon=42.5903"},{"Dublin - Ireland","?lat=53.3498&lon=-6.2603"},{"Durban - South Africa","?lat=-29.8587&lon=31.0218"}
        };
        public string black = "#FFFFFF";
        public string white = "#000000";
        public Settings()
        {
            InitializeComponent();
            loadToggleStates();
            loadLocalityPicker();
        }

        private void loadToggleStates()
        {
            //add toggles as they are made

            switch_Dark.IsToggled = Preferences.Get("setting_DarkMode", false);
            switch_Imperial.IsToggled = Preferences.Get("setting_Imperial", false);
            picker_Locality.Title = Preferences.Get("setting_CurrentPickerItem", "Perth - Australia");
        }

        private void loadLocalityPicker()
        {
            foreach (var item in locationDict)
            {
                picker_Locality.Items.Add(item.Key);
            }
        }

        private void chooseLocality(string localityName)
        {
            string latlong;

            foreach (var item in locationDict)
            {
                if (item.Key == localityName)
                {
                    latlong = item.Value;
                    setPref("setting_Locality", latlong);
                }
            }
        }

        private async void btn_HomePage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private bool checkForPref(string _key)
        {
            bool hasKey = Preferences.ContainsKey(_key);
            return hasKey;
        }


        private void setPref(string _settingKey, dynamic _settingValue)
        {
            Preferences.Set(_settingKey, _settingValue);
        }


        //TODO: Implement settings preferencees / additions here
        private void switch_Dark_Toggled(object sender, ToggledEventArgs e)
        {
            if (switch_Dark.IsToggled == true)
            {
                setPref("setting_DarkMode", true);
                App.Current.UserAppTheme = OSAppTheme.Dark;
                
            }
            else
            {
                setPref("setting_DarkMode", false);
                App.Current.UserAppTheme = OSAppTheme.Light;
            }
        }

        private  void switch_Imperial_Toggled(object sender, ToggledEventArgs e)
        {
            if (switch_Imperial.IsToggled == true)
            {
                setPref("setting_Imperial", true);
            }
            else
            {
                setPref("setting_Imperial", false);
            }
        }

        private void picker_Locality_SelectedIndexChanged(object sender, EventArgs e)
        {
            string pickerItem = picker_Locality.Items[picker_Locality.SelectedIndex];

            chooseLocality(pickerItem);
            setPref("setting_CurrentPickerItem", pickerItem);
        }
    }
}