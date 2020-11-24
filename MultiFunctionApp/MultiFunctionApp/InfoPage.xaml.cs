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
    public partial class InfoPage : ContentPage
    {
        public InfoPage()
        {
            InitializeComponent();
        }

        private async void button_Home_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Open API documentation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void button_APIdoc_Clicked(object sender, EventArgs e)
        {
            await Launcher.OpenAsync("https://openweathermap.org/");
        }
    }
}