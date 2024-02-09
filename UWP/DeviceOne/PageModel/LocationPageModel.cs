using System;
using System.Linq;
using DeviceOne.Models;
using Windows.Foundation;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using System.Collections.Generic;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Controls.Maps;

namespace DeviceOne.PageModel
{
    public class LocationPageModel
    {
        #region Variables
        // Location Model
        // =========================================
        // =========================================
        private List<LocationModel> Locations = new List<LocationModel>()
        {
            new LocationModel() { Label = "Levin", Latitude = -40.624477, Longitude = 175.283768 },
            new LocationModel() { Label = "New Plymouth", Latitude = -39.055790, Longitude = 174.084518 },
            new LocationModel() { Label = "Porirua", Latitude = -41.099579, Longitude = 174.869888 },
            new LocationModel() { Label = "Rotorua", Latitude = -38.140659, Longitude = 176.253372 },
            new LocationModel() { Label = "Taupo", Latitude = -38.688351, Longitude = 176.069366 },
            new LocationModel() { Label = "Whakatane", Latitude = -37.950580, Longitude = 176.997957 },
            new LocationModel() { Label = "Papanui", Latitude = -43.490704, Longitude = 172.622452 },
            new LocationModel() { Label = "Dunedin", Latitude = -45.875244, Longitude = 170.508835 }
        };


        // Map Style Model
        // =========================================
        // =========================================
        private List<MapStyleModel> MapStyles = new List<MapStyleModel>()
        {
            new MapStyleModel() { Label = "Western Wilds", CustomStyle = "ms-appx:///Assets/Location/Map/WesternWildMapStyle.mssjson" },
            new MapStyleModel() { Label = "Aerial", Style = MapStyle.Aerial },
            new MapStyleModel() { Label = "Aerial 3D", Style = MapStyle.Aerial3D },
            new MapStyleModel() { Label = "Aerial with Roads", Style = MapStyle.AerialWithRoads },
            new MapStyleModel() { Label = "Aerial 3D with Roads", Style = MapStyle.Aerial3DWithRoads },
            new MapStyleModel() { Label = "Road Light", StyleSheet = MapStyleSheet.RoadLight() },
            new MapStyleModel() { Label = "Road High Constrast Light", StyleSheet = MapStyleSheet.RoadHighContrastLight() },
            new MapStyleModel() { Label = "Road Dark", StyleSheet = MapStyleSheet.RoadDark() },
            new MapStyleModel() { Label = "Road High Contrast Dark", StyleSheet = MapStyleSheet.RoadHighContrastDark() }
        };


        // Elements
        // =========================================
        // =========================================
        private MapControl Map;
        private Grid gIconTitlePanel;
        private TextBox tbIconTitle;


        // Map Icons
        // ===================================================
        // ===================================================
        private Geopoint MapIconPoint;
        private MapIcon selectedIcon;
        private int selectedIconIndex;
        private bool isSelectedIcon = false;


        // Other
        // =========================================
        // =========================================
        private static bool isStreetView = false;
        private static string InvalidIconTitle = "You have entered an invalid icon title, please enter a valid icon title to continue.";
        #endregion Variables



        // Setup
        // ============================================
        // ============================================
        public void Setup(MapControl map, Grid gicontitlepanel, TextBox tbicontitle, ComboBox cblocations, ComboBox cbstyles)
        {
            //Set Elements
            Map = map;
            gIconTitlePanel = gicontitlepanel;
            tbIconTitle = tbicontitle;

            //Setup Location Model
            LocationModel.Setup(Locations, map, cblocations);

            //Setup Map Style Model
            MapStyleModel.Setup(MapStyles, Map, cbstyles);
        }



        #region Methods
        // Update Street View Status
        // ============================================
        // ============================================
        public async Task UpdateStreetViewStatusAsync()
        {
            //Check if the Map Cannot be Zoomed In, if the Street Side View is Supported and if isStreetView is Set to False
            if (!Map.CanZoomIn && Map.IsStreetsideSupported && isStreetView == false)
            {
                //Set isStreetView to True
                isStreetView = true;

                //Set the Position for the Street View
                BasicGeoposition position = new BasicGeoposition() { Latitude = Map.Center.Position.Latitude, Longitude = Map.Center.Position.Longitude };

                //Create a Geographic Point for the Street View
                Geopoint point = new Geopoint(position);

                //Create Panoramic View of Geographic Point 
                StreetsidePanorama street = await StreetsidePanorama.FindNearbyAsync(point);

                //Check if a Panoramic View has been Set.
                if (street != null)
                {
                    //Create the Streetside View
                    StreetsideExperience ssView = new StreetsideExperience(street);

                    //Assign the Streetside View into the Map
                    Map.CustomExperience = ssView;
                }

                //Set isStreetView to False
                isStreetView = false;
            }
        }



