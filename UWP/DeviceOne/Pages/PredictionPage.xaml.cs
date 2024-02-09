using Windows.UI.Xaml;
using DeviceOne.Models;
using Windows.UI.Xaml.Controls;

namespace DeviceOne.Pages
{
    public sealed partial class PredictionPage : Page
    {
        // Constructor
        // ================================================================
        // ================================================================
        public PredictionPage()
        {
            //Initialize Components
            this.InitializeComponent();

            //Setup Prediction Model
            PredictionModel.Setup(btnSpeak, TextBlockPrediction);
        }



        #region Event Handlers
        // Page
        // ================================================================
        // ================================================================
        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            //Unload Prediction Model
            PredictionModel.Unloaded();
        }


        // Speak Click
        // ================================================================
        // ================================================================
        private void btnSpeak_Click(object sender, RoutedEventArgs e)
        {
            //Toggle Speech
            PredictionModel.ToggleSpeech();
        }


        // Prediction Click
        // ================================================================
        // ================================================================
        private void btnPrediction_Click(object sender, RoutedEventArgs e)
        {
            //Display Prediction
            PredictionModel.Display();
        }
        #endregion Event Handlers
    }
}