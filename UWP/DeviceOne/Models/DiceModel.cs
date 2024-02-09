using System;
using Windows.UI.Xaml.Controls;
using System.Collections.Generic;
using Windows.UI.Xaml.Media.Imaging;

namespace DeviceOne.Models
{
    public class DiceModel
    {
        // Variables
        // =============================================
        // =============================================
        private static List<DiceModel> Models;
        private static Random random = new Random();
        private static List<string> Images = new List<string>()
        {
            "ms-appx:///Assets/Dice/Dice_1.png",
            "ms-appx:///Assets/Dice/Dice_2.png",
            "ms-appx:///Assets/Dice/Dice_3.png",
            "ms-appx:///Assets/Dice/Dice_4.png",
            "ms-appx:///Assets/Dice/Dice_5.png",
            "ms-appx:///Assets/Dice/Dice_6.png"
        };



        #region Properties
        // Roll
        // ==============================================
        // ==============================================
        private int _roll { get; set; }

        public int Roll { get => _roll; set => _roll = value; }


        // Dice Image
        // ==============================================
        // ==============================================
        private Image _dice { get; set; }

        public Image Dice { get => _dice; set => _dice = value; }


        // Number
        // ==============================================
        // ==============================================
        private int _number { get; set; }

        public int Number { get => _number; set => _number = value; }
        #endregion Properties



        // Setup
        // ==============================================
        // ==============================================
        public static void Setup(List<DiceModel> models, int defaultIndex = 0)
        {
            //Set Models to Passed models List
            Models = models;

            //Loop through elements in Models
            foreach (DiceModel model in Models)
            {
                //Set Default Dice Image for Dice
                model.Dice.Source = new BitmapImage(new Uri(Images[defaultIndex]));
            }
        }



        #region Methods
        // Roll
        // ==============================================
        // ==============================================
        public static void RollRow(bool isPlayer2 = false)
        {
            //Get Roll Indexes
            GetRollIndexes(isPlayer2, Models.Count, out int startPos, out int endPos);

            //Loop through elements in Models list
            for (int i = startPos; i < endPos; i++)
            {
                //Roll Current Looped Dice Element
                RollDice(Models[i]);
            }
        }
        #endregion Methods



        #region Extensions
        // Get Roll Indexes
        // =============================================
        // =============================================
        private static void GetRollIndexes(bool isPlayer2, int count, out int startPos, out int endPos)
        {
            //Set startPos
            startPos = 0;

            //Check what Player is Rolling
            if (!isPlayer2)
            {
                //Set End Position
                endPos = count / 2;
            }
            else
            {
                //Set Start Position
                startPos = count / 2;

                //Set End Position
                endPos = count;
            }
        }


        // Roll Dice
        // =============================================
        // =============================================
        private static void RollDice(DiceModel model)
        {
            //Generate Random Number Between 0 and 5
            int position = random.Next(0, 6);

            //Set Model's Dice Source to Dice Image at the Position of the position Integer
            model.Dice.Source = new BitmapImage(new Uri(Images[position]));

            //Set Model's Dice Number to Dice Number (Position + 1)
            model.Number = position + 1;
        }
        #endregion Extensions
    }
}