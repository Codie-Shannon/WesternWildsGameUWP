using System;
using System.Linq;
using Windows.UI.Xaml;
using DeviceOne.Models;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using System.Collections.Generic;

namespace DeviceOne.PageModel
{
    // ====================== Values Codex ======================
    /* Kind of Prefix - 0 Ending
     * Five of a Kind - 6
     * Four of a Kind - 5
     * Three of a Kind - 3
     * Two of a Kind - 1
     * 
     * Full House Prefix - Starting 4
     * Full House - Three Dice Value / Two Dice Value - Joined
     * 
     * Low Straight - 399
     * High Straight - 400
     * 
     * Pair Prefix - Starting 2
     * Pair - Lowest Dice Value / Highest Dice Value - Joined
     * 
     * Highest Number - No Prefix */
    public class DicePageModel
    {
        #region Variables
        // Dice Models
        // ====================================================
        // ====================================================
        public List<DiceModel> Dice;


        // Elements
        // ====================================================
        // ====================================================
        private Grid gResult, gGameRules;
        private Button btnPlayer1Roll, btnPlayer2Roll, btnGRulesOpen;
        private TextBlock tbResultHeader, tbResultContent;
        private TextBox tbPlayer1, tbPlayer2;


        // Roll
        // ====================================================
        // ====================================================
        public bool isPlayer1Rolled, isPlayer2Rolled;


        // Result
        // ====================================================
        // ====================================================
        private int ResultStart = 8 * 1000, ResultEnd = 5 * 1000;
        private string str_NoOneWon = "NO ONE";
        private string str_Won = "WON THE MATCH!";
        private string str_DrawHeader = "DRAW";
        private string str_DrawContent = "THE MATCH WAS A DRAW!";


        // Messages
        // ====================================================
        // ====================================================
        private string InvalidPlayerNames = "The player names must be filled in and unique before you play a match.";
        #endregion Variables



        // Setup
        // ====================================================
        // ====================================================
        public void Setup(Grid gresult, Grid ggamerules, Button btnplayer1roll, Button btnplayer2roll, Button btngrulesopen, TextBox tbplayer1, TextBox tbplayer2, TextBlock tbresultheader, TextBlock tbresultcontent, Image imgdice1, Image imgdice2, Image imgdice3, Image imgdice4, Image imgdice5, Image imgdice6, Image imgdice7, Image imgdice8, Image imgdice9, Image imgdice10)
        {
            //Initialize Dice Models
            Dice = new List<DiceModel>()
            {
                new DiceModel() { Dice = imgdice1 },
                new DiceModel() { Dice = imgdice2 },
                new DiceModel() { Dice = imgdice3 },
                new DiceModel() { Dice = imgdice4 },
                new DiceModel() { Dice = imgdice5 },
                new DiceModel() { Dice = imgdice6 },
                new DiceModel() { Dice = imgdice7 },
                new DiceModel() { Dice = imgdice8 },
                new DiceModel() { Dice = imgdice9 },
                new DiceModel() { Dice = imgdice10 },
            };

            //Setup Dice Symbol Model
            DiceModel.Setup(Dice);

            //Set Elements
            gResult = gresult;
            gGameRules = ggamerules;
            btnPlayer1Roll = btnplayer1roll;
            btnPlayer2Roll = btnplayer2roll;
            btnGRulesOpen = btngrulesopen;
            tbPlayer1 = tbplayer1;
            tbPlayer2 = tbplayer2;
            tbResultHeader = tbresultheader;
            tbResultContent = tbresultcontent;
        }



        #region Methods
        // Roll
        // ========================================================
        // ========================================================
        public void Roll(ref bool isPlayerRolled, bool isPlayer2)
        {
            //Validate Player Names (Titles)
            if (ValidateNames())
            {
                //Check if Player has Already Rolled
                if (isPlayerRolled != true)
                {
                    //Roll Player Row
                    DiceModel.RollRow(isPlayer2);

                    //Set isPlayerRolled to True
                    isPlayerRolled = true;

                    //Get End Game Result
                    Result();
                }
            }
            else
            {
                //Show Error Message
                MessageBox.Show(InvalidPlayerNames);
            }
        }


        // Result
        // ========================================================
        // ========================================================
        public void Result()
        {
            //Check if Player 1 and Player 2 have Rolled
            if (isPlayer1Rolled && isPlayer2Rolled)
            {
                //Declare Identification Key Variables
                int idPlayer1, idPlayer2;

                //Get the Middle Point of Dice List
                int middle = Dice.Count / 2;

                //Get Players Result
                idPlayer1 = PlayerResult(Dice.GetRange(0, middle));
                idPlayer2 = PlayerResult(Dice.GetRange(middle, middle));

                //Display Result
                DisplayResult(idPlayer1, idPlayer2);

                //Set isPlayerRolled to False
                isPlayer1Rolled = false;
                isPlayer2Rolled = false;
            }
        }


