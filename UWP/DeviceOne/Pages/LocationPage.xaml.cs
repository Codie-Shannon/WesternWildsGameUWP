using DeviceOne.Models;
using DeviceOne.PageModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;

namespace DeviceOne.Pages
{
    public sealed partial class LocationPage : Page
    {
        #region Variables
        // Page Model
        // =========================================
        // =========================================
        private LocationPageModel Model = new LocationPageModel();


        // Map Token
        // =========================================
        // =========================================
        public string MapToken = "08bJfaRkjW4jnmuX8FX0~bs4nCP6fZG-RhHmEVlmW0w~AiEH30r4Eo6OGfv_SRsEVMYn0NOdvnz0Ps-s8UffRIZpLyyL3OoillHY-au2OepJ";
        #endregion Variables



        // Constructor
        // ============================================
        // ============================================
        public LocationPage()
        {
            //Initialize Components
            this.InitializeComponent();

            //Set DataContext
            this.DataContext = this;

            //Run Setup Method
            Model.Setup(Map, gIconTitlePanel, tbIconTitle, cbLocations, cbStyles);
        }



        #region Event Handlers
        // Map
        // ========================================================
        // ========================================================
        private void Map_MapTapped(MapControl sender, MapInputEventArgs args)
        {
            //Run Map Tapped Method
            Model.MapTapped(args.Position, args.Location);
        }

        private void Map_MapRightTapped(MapControl sender, MapRightTappedEventArgs args)
        {
            //Remove Map Icon
            Model.RemoveMapIcon(Map.FindMapElementsAtOffset(args.Position));
        }

        private void Map_ZoomLevelChanged(MapControl sender, object args)
        {
            //Update Street View Status
            Model.UpdateStreetViewStatusAsync();
        }


        // User Map Icons
        // ========================================================
        // ========================================================
        private void btnIconTitleApply_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            //Apply Icon
            Model.ApplyIcon();
        }

        private void btnIconTitleBack_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            //Hide Icon Title Panel Grid
            gIconTitlePanel.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }


        // Locations ComboBox
        // ========================================================
        // ========================================================
        private void cbLocations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Get and Set Selected Location
            LocationModel.SetLocation(LocationModel.GetLocation());
        }


        // Styles ComboBox
        // ========================================================
        // ========================================================
        private void cbStyles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Get and Set Selected Location
            MapStyleModel.SetMapStyle(MapStyleModel.GetMapStyle());
        }


        // Traffic Flow Toggle Button
        // ========================================================
        // ========================================================
        private void tglTrafficFlow_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            //Toggle Traffic Flow
            Map.TrafficFlowVisible = (bool)tglTrafficFlow.IsChecked;
        }
        #endregion Event Handlers
    }
}
