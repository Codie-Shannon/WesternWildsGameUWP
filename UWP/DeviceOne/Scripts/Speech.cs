using System;
using System.Threading.Tasks;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DeviceOne
{
    public class Speech
    {
        // Variables
        // =============================================================
        // =============================================================
        private static MediaElement meSpeak;



        // Setup
        // =============================================================
        // =============================================================
        public static void Setup()
        {
            //Get Main Page
            MainPage MainPage = (MainPage)((Frame)Window.Current.Content).Content;

            //Get Main Page's Media Element
            meSpeak = MainPage.meSpeak;
        }



        #region Methods
        // Get Element
        // =============================================================
        // =============================================================
        public static MediaElement GetElement() { return meSpeak; }


        // Start Text To Speech
        // =============================================================
        // =============================================================
        public static async Task<bool> StartAsync(string text)
        {
            //Check if text contains a value
            if (!string.IsNullOrEmpty(text))
            {
                //Open Speech Synthesizer Object
                using (var speech = new SpeechSynthesizer())
                {
                    //Set Voice
                    speech.Voice = SpeechSynthesizer.AllVoices[2];

                    //Open Speech Synthesis Stream
                    SpeechSynthesisStream stream = await speech.SynthesizeTextToStreamAsync(text);

                    //Set Media Element Source to Speech Synthesis Stream
                    meSpeak.SetSource(stream, stream.ContentType);
                }

                //Return True
                return true;
            }

            //Return False
            return false;
        }


        // Toggle Text To Speech
        // =============================================================
        // =============================================================
        // Play
        public static void Play() { meSpeak.Play(); }

        // Pause
        public static void Pause() { meSpeak.Pause(); }
        #endregion Methods
    }
}
