using Windows.UI.Xaml;
using DeviceOne.PageModel;
using Windows.UI.Xaml.Controls;

namespace DeviceOne.Pages
{
    public sealed partial class DicePage : Page
    {
        // Page Model
        // ====================================================
        // ====================================================
        private DicePageModel Model = new DicePageModel();



        // Constructor
        // ====================================================
        // ====================================================
        public DicePage()
        {
            //Initialize Components
            this.InitializeComponent();

            //Run Setup Method
            Model.Setup(gResult, gGameRules, player1Roll, player2Roll, btnGRulesOpen, Player1, Player2, tbResultHeader, tbResultContent, imageDice1, imageDice2, imageDice3, imageDice4, imageDice5, imageDice6, imageDice7, imageDice8, imageDice9, imageDice10);
        }



        #region Event Handlers
        // Player 1 Roll
        // ====================================================
        // ====================================================
        private void Player1Roll_Click(object sender, RoutedEventArgs e)
        {
            //Roll Player 1 Dice
            Model.Roll(ref Model.isPlayer1Rolled, false);
        }


        // Player 2 Roll
        // ====================================================
        // ====================================================
        private void Player2Roll_Click(object sender, RoutedEventArgs e)
        {
            //Roll Player 2 Dice
            Model.Roll(ref Model.isPlayer2Rolled, true);
        }


        // Game Rules
        // ====================================================
        // ====================================================
        private void btnGameRules_Click(object sender, RoutedEventArgs e)
        {
            //Toggle Games Rules Poster
            gGameRules.Visibility = gGameRules.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }
        #endregion Event Handlers
    }
}