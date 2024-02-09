using System;
using System.Linq;
using System.Collections.Generic;
using Windows.Devices.Geolocation;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;

namespace DeviceOne.Models
{
    public class LocationModel
    {
        #region Variables
        // Location Model
        private static List<LocationModel> Locations;


        // Store Icon
        // =================================================
        // =================================================
        private static RandomAccessStreamReference StoreIcon = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/Location/Map/Location_Icon.png"));


        // Elements
        // =================================================
        // =================================================
        private static TextBlock tbLocation;
        private static ComboBox cbLocations;
        private static MapControl Map;
        #endregion Variables



        #region Properties
        // Label
        // =================================================
        // =================================================
        private string _label { get; set; }

        public string Label { get => _label; set => _label = value; }


        // Latitude
        // =================================================
        // =================================================
        private double _latitude { get; set; }

        public double Latitude { get => _latitude; set => _latitude = value; }


        // Longitude
        // =================================================
        // =================================================
        private double _longitude { get; set; }

        public double Longitude { get => _longitude; set => _longitude = value; }
        #endregion Properties



        // Setup
        // =======================================================
        // =======================================================
        public static void Setup(List<LocationModel> locations, MapControl map, ComboBox cblocations)
        {
            //Set Variables for Location Model
            cbLocations = cblocations;
            Map = map;
            Locations = locations;

            //Add Locations to ComboBox
            Add();

            //Set Store Location Icons
            SetLocationIcons();
        }



        #region Methods
        // Model Setup
        // =======================================================
        // =======================================================
        // Add Items to ComboBox
        private static void Add()
        {
            //Loop through Elements in List
            foreach (LocationModel location in Locations)
            {
                //Add Current Looped Location to ComboBox
                cbLocations.Items.Add(new ComboBoxItem() { Content = location.Label });
            }
        }

        // Set Location Icons
        private static void SetLocationIcons()
        {
            //Loop through elements in Locations List
            foreach (LocationModel location in Locations)
            {
                //Get Store's Basic Geo Position
                BasicGeoposition position = new BasicGeoposition() { Latitude = location.Latitude, Longitude = location.Longitude };

                //Initialize New Map Object
                MapIcon icon = new MapIcon
                {
                    //Set Location of Map Icon Object
                    Location = new Geopoint(position),

                    //Set Anchor Point for Map Icon
                    NormalizedAnchorPoint = new Windows.Foundation.Point(0.5, 1.0),

                    //Set Title
                    Title = $"{location.Label} Store Location",

                    //Set Image
                    Image = StoreIcon,

                    //Set Map Icon Index to 0
                    ZIndex = 0
                };

                //Add Map Icon to Map
                Map.MapElements.Add(icon);
            }
        }


        // Other
        // =======================================================
        // =======================================================
        // Get Location
        public static LocationModel GetLocation()
        {
            //Get Selected Item
            ComboBoxItem selected = (ComboBoxItem)cbLocations.SelectedItem;

            //Get and Return Location Model
            return Locations.Single(i => i.Label == (string)selected.Content);
        }

        // Display Location
        public static void DisplayLocation()
        {
            //Get and Format Latitude and Longitude
            string latitude = String.Format("{0,18:000.0000000000000}", Map.Center.Position.Latitude);
            string longitude = String.Format("{0,18:000.0000000000000}", Map.Center.Position.Longitude);

            //Display Latitude and Longitude
            tbLocation.Text = $"Latitude: {latitude}         Longitude: {longitude}";
        }

        // Set Location
        public static async void SetLocation(LocationModel location)
        {
            //Create Geoposition Object
            BasicGeoposition position = new BasicGeoposition() { Latitude = location.Latitude, Longitude = location.Longitude };

            //Unset Street Side View
            Map.CustomExperience = null;

            //Create Geopoint Object
            Geopoint point = new Geopoint(position);

            //Create Map Scene from Point Object
            MapScene scene = MapScene.CreateFromLocationAndRadius(point, 80, 0, 60);

            //Try Set Scene
            await Map.TrySetSceneAsync(scene, MapAnimationKind.Bow);
        }
        #endregion Methods
    }
}