        // Map Icons
        // ============================================
        // ============================================
        public void MapTapped(Point position, Geopoint point)
        {
            //Get Nearby Map Elements
            IReadOnlyList<MapElement> elements = Map.FindMapElementsAtOffset(position);

            //Set Map Icon Point
            MapIconPoint = point;

            //Check if a Custom Experience (Street View) is Not Set
            if (Map.CustomExperience == null)
            {
                //Check if No Nearby Elements Exist
                //Else Check if the Nearby Element is Valid (Not a Store Icon)
                if (elements.Count == 0)
                {
                    //Clear Icon Title TextBox
                    tbIconTitle.Text = string.Empty;

                    //Show Icon Title Panel
                    gIconTitlePanel.Visibility = Windows.UI.Xaml.Visibility.Visible;
                }
                else if(ValidateIcon(elements[0] as MapIcon))
                {
                    //Set isSelectedIcon to True
                    isSelectedIcon = true;

                    //Set Selected Icon
                    selectedIcon = elements[0] as MapIcon;

                    //Set Selected Icon Index
                    selectedIconIndex = Map.MapElements.IndexOf(elements[0]);

                    //Set Icon Title TextBox to the Selected Icon's Title
                    tbIconTitle.Text = selectedIcon.Title;

                    //Show Icon Title Panel
                    gIconTitlePanel.Visibility = Windows.UI.Xaml.Visibility.Visible;
                }
            }
        }

        public void ApplyIcon()
        {
            //Check if TextBox is Empty
            if (string.IsNullOrEmpty(tbIconTitle.Text))
            {
                //Show Error Message
                MessageBox.Show(InvalidIconTitle);
            }
            else
            {
                //Hide Icon Title Panel
                gIconTitlePanel.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

                //Check if a Map Icon is Selected
                if (isSelectedIcon == true)
                {
                    //Update Map Icon
                    UpdateMapIcon();
                }
                else
                {
                    //Create Map Icon
                    CreateMapIcon();
                }
            }
        }

        public async void RemoveMapIcon(IReadOnlyList<MapElement> elements)
        {
            //Check if any Nearby Map Elements Exist and if a Nearby Element does Exist if it valid (Not a Store Icon)
            if (elements.Count > 0 && ValidateIcon(elements[0] as MapIcon))
            {
                //Show Confirmation Message
                bool isRemove = await MessageBox.ShowYesNo($"Are you sure you would like to remove the map icon of {(elements[0] as MapIcon).Title} from the map?");

                //Check if the user responded with yes
                if (isRemove)
                {
                    //Remove Map Element
                    Map.MapElements.Remove(elements[0]);
                }
            }
        }


        // Extensions
        // ============================================
        // ============================================
        private void UpdateMapIcon()
        {
            //Set Selected Icon Title
            selectedIcon.Title = tbIconTitle.Text;

            //Remove Existing Icon
            Map.MapElements.RemoveAt(selectedIconIndex);

            //Insert Updated Icon
            Map.MapElements.Add(selectedIcon);

            //Set isSelectedIcon to False
            isSelectedIcon = false;
        }

        private void CreateMapIcon()
        {
            //Initialize New Map Object
            MapIcon icon = new MapIcon
            {
                //Set Location of Map Icon Object
                Location = MapIconPoint,

                //Set Anchor Point for Map Icon
                NormalizedAnchorPoint = new Point(0.5, 1.0),

                //Set Text
                Title = tbIconTitle.Text,

                //Set Map Icon Index to 0
                ZIndex = 0
            };

            //Add Map Icon to Map
            Map.MapElements.Add(icon);
        }



        // Validation
        // ============================================
        // ============================================
        private bool ValidateIcon(MapIcon icon)
        {
            //Format Icon Title
            string title = icon.Title.Replace(" Store Location", "");

            //Check if the selected icon is a store icon
            if (Locations.Any(i => i.Label == title))
            {
                //Return False
                return false;
            }

            //Return True
            return true;
        }
        #endregion Methods
    }
}