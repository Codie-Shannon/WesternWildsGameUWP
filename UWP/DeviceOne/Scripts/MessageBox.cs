using System;
using Windows.UI.Popups;
using System.Threading.Tasks;

namespace DeviceOne
{
    public class MessageBox
    {
        // Standard Message
        // ========================================================
        // ========================================================
        public static async Task Show(string message)
        {
            //Create Dialog Object with Error Message
            MessageDialog dialog = new MessageDialog(message);

            //Show Error Message
            await dialog.ShowAsync();
        }



        // Message Box (Yes / No)
        // ========================================================
        // ========================================================
        // Source: https://stackoverflow.com/a/38128641
        public static async Task<bool> ShowYesNo(string message)
        {
            //Create New Message Dialog Object
            MessageDialog dialog = new MessageDialog(message);

            //Create Yes / No Command
            dialog.Commands.Add(new UICommand("Yes", null));
            dialog.Commands.Add(new UICommand("No", null));

            //Set Indexes
            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = 1;

            //Show Message Dialog
            var cmd = await dialog.ShowAsync();

            //Check if Result was Yes
            if (cmd.Label == "Yes")
            {
                //Return True
                return true;
            }

            //Return False
            return false;
        }
    }
}
