using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace DeviceOne
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        #region Variables
        // Window Minimum Size
        // ====================================================
        // ====================================================
        internal static double DeviceMinimumScreenWidth = 200;
        internal static double DeviceMinimumScreenHeight = 400;


        // Window Maximum Size
        // ====================================================
        // ====================================================
        internal static double DeviceScreenWidth = 1288;
        internal static double DeviceScreenHeight = 715;


        // Player Cash
        // ============================================================
        // ============================================================
        public static int cash, addCash = 250;
        private static int initialCash = 500, maxCash = 999999999, maxAddCash = 1000;


        // Messages
        // ============================================================
        // ============================================================
        private static string str_AddCashLimitError = "The Add Cash Limit for this Game has been Reached!";
        private static string str_CashLimitError = "The Cash Limit for this Game has been Reached!";
        #endregion Variables



        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            //Initialize Components
            this.InitializeComponent();

            //Initialize Cash
            InitializeCash();

            //Set Event Handlers
            this.Suspending += OnSuspending;
        }



        #region Event Handlers
        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            //Get Root Frame
            Frame rootFrame = Window.Current.Content as Frame;

            //Do not repeat app initialization when the Window already has content, just ensure that the window is active
            if (rootFrame == null)
            {
                //Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                //Set OnNavigationFailed Event Handler
                rootFrame.NavigationFailed += OnNavigationFailed;

                //Place the Frame in the Current Window
                Window.Current.Content = rootFrame;
            }

            //Check if the Application was Not Prelaunched
            if (e.PrelaunchActivated == false)
            {
                //Check if the Content of RootFrame has not been Set
                if (rootFrame.Content == null)
                {
                    //When the navigation stack isn't restored navigate to the first page,
                    //configuring the new page by passing required information as a navigation parameter
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                // Ensure the current window is active
                Window.Current.Activate();
            }
        }


        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e) { throw new Exception("Failed to load Page " + e.SourcePageType.FullName); }


        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            //Delay Application Suspending Operation
            var deferral = e.SuspendingOperation.GetDeferral();

            //Notify OS that the Application has Saved it's Data and is Ready to be Suspended
            deferral.Complete();
        }
        #endregion Event Handlers



        #region Methods
        // Initialize The Player's Cash
        // ============================================================
        // ============================================================
        private void InitializeCash()
        {
            //Get Value which Indicates if the Application has been Loaded before
            bool isSetup = ApplicationData.Current.LocalSettings.Values["Loaded"] == null ? false : true;

            //Check if it is the first time the application has been loaded
            if (!isSetup)
            {
                //Setup Cash Value
                ApplicationData.Current.LocalSettings.Values["Cash"] = initialCash;

                //Set Value Indicating that the Application has been Loaded before
                ApplicationData.Current.LocalSettings.Values["Loaded"] = true;
            }

            //Set Cash
            cash = (int)ApplicationData.Current.LocalSettings.Values["Cash"];
        }


        // Manage Player's Cash
        // ============================================================
        // ============================================================
        public static void ManageCash(int value, bool isAddCash = false)
        {
            //Check if isAddCash is equal to true and if cash is above or equal to maxAddCash
            //Else check if adding addCash to cash is equal or above maxAddCash
            //Else check if cash is above or equal to maxCash
            //Else check if cash + value is above or equal maxCash
            if (isAddCash == true && cash >= maxAddCash)
            {
                //Show Error Message
                MessageBox.Show(str_AddCashLimitError);
            }
            else if (isAddCash == true && cash + addCash >= maxAddCash)
            {
                //Set cash to maxAddCash
                cash = maxAddCash;
            }
            else if (cash >= maxCash)
            {
                //Show Error Message
                MessageBox.Show(str_CashLimitError);
            }
            else if ((cash + value) >= maxCash)
            {
                //Set cash to maxCash
                cash = maxCash;
            }
            else
            {
                //Add value to cash
                cash += value;
            }

            //Save Cash Value
            ApplicationData.Current.LocalSettings.Values["Cash"] = cash;
        }


        // Display Player's Cash
        // ============================================================
        // ============================================================
        public static void DisplayCash(TextBlock text, int value = -1)
        {
            //Get Value
            value = value == -1 ? cash : value;

            //Format and Set Value
            text.Text = $"${Math.Abs(value).ToString("0.0")}";
        }
        #endregion Methods
    }
}