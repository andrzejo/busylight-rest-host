using System.Windows.Forms;

namespace BusylightRestHost.Utils
{
    public static class Dialogs
    {
        public static void Error(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}