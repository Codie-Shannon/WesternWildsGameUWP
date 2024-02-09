using System;
using Windows.UI.Xaml.Controls;
using System.Collections.Generic;
using Windows.UI.Xaml.Media.Imaging;

namespace DeviceOne.Models
{
    public class SlotModel
    {
        #region Variables
        // Main
        // ========================================================
        // ========================================================
        private static int spinCount = 3;
        private static Random number = new Random();


        // Image List
        // ========================================================
        // ========================================================
        private List<string> slotImages = new List<string>() { "ms-appx:///Assets/Slot/Clover.png", "ms-appx:///Assets/Slot/Diamond.png", "ms-appx:///Assets/Slot/Heart.png", "ms-appx:///Assets/Slot/Spade.png" };
        #endregion Variables



        #region Properties
        // Slot Image
        // ========================================================
        // ========================================================
        private Image _image { get; set; }

        public Image Image { get => _image; set => _image = value; }


        // Image Index
        // ========================================================
        // ========================================================
        private int _index { get; set; }

        public int Index { get => _index; set => _index = value; }


        // Wheel Spin Counter
        // ========================================================
        // ========================================================
        private int _spinCounter { get; set; }

        public int SpinCounter { get => _spinCounter; set => _spinCounter = value; }
        #endregion Properties



        #region Methods
        // Change Image
        // ========================================================
        // ========================================================
        public void ChangeImage()
        {
            //Generate Number Between 1 and 4 and Assign it into Index
            Index = number.Next(0, 4);

            //Set Image of Image Wheel
            Image.Source = new BitmapImage(new Uri(slotImages[Index]));
        }


        // Reset Spin Counter
        // ========================================================
        // ========================================================
        public void ResetSpinCounter()
        {
            //Set Spin Counter
            SpinCounter = spinCount;
        }


        // Deduct Spin Counter
        // ========================================================
        // ========================================================
        public void DeductSpinCounter()
        {
            //Deduct 1 from Counter
            SpinCounter--;
        }
        #endregion Methods
    }
}