using Windows.UI.Xaml;
using DeviceOne.PageModel;
using Windows.UI.Xaml.Controls;

namespace DeviceOne.Pages
{
    public sealed partial class SlotPage : Page
    {
        // Slot View Model
        // ============================================================
        // ============================================================
        SlotPageModel Model;



        // Constructor
        // ============================================================
        // ============================================================
        public SlotPage()
        {
            //Initialize Components
            this.InitializeComponent();

            //Initialize Model
            Model = new SlotPageModel(textBlockDollars, tbFreeSpins, tbCashWon, buttonPlay, btnFreeSpin, buttonAddCash, btnGRulesOpen, ResultPanel, gGameRules, imageWinLose, imageWheel1, imageWheel2, imageWheel3);
        }



        #region Event Handlers
        // Play
        // ============================================================
        // ============================================================
        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            //Run Play Method
            Model.Play();
        }


        // Free Spin
        // ============================================================
        // ============================================================
        private void btnFreeSpin_Click(object sender, RoutedEventArgs e)
        {
            //Free Spin
            Model.FreeSpin();
        }


        // Add Cash
        // ============================================================
        // ============================================================
        private void btnAddCash_Click(object sender, RoutedEventArgs e)
        {
            //Run Manage Cash Method
            App.ManageCash(App.addCash, true);

            //Display Updated Cash Value
            App.DisplayCash(textBlockDollars);
        }


        // Wheel Tapped
        // ============================================================
        // ============================================================
        private void Wheel_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            //Run WheelSpin Method
            Model.WheelSpin(sender);
        }


        // Game Rules
        // ============================================================
        // ============================================================
        private void btnGameRules_Click(object sender, RoutedEventArgs e)
        {
            //Toggle Game Rules Poster
            gGameRules.Visibility = gGameRules.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }
        #endregion Event Handlers
    }
}