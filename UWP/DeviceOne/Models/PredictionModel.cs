using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DeviceOne.Models
{
    public class PredictionModel
    {
        #region Variables
        // Arrays
        // ================================================================
        // ================================================================
        private static string[] timeArray = new string[] { "thirty minutes", "an hour", "eight hours", "twelve hours", "a day", "a week", "a month", "a year", "a decade" };
        private static string[] aspectArray = new string[] { "finances", "love life", "career prospects", "travel plans", "relationships" };
        private static string[] effectArray = new string[] { "fall apart", "exceed your expectation", "become awkward in an unexpected way", "become manageable", "become spectacular", "come to a positive outcome" };
        private static string[] personaArray = new string[] { "cowboy", "cowgirl", "rancher", "bloke", "buffalo", "cow", "bull", "singer", "dead man" };
        private static string[] featureArray = new string[] { "golden streaked hair", "a broken golden chain", "dead eyes", "a very red neck", "silver feet", "silver hands" };
        private static string[] consequenceArray = new string[] { "avoid looking at directly", "sing a sad song with", "stop and talk to", "dance with", "tell a secret", "buy a bottle of rum" };


        // Elements
        // ================================================================
        // ================================================================
        private static Button btnSpeak;
        private static TextBlock tbPrediction;


        // Other
        // ================================================================
        // ================================================================
        private static string str_Prediction;
        private static bool isPlaying = false;
        private static Random random = new Random();
        #endregion Variables



        #region Text To Speech Icons
        // Type
        // ================================================================
        // ================================================================
        public enum TextToSpeechIconTypes { Playing, Stopped }


        // Dictionary
        // ================================================================
        // ================================================================
        private static Dictionary<TextToSpeechIconTypes, string> TextToSpeechIcons = new Dictionary<TextToSpeechIconTypes, string>() { { TextToSpeechIconTypes.Playing, "\uE74F" }, { TextToSpeechIconTypes.Stopped, "\uE995" } };
        #endregion Text To Speech Icons



        // Event Handlers
        // ================================================================
        // ================================================================
        private static void MediaEnded(object sender, RoutedEventArgs e)
        {
            //Stop Audio
            StopAudio();
        }



        // Setup
        // ================================================================
        // ================================================================
        public static void Setup(Button btnspeak, TextBlock tbprediction)
        {
            //Set Elements
            btnSpeak = btnspeak;
            tbPrediction = tbprediction;

            //Set Event Handlers for Speech Element
            Speech.GetElement().MediaEnded += MediaEnded;
        }
        
        
        
        // Unloaded
        // =============================================================
        // =============================================================
        public static void Unloaded()
        {
            //Remove Event Handlers From Speech Element
            Speech.GetElement().MediaEnded -= MediaEnded;
        }



        #region Methods
        // Toggle Speech
        // ================================================================
        // ================================================================
        public static void ToggleSpeech()
        {
            //Check if a Prediction is Currently Playing
            //Else Check if a Prediction has been Set
            if (isPlaying)
            {
                //Stop Audio
                StopAudio();
            }
            else if (!string.IsNullOrEmpty(str_Prediction))
            {
                //Play Audio
                PlayAudio();
            }
        }


        // Read Prediction
        // ================================================================
        // ================================================================
        public static async void ReadPredictionAsync()
        {
            //Start Text To Speech
            bool isStarted = await Speech.StartAsync(str_Prediction);

            //Run Play Audio Method
            if (isStarted) { PlayAudio(); }
        }


        // Display Prediction
        // ================================================================
        // ================================================================
        public static void Display()
        {
            //Declare Variables
            string time, aspect, effect, persona, feature, consequence;

            //Get Values
            time = GetEntry(timeArray);
            aspect = GetEntry(aspectArray);
            effect = GetEntry(effectArray);
            persona = GetEntry(personaArray);
            feature = GetEntry(featureArray);
            consequence = GetEntry(consequenceArray);

            //Format Prediction
            str_Prediction = $"Over a period of {time} your {aspect} will {effect}. This will come to pass after you meet a {persona} with {feature} who for some reason you find yourself obliged to {consequence}.";

            //Display Prediction
            tbPrediction.Text = str_Prediction;

            //Read Prediction
            ReadPredictionAsync();
        }
        #endregion Methods



        #region Extensions
        // Audio
        // ================================================================
        // ================================================================
        // Play
        private static void PlayAudio()
        {
            //Play Audio
            Speech.Play();

            //Set isPlaying to True
            isPlaying = true;

            //Set Icon
            SetTextToSpeechIcon(TextToSpeechIconTypes.Playing);
        }

        // Stop
        private static void StopAudio()
        {
            //Pause Audio
            Speech.Pause();

            //Set isPlaying to False
            isPlaying = false;

            //Set Icon
            SetTextToSpeechIcon(TextToSpeechIconTypes.Stopped);
        }


        // Get Array Entry
        // ================================================================
        // ================================================================
        private static string GetEntry(string[] array)
        {
            //Return Array Entry
            return array[random.Next(0, array.Length)];
        }


        // Set Text to Speech Icon
        // ================================================================
        // ================================================================
        private static void SetTextToSpeechIcon(TextToSpeechIconTypes IconType)
        {
            //Get Icon
            TextToSpeechIcons.TryGetValue(IconType, out string icon);

            //Set Icon
            btnSpeak.Content = icon;
        }
        #endregion Extensions
    }
}