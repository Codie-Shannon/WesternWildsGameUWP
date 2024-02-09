using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;

namespace DeviceOne.Models
{
    public class MapStyleModel
    {
        #region Variables
        // Styles Model
        // =================================================
        // =================================================
        public static List<MapStyleModel> Styles;


        // Elements
        // =================================================
        // =================================================
        public static ComboBox cbStyles;
        public static MapControl Map;
        #endregion Variables



        #region Properties
        // Label
        // =================================================
        // =================================================
        private string _label { get; set; }

        public string Label { get => _label; set => _label = value; }


        // Style
        // =================================================
        // =================================================
        private MapStyle _style { get; set; }

        public MapStyle Style { get => _style; set => _style = value; }


        // Style Sheet
        // =================================================
        // =================================================
        private MapStyleSheet _styleSheet { get; set; }

        public MapStyleSheet StyleSheet { get => _styleSheet; set => _styleSheet = value; }


        // Custom Style Sheet
        // =================================================
        // =================================================
        private string _customStyle { get; set; }

        public string CustomStyle { get => _customStyle; set => _customStyle = value; }
        #endregion Properties



        // Setup
        // =================================================
        // =================================================
        public static void Setup(List<MapStyleModel> styles, MapControl map, ComboBox cbstyles)
        {
            //Set Variables for Map Style
            cbStyles = cbstyles;
            Map = map;
            Styles = styles;

            //Add Map Styles to ComboBox
            Add();
        }



        #region Methods
        // Add Items to ComboBox
        // =================================================
        // =================================================
        private static void Add()
        {
            //Loop through Elements in List
            foreach (MapStyleModel style in Styles)
            {
                //Add Current Looped Style to ComboBox
                cbStyles.Items.Add(new ComboBoxItem() { Content = style.Label });
            }
        }


        // Get Map Style
        // =================================================
        // =================================================
        public static MapStyleModel GetMapStyle()
        {
            //Get Selected Item
            ComboBoxItem selected = (ComboBoxItem)cbStyles.SelectedItem;

            //Get and Return Map Style Model
            return Styles.Single(i => i.Label == (string)selected.Content);
        }


        // Set Map Styless
        // =================================================
        // =================================================
        public static void SetMapStyle(MapStyleModel model)
        {
            //Check What Type of Map Style is Being Set
            if(model.Style != null)
            {
                //Set Map Style
                Map.Style = model.Style;
            }
            if (model.StyleSheet != null)
            {
                //Set Map Style Sheet
                Map.StyleSheet = model.StyleSheet;
            }
            else
            {
                //Set Custom Map Style
                SetCustomMapStyle(model.CustomStyle);
            }
        }

        private static async Task SetCustomMapStyle(string path)
        {
            //Get Map Style File
            Uri file = new Uri(path);

            //Convert Uri to Storage File
            StorageFile sfile = await StorageFile.GetFileFromApplicationUriAsync(file);

            //Read Text From Storage File
            string style = await FileIO.ReadTextAsync(sfile);

            //Set Style
            Map.StyleSheet = MapStyleSheet.Combine(new List<MapStyleSheet> { MapStyleSheet.ParseFromJson(style) });
        }
        #endregion Methods
    }
}