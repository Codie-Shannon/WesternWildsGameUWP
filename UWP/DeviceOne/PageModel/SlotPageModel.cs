using DeviceOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace DeviceOne.PageModel
{
    public class SlotPageModel
    {
        #region Variables
        // Main
        // ============================================================
        // ============================================================
        private int freeSpins, maxFreeSpins = 25, spinCost = 25, wheelSpinCost = 5;
        private int lostDeduction = 50;
        private bool isSpin, isSpinReset = true;


        // Elements
        // ============================================================
        // ============================================================
        private TextBlock textBlockDollars, tbFreeSpins, tbCashWon;
        private Button buttonPlay, btnFreeSpins, buttonAddCash, btnGRulesOpen;
        private Grid ResultPanel, gGameRules;
        private Image imageWinLose;


        // Win / Lose Images
        // ============================================================
        // ============================================================
        private string str_WinImage = "ms-appx:///Assets/Slot/Slot_Won.png";
        private string str_LoseImage = "ms-appx:///Assets/Slot/Slot_Lost.png";


        // Messages
        // ============================================================
        // ============================================================
        private static string str_AddCashToPlayError = "Please Add Cash to Play the Game!";
        private string NoFreeSpinsError = "No Free Spins are Available!";
        private string str_WheelCostError = "You do not have enough cash to spin the wheels.";
        private string str_WheelLimitEror = "The spins for this wheel have ran out.";
        private string str_LostMessage = "You have lost the game! do you want to spin individual wheels in order to attempt to win the game?";


        // Slot Model Items
        // ============================================================
        // ============================================================
        private List<SlotModel> items = new List<SlotModel>();


        // Slot Symbol Items
        // ============================================================
        // ============================================================
        private List<SlotSymbol> symbols = new List<SlotSymbol>()
        {
            new SlotSymbol() { Name = "Clover", DoubleCash = 18, TripleCash = 35 },
            new SlotSymbol() { Name = "Diamond", DoubleCash = 25, TripleCash = 40 },
            new SlotSymbol() { Name = "Heart", DoubleCash = 35, TripleCash = 45, TripleFreeSpins = 1 },
            new SlotSymbol() { Name = "Spade", DoubleCash = 50, DoubleFreeSpins = 1, TripleCash = 100, TripleFreeSpins = 2 }
        };
        #endregion Variables



        // Constructor
        // ============================================================
        // ============================================================
        public SlotPageModel(TextBlock tbdollars, TextBlock tbfreespins, TextBlock tbcashwon, Button btnplay, Button btnfreespins, Button btnaddcash, Button btngrulesopen, Grid resultpanel, Grid ggamerules, Image imgwinlose, Image imgWheel1, Image imgWheel2, Image imgWheel3)
        {
            //Initialize Items
            items = new List<SlotModel> { new SlotModel { Image = imgWheel1 }, new SlotModel { Image = imgWheel2 }, new SlotModel { Image = imgWheel3 } };

            //Set Elements
            textBlockDollars = tbdollars;
            tbFreeSpins = tbfreespins;
            tbCashWon = tbcashwon;
            buttonPlay = btnplay;
            btnFreeSpins = btnfreespins;
            buttonAddCash = btnaddcash;
            btnGRulesOpen = btngrulesopen;
            ResultPanel = resultpanel;
            gGameRules = ggamerules;
            imageWinLose = imgwinlose;

            //Set Cash Text
            App.DisplayCash(textBlockDollars);

            //Initialize Free Spins
            InitializeFreeSpins();
        }



        #region Methods
        // Play
        // ============================================================
        // ============================================================
        public void Play()
        {
            //Check if Spin is Set to True
            //Else check if cash is above or equal to spinCost
            if (isSpin == true)
            {
                //Set Play Button Content to Play
                buttonPlay.Content = "Play";

                //Set isSpinReset to True
                isSpinReset = true;

                //Set isSpin to False
                isSpin = false;

                //Check Values
                CheckValues();
            }
            else if (App.cash >= spinCost)
            {
                //Deduct Cash
                App.ManageCash(-spinCost);

                //Update Cash Text
                App.DisplayCash(textBlockDollars);

                //Spin Slot
                SpinSlot();
            }
            else
            {
                //Show Message Dialog
                MessageBox.Show(str_AddCashToPlayError);
            }
        }


        // Free Spin
        // ============================================================
        // ============================================================
        // Initialize Free Spins
        private void InitializeFreeSpins()
        {
            //Get Value which Indicates if the Slot Page has been Loaded before
            bool isSetup = ApplicationData.Current.LocalSettings.Values["Slot_Loaded"] == null ? false : true;

            //Check if it is the first time the slot page has been loaded
            if (!isSetup)
            {
                //Setup Free Spins Value
                ApplicationData.Current.LocalSettings.Values[nameof(freeSpins)] = freeSpins;

                //Set Value Indicating that the Slot Page has been Loaded before
                ApplicationData.Current.LocalSettings.Values["Slot_Loaded"] = true;
            }

            //Set Free Spins to Saved Free Spins Value
            freeSpins = (int)ApplicationData.Current.LocalSettings.Values[nameof(freeSpins)];

            //Display Free Spins
            tbFreeSpins.Text = freeSpins.ToString();
        }

        // Free Spin
        public void FreeSpin()
        {
            //Check if the user has any free spins
            if(freeSpins > 0)
            {
                //Deduct 1 from Free Spins Variable
                freeSpins--;

                //Set Free Spins Set
                tbFreeSpins.Text = freeSpins.ToString();

                //Spin Slot
                SpinSlot();
            }
            else
            {
                //Show Error Message
                MessageBox.Show(NoFreeSpinsError);
            }
        }


        // Wheel Spin
        // ============================================================
        // ============================================================
        public void WheelSpin(object sender)
        {
            //Check if isSpin is Set to True
            if (isSpin == true)
            {
                //Get Wheel
                Image wheel = (Image)sender;

                //Get Image's SlotModel
                SlotModel item = items.Single(i => i.Image == wheel);

                //Perform Spin Validation
                if (item.SpinCounter > 0 && App.cash >= wheelSpinCost)
                {
                    //Change Image
                    item.ChangeImage();

                    //Deduct Cash
                    App.ManageCash(-wheelSpinCost);

                    //Deduct Spin Counter
                    item.DeductSpinCounter();
                }
                else if(App.cash < wheelSpinCost)
                {
                    //Show Error Message
                    MessageBox.Show(str_WheelCostError);
                }
                else
                {
                    //Show Error Message
                    MessageBox.Show(str_WheelLimitEror);
                }
            }
        }
        #endregion Methods



        #region Extensions
        // Result
        // ==============================================================================
        // ==============================================================================
        // Set Values Won
        private void SetValuesWon(int value, SlotSymbol symbol, ref int cash, ref int freespins)
        {
            //Check if value is equal to 2
            //Else check if value is equal to 3
            if (value == 2)
            {
                //Add DoubleCash to Cash
                cash += symbol.DoubleCash;

                //Add DoubleFreeSpins to freespins
                freespins += symbol.DoubleFreeSpins;
            }
            else if (value == 3)
            {
                //Add TripleCash to Cash
                cash += symbol.TripleCash;

                //Add TripleFreeSpins to freespins
                freespins += symbol.TripleFreeSpins;
            }
        }

        // Lost
        private async Task LostAsync()
        {
            //Check if isSpinReset is Set to True and if the Player has Enough Cash to Spin the Wheels
            if (isSpinReset == true && App.cash >= wheelSpinCost)
            {
                //Show Lost Message Dialog
                isSpin = await MessageBox.ShowYesNo(str_LostMessage);

                //Check if user wants to spin the wheels
                if (isSpin)
                {
                    //Set isSpinReset to False
                    isSpinReset = false;

                    //Loop through elements in items list
                    foreach (SlotModel item in items)
                    {
                        //Reset Spin Counter
                        item.ResetSpinCounter();
                    }

                    //Set Play Button's Content to Apply
                    buttonPlay.Content = "Apply";

                    //Return Method
                    return;
                }
            }

            //Lose
            Result(str_LoseImage, -lostDeduction);

            //Set isSpinReset to True
            isSpinReset = true;
        }

        // Result
        private async void Result(string image, int value)
        {
            //Hide Game Rules Poster
            gGameRules.Visibility = Visibility.Collapsed;

            //Set Result
            SetResult(Visibility.Visible, image, value);

            //Delay Task
            await Task.Delay(2000);

            //Show Result Panel
            ResultPanel.Visibility = Visibility.Visible;

            //Run Manage Cash Method
            App.ManageCash(value);

            //Display Updated Cash Value
            App.DisplayCash(textBlockDollars);

            //Delay Task
            await Task.Delay(5000);

            //Unset Result
            SetResult(Visibility.Collapsed);
        }

        // Set Result
        private void SetResult(Visibility toggle, string image = "", int value = 0)
        {
            //Check if Toggle Boolean Value (Visible = True, Collasped = False)
            if (toggle == Visibility.Visible)
            {
                //Set Image
                imageWinLose.Source = new BitmapImage(new Uri(image));

                //Check if adding value to cash will give a result below 0
                if ((value + App.cash) <= -1)
                {
                    //Set value to minus dollars
                    value = -App.cash;
                }

                //Set Cash Text Value
                App.DisplayCash(tbCashWon, value);
            }
            else if (toggle == Visibility.Collapsed)
            {
                //Hide Result Panel
                ResultPanel.Visibility = Visibility.Collapsed;
            }

            //Set toggle to Opposite Value
            toggle = toggle == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;

            //Toggle Game Rules Button
            btnGRulesOpen.IsEnabled = toggle == Visibility.Visible ? true : false;

            //Toggle Buttons
            buttonPlay.Visibility = toggle;
            btnFreeSpins.Visibility = toggle;
            buttonAddCash.Visibility = toggle;
        }


        // Generic
        // ==============================================================================
        // ==============================================================================
        // Check Values
        private void CheckValues()
        {
            //Variables
            int clover = 0, diamond = 0, heart = 0, spade = 0, cash = 0, freespins = 0;

            //Loop through elements in items list
            for (int i = 0; i < items.Count; i++)
            {
                //Check Index and Increment Appropriate Value by 1
                if (items[i].Index == 0) spade++;
                else if (items[i].Index == 1) clover++;
                else if (items[i].Index == 2) diamond++;
                else if (items[i].Index == 3) heart++;
            }

            //Set Wheel Spins and Cash Won
            SetValuesWon(clover, symbols.Single(i => i.Name == "Clover"), ref cash, ref freespins);
            SetValuesWon(diamond, symbols.Single(i => i.Name == "Diamond"), ref cash, ref freespins);
            SetValuesWon(heart, symbols.Single(i => i.Name == "Heart"), ref cash, ref freespins);
            SetValuesWon(spade, symbols.Single(i => i.Name == "Spade"), ref cash, ref freespins);

            //Check Value of cash
            if (cash > 0)
            {
                //Set Free Spins
                SetFreeSpins(freespins);

                //Show Result
                Result(str_WinImage, cash);
            }
            else
            {
                //Run Lost Method
                LostAsync();
            }
        }

        // Spin Slot
        private void SpinSlot()
        {
            //Loop through elements in items list
            foreach (SlotModel item in items)
            {
                //Change Image
                item.ChangeImage();
            }

            //Check Values
            CheckValues();
        }

        // Set Free Spins
        private void SetFreeSpins(int freespins)
        {
            //Check if freeSpins is above or equal to maxFreeSpins
            if ((freeSpins + freespins) >= maxFreeSpins)
            {
                //Set freeSpins to maxFreeSpins
                freeSpins = maxFreeSpins;
            }
            else
            {
                //Add freespins to freeSpins Variable
                freeSpins += freespins;
            }

            //Set Free Spins Text
            tbFreeSpins.Text = freeSpins.ToString();

            //Save Free Spins Value
            ApplicationData.Current.LocalSettings.Values[nameof(freeSpins)] = freeSpins;
        }
        #endregion Extensions
    }
}