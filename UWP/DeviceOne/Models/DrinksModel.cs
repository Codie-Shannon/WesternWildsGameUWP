using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace DeviceOne.Models
{
    public class DrinksModel
    {
        #region Variables
        // Model
        // ===============================================================
        // ===============================================================
        private static List<DrinksModel> Drinks = new List<DrinksModel>();


        // Elements
        // ===============================================================
        // ===============================================================
        public static ListBox lbDrinks;
        private static TextBox tbDrinkName;
        private static Image imgDrink;
        private static TextBlock tbDescription, tbMix;
        #endregion Variables



        #region Properties
        // Name
        // ===============================================================
        // ===============================================================
        private string _name { get; set; }

        public string Name { get => _name; set => _name = value; }


        // Image
        // ===============================================================
        // ===============================================================
        private string _image { get; set; }

        public string Image { get => _image; set => _image = value; }


        // Description
        // ===============================================================
        // ===============================================================
        private string _description { get; set; }

        public string Description { get => _description; set => _description = value; }


        // Mix
        // ===============================================================
        // ===============================================================
        private string _mix { get; set; }

        public string Mix { get => _mix; set => _mix = value; }
        #endregion Properties



        // Setup
        // ===============================================================
        // ===============================================================
        public static void Setup(List<DrinksModel> drinks, ListBox lbdrinks, TextBox tbdrinkname, Image imgdrink, TextBlock tbdescription, TextBlock tbmix)
        {
            //Set Elements
            lbDrinks = lbdrinks;
            tbDrinkName = tbdrinkname;
            imgDrink = imgdrink;
            tbDescription = tbdescription;
            tbMix = tbmix;

            //Set Drinks List to Passed drinks List
            Drinks = drinks;
        }



        #region Methods
        // Display Item
        // ===============================================================
        // ===============================================================
        public static void DisplayItem(DrinksModel model)
        {
            //Stop Reading Text
            Speech.Pause();

            //Display Drink
            tbDrinkName.Text = model.Name;
            imgDrink.Source = new BitmapImage(new Uri(model.Image));
            tbDescription.Text = model.Description;
            tbMix.Text = model.Mix;
        }


        // Read Text
        // ===============================================================
        // ===============================================================
        public static void ReadText()
        {
            //Get Name
            string name = tbDrinkName.Text;

            //Format Text
            string text = $"The name of this drink is {name}. {tbDescription.Text}. {name} has a mix of {tbMix.Text}";

            //Read Text
            Speech.StartAsync(text);
        }
        #endregion Methods
    }
}