using System;
using System.Drawing;
using System.Windows.Forms;

namespace BusylightRestHost
{
    public class TrayMenu
    {
        private readonly NotifyIcon _notifyIcon;

        public TrayMenu()
        {
            var contextMenu = new ContextMenu();
            var menuItem = new MenuItem();

            contextMenu.MenuItems.AddRange(new[] {menuItem});

            menuItem.Index = 0;
            menuItem.Text = "E&xit";
            menuItem.Click += ExitMenuItem_Click;

            _notifyIcon = new NotifyIcon
            {
                ContextMenu = contextMenu,
                Text = "Busylight - Browser Integration Host",
                Visible = true,
                Icon = new Icon(GetType(), "Resources.icon.ico")
            };
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            _notifyIcon.Visible = false;
            Application.Exit();
        }
    }
}