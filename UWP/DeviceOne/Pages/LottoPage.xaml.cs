using Windows.UI.Xaml;
using DeviceOne.PageModel;
using Windows.UI.Xaml.Controls;

namespace DeviceOne.Pages
{
    public sealed partial class LottoPage : Page
    {
        // Model
        // =====================================================
        // =====================================================
        private LottoPageModel Model = new LottoPageModel();



        // Constructor
        // =====================================================
        // =====================================================
        public LottoPage()
        {
            //Initialize Components
            this.InitializeComponent();

            //Setup Model
            Model.Setup(vswgTickets, tbCash, tbResult, TextBoxTickets, tbNumbers, ButtonSelect);
        }



        #region Event Handlers
        // Numbers TextBox
        // =====================================================
        // =====================================================
        private void tbNumbers_LostFocus(object sender, RoutedEventArgs e)
        {
            //Sort Player Numbers
            Model.SortPlayerNumbers();
        }


        // Get Tickets Button
        // =====================================================
        // =====================================================
        private void ButtonSelect_Click(object sender, RoutedEventArgs e)
        {
            //Generate Tickets
            Model.GenerateTickets();
        }
        #endregion Event Handlers
    }
}