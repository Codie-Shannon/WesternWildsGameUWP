using Windows.UI.Xaml;
using DeviceOne.Pages;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Animation;

namespace DeviceOne
{
    public sealed partial class MainPage : Page
    {
        // Constructor
        // Page Buttonn
        // ===================================================================
        // ===================================================================
        public MainPage()
        {
            //Initialize Components
            this.InitializeComponent();

            //Run Setup Method
            Setup();
        }



        // Setup
        // ===================================================================
        // ===================================================================
        private void Setup()
        {
            //Setup the Device Sizing for the Application
            ApplicationView.GetForCurrentView().TryResizeView(new Size(App.DeviceScreenWidth, App.DeviceScreenHeight));
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(App.DeviceMinimumScreenWidth, App.DeviceMinimumScreenHeight));
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
        }



        #region Event Handlers
        // Page
        // ===================================================================
        // ===================================================================
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //Setup Speech Script
            Speech.Setup();

            //Set Start Page
            LoadPage();
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //Resize View
            ResizeView();
        }


        // Frame
        // ===================================================================
        // ===================================================================
        private void MyFrame_Navigated(object sender, NavigationEventArgs e)
        {
            //Stop Reading Text
            Speech.Pause();
        }


        // Page Buttonn
        // ===================================================================
        // ===================================================================
        private void PageButton_Click(object sender, RoutedEventArgs e)
        {
            //Get Page Name
            string name = ((Button)sender).Name.Replace("btn", "");

            //Load Page
            LoadPage(name);
        }
        #endregion Event Handlers



        #region Methods
        // View
        // ===================================================================
        // ===================================================================
        private void ResizeView()
        {
            //Resize View
            ApplicationView.GetForCurrentView().TryResizeView(new Size(App.DeviceScreenWidth, App.DeviceScreenHeight));
        }


        // Page
        // ===================================================================
        // ===================================================================
        private void LoadPage(string name = "")
        {
            //Check the value of name
            if (name == "Dice")
            {
                //Load Page
                MyFrame.Navigate(typeof(DicePage), null, new SuppressNavigationTransitionInfo());
            }
            else if (name == "Lotto")
            {
                //Load Page
                MyFrame.Navigate(typeof(LottoPage), null, new SuppressNavigationTransitionInfo());
            }
            else if (name == "Prediction")
            {
                //Load Page
                MyFrame.Navigate(typeof(PredictionPage), null, new SuppressNavigationTransitionInfo());
            }
            else if (name == "Slot")
            {
                //Load Page
                MyFrame.Navigate(typeof(SlotPage), null, new SuppressNavigationTransitionInfo());
            }
            else if (name == "Drinks")
            {
                //Load Page
                MyFrame.Navigate(typeof(DrinksPage), null, new SuppressNavigationTransitionInfo());
            }
            else if (name == "Company")
            {
                //Load Page
                MyFrame.Navigate(typeof(CompanyPage), null, new SuppressNavigationTransitionInfo());
            }
            else if (name == "Location")
            {
                //Load Page
                MyFrame.Navigate(typeof(LocationPage), null, new SuppressNavigationTransitionInfo());
            }
            else
            {
                //Load Page
                MyFrame.Navigate(typeof(HomePage), null, new SuppressNavigationTransitionInfo());
            }
        }
        #endregion Methods
    }
}