        // Display Result
        // ========================================================
        // ========================================================
        private async void DisplayResult(int idPlayer1, int idPlayer2)
        {
            //Variables
            string header, content = str_Won;

            //Disable Game Rules Poster
            gGameRules.Visibility = Visibility.Collapsed;

            //Validate Victor and Set Appropriate Values for the header and content Variables 
            if (idPlayer1 > idPlayer2) { header = tbPlayer1.Text; }
            else if(idPlayer1 < idPlayer2) { header = tbPlayer2.Text; }
            else if(idPlayer1 == 0 && idPlayer2 == 0) { header = str_NoOneWon; }
            else { header = str_DrawHeader; content = str_DrawContent; }

            //Set Result
            SetResult(Visibility.Visible, header, content);

            //Delay Task
            await Task.Delay(ResultStart);

            //Show Result Poster
            gResult.Visibility = Visibility.Visible;

            //Delay Task
            await Task.Delay(ResultEnd);

            //Unset Result
            SetResult(Visibility.Collapsed);
        }
        #endregion Methods



        #region Extensions
        // Validation
        // ========================================================
        // ========================================================
        private bool ValidateNames()
        {
            //Validate Player Names
            if(string.IsNullOrEmpty(tbPlayer1.Text) || string.IsNullOrEmpty(tbPlayer2.Text) || tbPlayer1.Text == tbPlayer2.Text)
            {
                //Return False
                return false;
            }

            //Return True
            return true;
        }


        // Player Result
        // ========================================================
        // ========================================================
        private int PlayerResult(List<DiceModel> models)
        {
            //Get Values
            GetValues(models, out int kind, out int house, out int straight, out int pairs, out int counter);

            //Get and Return Player Result
            return GetPlayerResult(kind, house, straight, pairs, counter);
        }


        // Set Result
        // ========================================================
        // ========================================================
        private void SetResult(Visibility toggle, string header = "", string content = "")
        {
            //Check if header and content has been set
            if (!string.IsNullOrEmpty(header) && !string.IsNullOrEmpty(content))
            {
                //Set Header and Content of Result Poster
                tbResultHeader.Text = header;
                tbResultContent.Text = content;
            }

            //Check if toggle is Set to Visibility.Collasped
            if (toggle == Visibility.Collapsed) { gResult.Visibility = toggle; }

            //Set toggle to Opposite Value
            toggle = toggle == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;

            //Get Boolean Based Value from toggle
            bool istoggle = toggle == Visibility.Visible ? true : false;

            //Toggle Game Rules Open Button
            btnGRulesOpen.IsEnabled = istoggle;

            //Toggle Editing on Player Name Title TextBoxes
            tbPlayer1.IsReadOnly = !istoggle;
            tbPlayer2.IsReadOnly = !istoggle;

            //Toggle Roll Buttons
            btnPlayer1Roll.Visibility = toggle;
            btnPlayer2Roll.Visibility = toggle;
        }
        #endregion Extensions



        #region Get Methods
        // All
        // ========================================================
        // ========================================================
        private void GetValues(List<DiceModel> models, out int kind, out int house, out int straight, out int pairs, out int counter)
        {
            //Get Match Count
            List<int> match = GetMatchCount(models);

            //Get Kind
            kind = GetKind(match);

            //Get Full House
            house = GetHouse(match);

            //Get Pairs
            pairs = GetPairs(match);

            //Get Straight
            straight = GetStraight(models);

            //Get Collective Number
            counter = GetCollectiveNumber(models);
        }


        // Player Result
        // ========================================================
        // ========================================================
        private int GetPlayerResult(int kind, int house, int straight, int pairs, int counter)
        {
            //Variables
            int identity;

            //Validate Values Against Game Rules to Find Highest Valid Value
            //==============================================================
            //Check if Kind Starts with a Value of 6 or 5 and if Successful Set identity to kind
            if (kind.ToString().StartsWith("6") || kind.ToString().StartsWith("5")) { identity = kind; }

            //Else Check if House has been Set and if Successful Set identity to house
            else if (house > 400) { identity = house; }

            //Else Check if Straight has been Set and if Successful Set identity to straight
            else if (straight > 398) { identity = straight; }

            //Else Check if Kind Starts with a Value of 3 and if Successful Set identity to kind
            else if (kind.ToString().StartsWith("3")) { identity = kind; }

            //Else Check if Pairs has been Set and if Successful Set identity to Pairs
            else if (pairs > 200) { identity = pairs; }

            //Else Check if Kind Starts with 1 and if Sucessful Set identity to kind
            else if (kind.ToString().StartsWith("1")) { identity = kind; }

            //Else Set Identity to Counter
            else { identity = counter; }

            //Return Identity
            return identity;
        }


