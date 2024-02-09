using DeviceOne.Models;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using System.Linq;

namespace DeviceOne.Pages
{
    public sealed partial class DrinksPage : Page
    {
        // Variables
        // ========================================================================
        // ========================================================================
        public List<DrinksModel> Drinks = new List<DrinksModel>()
        {
            new DrinksModel() { Name = "Black Heart", Image = "ms-appx:///Assets/Drinks/Black_Heart.png", Description = "A true blend of dark rum and cola, Black Heart Rum has a genuine heritage in New Zealand, closely linked with the nation's maritime history. It was first produced in 1842 by Henry White, who made his fortune selling Rum to troops during the Crimean War of 1854 TO 1856.", Mix = "4.8% Alcohol" },
            new DrinksModel() { Name = "Cody VSOB", Image = "ms-appx:///Assets/Drinks/Cody.png", Description = "Cody's Very Special Old Bourbon (VSOB) is made with 4 year old Special Reserve Bourbon for a smooth taste.", Mix = "7% Alcohol" },
            new DrinksModel() { Name = "Guinness", Image = "ms-appx:///Assets/Drinks/Guinness.png", Description = "Guinness is known worldwide as the beer of Ireland, and the gold standard for stouts. With an initial malt and caramel flavour, Guinness Draught finishes with a dry roasted bitterness. The malted barley is roasted giving Guinness it's dark, rich red colour.", Mix = "4.1% Alcohol" },
            new DrinksModel() { Name = "KGB", Image = "ms-appx:///Assets/Drinks/KGB.png", Description = "KGB Vodka and Lemon is a blend of 100% premium vodka and lemon. KGB's are lightly carbonated, refreshing and full of zest.", Mix = "4.8% Alcohol" },
            new DrinksModel() { Name = "Lion Red", Image = "ms-appx:///Assets/Drinks/Lion_Red.png", Description = "According to the date on cans and bottles of Lion Red, the beer was established (and thus presumably first brewed) in 1907. The name Lion Red was first adopted by the public to describe the red coloured label and can. Lion Breweries responded by changing the official name from Lion Beer to Lion Red in the midst of 1980.", Mix = "4.0% Alcohol" },
            new DrinksModel() { Name = "Tui", Image = "ms-appx:///Assets/Drinks/Tui.png", Description = "For more than 100 years Tui was the best kept secret throughout the bars of the Wairarapa and Hawkes Bay. First brewed on the banks of the Mangatainoka River in 1889 by entrepreneur Henry Wagstaff, Tui has now firmly cemented its place in Kiwi's lives.", Mix = "4.0% Alcohol" },
        };



        // Constructor
        // ========================================================================
        // ========================================================================
        public DrinksPage()
        {
            //Initialize Components
            this.InitializeComponent();

            //Setup Drinks Model
            DrinksModel.Setup(Drinks, DrinksListBox, TextBoxName, ImageDrink, TextBlockRecipe, TextBlockMix);
        }



        #region Event Handlers
        // Selection Changed
        // ========================================================================
        // ========================================================================
        private void lbDrinks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Get Drink Model
            DrinksModel model = DrinksListBox.SelectedItem as DrinksModel;

            //Display Selected Item
            DrinksModel.DisplayItem(model);
        }


        // Tapped
        // ========================================================================
        // ========================================================================
        private void Item_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            //Get Drink Model
            DrinksModel model = Drinks.Single(i => i.Name == (string)(sender as ComboBoxItem).Content);

            //Set Selected Item
            DrinksListBox.SelectedItem = model;

            //Display Tapped Item
            DrinksModel.DisplayItem(model);
        }


        // Double Tapped
        // ========================================================================
        // ========================================================================
        private void DrinksListBox_DoubleTapped(object sender, Windows.UI.Xaml.Input.DoubleTappedRoutedEventArgs e)
        {
            //Read Drink Text
            DrinksModel.ReadText();
        }
        #endregion Event Handlers
    }
}