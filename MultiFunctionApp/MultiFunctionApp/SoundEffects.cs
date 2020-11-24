using System;
using System.Collections.Generic;
using System.Text;

namespace MultiFunctionApp
{
    /// <summary>
    /// SoundEffects class made from guide on MobileApps Blackboard
    /// TODO: Still need to get gesture recognizer to play taps on button presses
    /// </summary>
    class SoundEffects
    {
        static Plugin.SimpleAudioPlayer.ISimpleAudioPlayer tap = null;
        public static void PlayTap()
        {
            //-- Just in time initialisation

            if (tap == null)
            {
                tap = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
                tap.Load("tap.wav");
            }

            // - Play Sound by SoundEffects.PlayTap()

            tap.Play();
        }
    }
}