        // Match Count
        // ========================================================
        // ========================================================
        private List<int> GetMatchCount(List<DiceModel> models)
        {
            //Initialize Variables
            List<int> match = new List<int>(new int[6]);

            //Loop through Dice Models in models
            foreach (DiceModel model in models)
            {
                //Check what Number the Current Dice is and Add 1 to the Appropriate Integer
                if (model.Number == 1) match[5]++;
                else if (model.Number == 2) match[4]++;
                else if (model.Number == 3) match[3]++;
                else if (model.Number == 4) match[2]++;
                else if (model.Number == 5) match[1]++;
                else if (model.Number == 6) match[0]++;
            }

            //Return match Array
            return match;
        }


        // Kind
        // ========================================================
        // ========================================================
        private int GetKind(List<int> match)
        {
            //Get Highest Match Value
            int value = match.Max();

            //Get Index of Highest Match Value
            int index = match.IndexOf(value);

            //Check if Highest Match Value is Above 1
            if (value > 1)
            {
                //Get Dice Number
                int number = 6 - index;

                //Check Kind
                if (value > 3)
                {
                    //Return Kind Identification Key For Kind of 4 / 5
                    return Convert.ToInt32($"{value + 1}{number}0");
                }
                else if (value == 2)
                {
                    //Return Kind Identification Key For Kind of 2
                    return Convert.ToInt32($"{value - 1}{number}0");
                }
                else
                {
                    //Return Kind Identification Key For Kind of 3
                    return Convert.ToInt32($"{value}{number}0");
                }
            }

            //Return 0
            return 0;
        }


        // Full House
        // ========================================================
        // ========================================================
        private int GetHouse(List<int> match)
        {
            //Check if Matches Exist with Values of 3 and 2
            if (match.Any(i => i == 3) && match.Any(i => i == 2))
            {
                //Get Dice Numbers
                int dice2 = 6 - match.IndexOf(match.Single(i => i == 2));
                int dice3 = 6 - match.IndexOf(match.Single(i => i == 3));


                //Return House Identification Key
                return Convert.ToInt32($"4{dice3}{dice2}");
            }

            //Return 0
            return 0;
        }


        // Straight
        // ========================================================
        // ========================================================
        private int GetStraight(List<DiceModel> models)
        {
            //Initialize Variables
            int low = 0, high = 0;

            //Loop through elements in models list
            for (int i = 0; i < models.Count; i++)
            {
                //Check Model Number and Add Value to Appropriate Integer
                if (models[i].Number == i + 1) low++;
                else if (models[i].Number == i + 2) high++;
            }

            //Check if the Dice are in a Low Straight (1 - 5) or High Straight (2 - 6) Formation and Return Appropriate Value
            if (low == models.Count) { return 399; }
            else if (high == models.Count) { return 400; }

            //Return 0
            return 0;
        }


        // Pairs
        // ========================================================
        // ========================================================
        private int GetPairs(List<int> match)
        {
            //Variables
            List<int> pairs = new List<int>();

            //Loop through elements in match
            for (int i = 0; i < match.Count(); i++)
            {
                //Check if current looped match element is a Pair
                if (match[i] == 2)
                {
                    //Add Pair's Dice Number to pairs List
                    pairs.Add(6 - i);
                }
            }

            //Check if two Dice Numbers have been added to the pairs List
            if (pairs.Count == 2)
            {
                //Sort List from Lowest to Highest Values
                pairs.Sort();

                //Return Pair Identification Key
                return Convert.ToInt32($"2{pairs[0]}{pairs[1]}");
            }

            //Return 0
            return 0;
        }


        // Highest Number
        // ========================================================
        // ========================================================
        private int GetCollectiveNumber(List<DiceModel> models)
        {
            //Initialize Variables
            int counter = 0;

            //Loop through the models in the models list
            for (int i = 0; i < models.Count; i++)
            {
                //Add current looped model's number to the counter integer
                counter += models[i].Number;
            }

            //Return counter
            return counter;
        }
        #endregion Get Methods
    }
}