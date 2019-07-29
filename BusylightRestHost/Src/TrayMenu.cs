using System;
using System.Drawing;
using System.Windows.Forms;
using BusylightRestHost.Utils;

namespace BusylightRestHost
{
    public class TrayMenu
    {
        private readonly NotifyIcon _notifyIcon;
        private readonly ContextMenu _contextMenu;

        public TrayMenu()
        {
            _contextMenu = new ContextMenu();

            AddItem(ApplicationText.GetAppHint(), AboutMenuItem_Click);
            AddItem("-");
            AddItem("Open test page", OpenTestPageMenuItem_Click);
            AddItem("Open documentation", OpenDocsMenuItem_Click);
            AddItem("-");
            AddItem("E&xit", ExitMenuItem_Click);

            _notifyIcon = new NotifyIcon
            {
                ContextMenu = _contextMenu,
                Text = ApplicationText.GetAppHint(),
                Visible = true,
                Icon = new Icon(GetType(), "Resources.icon.ico")
            };
        }

        private void AboutMenuItem_Click(object sender, EventArgs e)
        {
            Dialogs.Info(ApplicationText.GetAbout());
        }

        private void AddItem(string label, EventHandler eventHandler = null)
        {
            var menuItem = new MenuItem {Text = label};
            if (eventHandler != null)
            {
                menuItem.Click += eventHandler;
            }
            _contextMenu.MenuItems.Add(menuItem);
        }

        private void OpenTestPageMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(ApplicationText.GetTestPageUrl());
        }

        private void OpenDocsMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(ApplicationText.DocsUrl);
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            _notifyIcon.Visible = false;
            Application.Exit();
        }
    }
}