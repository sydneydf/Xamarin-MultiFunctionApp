using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace MultiFunctionApp
{
    public partial class App : Application
    {
        public App()
        {
            //To use experimental AppThem feature to easily have Light and Dark Theme modes for our app
            Device.SetFlags(new string[] { "AppTheme_Experimental" });

            InitializeComponent();

            MainPage = new MainPage();
            //Calling Theme method
            themeSetup();

            ///TODO: Get this method working
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => {
                SoundEffects.PlayTap();
            };

            //Element.GestureRecognizers.Add(tapGestureRecognizer);
        }

        /// <summary>
        /// This method takes in user preference and sets our App.Xaml elements to either have a light or dark mode theme
        /// </summary>
        public void themeSetup()
        {
            bool dark = Preferences.Get("setting_DarkMode", false);
            if (dark)
            {
                App.Current.UserAppTheme = OSAppTheme.Dark;
            }
            else
            {
                App.Current.UserAppTheme = OSAppTheme.Light;
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}