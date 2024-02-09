using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace DeviceOne.PageModel
{
    public class LottoPageModel
    {
        #region Variables
        // Elements
        // =============================================
        // =============================================
        private VariableSizedWrapGrid vswgTickets;
        private TextBlock tbCash, tbResult;
        private TextBox TextBoxTickets, tbNumbers, tbMatch;
        private Button ButtonSelect;


        // Tickets
        // =====================================================
        // =====================================================
        private int[] numbers, numArray = new int[6];
        private Random randomNumber;
        private int ticketCost = 20, lotteryLow = 5000, lotteryHigh = 20000;


        // Result
        // =====================================================
        // =====================================================
        private bool isWin = false;
        private string str_win = "Winner!!!", str_lose = "Loser!!!";
        private int resultDelay = 5 * 1000;


        // Validation
        // =====================================================
        // =====================================================
        private string str_InvalidTicketEntry = "An invalid ticket count or ticket number was entered. Enter a valid ticket count and ticket number in order to play the game.";
        #endregion Variables



        // Setup
        // =====================================================
        // =====================================================
        public void Setup(VariableSizedWrapGrid vswgtickets, TextBlock tbcash, TextBlock tbresult, TextBox textboxtickets, TextBox tbnumbers, Button buttonselect)
        {
            //Set Elements
            vswgTickets = vswgtickets;
            tbCash = tbcash;
            tbResult = tbresult;
            TextBoxTickets = textboxtickets;
            tbNumbers = tbnumbers;
            ButtonSelect = buttonselect;

            //Initialize randomNumber
            randomNumber = new Random();

            //Set Cash Text
            App.DisplayCash(tbCash);
        }



        #region Methods
        // Generate Tickets
        // =====================================================
        // =====================================================
        // Generate Tickets
        public void GenerateTickets()
        {
            //Clear vswgTickets
            vswgTickets.Children.Clear();

            //Get Ticket Count
            int.TryParse(TextBoxTickets.Text, out int ticketCount);

            //Get Player Numbers
            GetNumbers();

            //Validate Ticket Count and Ticket Numbers
            if (ValidateTicketCount(ticketCount) && ValidateNumbers())
            {
                //Validate Ticket Cost
                if (ValidateTicketCost(ticketCount))
                {
                    //Generate Rows
                    GenerateRows(ticketCount);

                    //Run Result Method
                    Result();

                    //Bring Match into View
                    BringInToView(tbMatch);

                    //Clear tbMatch
                    tbMatch = null;
                }
            }
            else
            {
                //Show Error Message
                MessageBox.Show(str_InvalidTicketEntry);
            }
        }


        // Generate Rows
        private void GenerateRows(int ticketCount)
        {
            //Loop the Length of ticketCount
            for (int i = 0; i < ticketCount; i++)
            {
                //Set Numbers to Zero
                for (int j = 0; j < numArray.Length; j++) { numArray[j] = 0; }

                //Generate Numbers
                for (int j = 0; j < numArray.Count(); j++) { numArray[j] = randomNumber.Next(1, 50); }

                //Sort Numbers
                SortNumbers(ref numArray);

                //Display Numbers
                DisplayNumbers();
            }
        }


        // Player Numbers
        // =====================================================
        // =====================================================
        public void SortPlayerNumbers()
        {
            //Get Player Numbers
            GetNumbers();

            //Sort Numbers
            SortNumbers(ref numbers);

            //Display Numbers
            DisplayNumbers(tbNumbers);
        }


        // Result
        // =====================================================
        // =====================================================
        private async void Result()
        {
            //Unfocus Button
            (Window.Current.Content as Frame).Focus(FocusState.Programmatic);

            //Disable ButtonSelect
            ButtonSelect.IsEnabled = false;

            //Delay Task
            await Task.Delay(resultDelay);

            //Check if the Player Won
            if (isWin)
            {
                //Set Result Text
                tbResult.Text = str_win;

                //Generate Random Number Between lotteryLow - lotteryHigh and Add it to Cash
                App.ManageCash(randomNumber.Next(lotteryLow, lotteryHigh));

                //Update Cash Text
                App.DisplayCash(tbCash);
            }
            else
            {
                //Set Result Text
                tbResult.Text = str_lose;
            }

            //Toggle Result
            ToggleResult(true);

            //Delay Task
            await Task.Delay(resultDelay);

            //Toggle Result
            ToggleResult(false);

            //Enable Button Select
            ButtonSelect.IsEnabled = true;

            //Set isWin to False
            isWin = false;
        }


        // Validation
        // =====================================================
        // =====================================================
        // Validate Numbers
        private bool ValidateNumbers() { return numbers.Any(i => i < 1 || i >= 50) || numbers.Count() != 6 ? false : true; }

        // Validate Ticket Count
        private bool ValidateTicketCount(int ticketCount) { return ticketCount == 0 || ticketCount > 20 ? false : true; }

        // Validate Ticket Cost
        private bool ValidateTicketCost(int ticketcount)
        {
            //Get Cost
            int cost = ticketCost * ticketcount;

            //Check if the player has enough cash to purchase the entered tickets
            if (cost <= App.cash)
            {
                //Deduct Cash
                App.ManageCash(-cost);

                //Update Cash Text
                App.DisplayCash(tbCash);

                //Return True
                return true;
            }
            else
            {
                //Show Error Message
                MessageBox.Show($"You do not have enough cash to pay for the tickets. The tickets cost ${ticketCost} each. Enter a lower ticket count or earn more money via another game mode.");

                //Return False
                return false;
            }
        }
        #endregion Methods



        #region Extensions
        // Get Numbers
        // =====================================================
        // =====================================================
        private void GetNumbers()
        {
            //Get String Values
            string[] values = tbNumbers.Text.Contains(" ") ? tbNumbers.Text.Split(" ") : tbNumbers.Text.Split("-");

            //Convert String Array to Integer Array 
            numbers = Array.ConvertAll(values, s => s.StringToInt());
        }


        // Sort Numbers
        // =====================================================
        // =====================================================
        private void SortNumbers(ref int[] array)
        {
            //Loop through elemenets in array
            for (int i = 0; i < array.Length; i++)
            {
                //Loop through elements in array
                for (int j = 0; j < array.Length; j++)
                {
                    //Check if current looped outer element is more than the inner loop current element
                    if (array[i] < array[j])
                    {
                        //Get Numebers
                        int num1 = array[i];
                        int num2 = array[j];

                        //Swap Numbers
                        array[i] = num2;
                        array[j] = num1;
                    }
                }
            }
        }


        // Display Numbers
        // =====================================================
        // =====================================================
        private void DisplayNumbers()
        {
            //Variables
            string text = "";

            //Create TextBox Object
            TextBox tbTicket = new TextBox();

            //Validate Array
            if (numArray.Any(i => i > 0 && i < 50))
            {
                //Check Win Condition
                CheckWinCondition(ref tbTicket);

                //Loop through Elements in numArray
                for (int i = 0; i < numArray.Count(); i++)
                {
                    //Add Current Looped Element to Text Variable
                    text = numArray[i] < 10 ? $"{text} {numArray[i]} " : $"{text}{numArray[i]} ";
                }

                //Set Text
                tbTicket.Text = string.IsNullOrEmpty((string)tbTicket.Text) ? text : $"{tbTicket.Text}\n{text}";

                //Add TextBlock to VariableSizedWrapGrip
                vswgTickets.Children.Add(tbTicket);
            }
        }

        private void DisplayNumbers(TextBox tbNumbers)
        {
            //Variables
            string text = "";

            //Validate Array
            if (numbers.Any(i => i > 0 && i < 50))
            {
                //Loop through Elements in Array
                for (int i = 0; i < numbers.Count(); i++)
                {
                    //Add Current Looped Element to Text Variable
                    text = i == numbers.Count() - 1 ? $"{text}{numbers[i]}" : $"{text}{numbers[i]} ";
                }

                //Set Text
                tbNumbers.Text = text;
            }
        }


        // Result
        // =====================================================
        // =====================================================
        // Check Win Condition
        private void CheckWinCondition(ref TextBox tbTicket)
        {
            //Check if Conditions are a Match a For a Win
            bool iswin = DateTime.Now.Millisecond.ToString().StartsWith("7") ? true : false;

            //Check if iswin is Set to True and if isWin is Set to False
            if (iswin && !isWin)
            {
                //Assign iswin into isWin
                isWin = iswin;

                //Assign numbers into numArray
                numArray = numbers;

                //Set Border of TextBlock
                tbTicket.BorderBrush = new SolidColorBrush(Colors.White);
                tbTicket.BorderThickness = new Thickness(5);

                //Set tbMatch to tbTicket
                tbMatch = tbTicket;
            }
        }

        // Toggle Result Panel
        private void ToggleResult(bool toggle)
        {
            //Set Visibility of Tickets
            vswgTickets.Visibility = toggle ? Visibility.Collapsed : Visibility.Visible;

            //Set Visibility of Result TextBlock
            tbResult.Visibility = toggle ? Visibility.Visible : Visibility.Collapsed;
        }


        // Bring Element In To View
        // =====================================================
        // =====================================================
        private void BringInToView(UIElement element) { if (element != null) { element.StartBringIntoView(); } }
        #endregion Extensions
    }
}