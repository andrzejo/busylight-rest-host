using System;
using System.Drawing;
using System.Windows.Forms;

namespace BusylightRestHost
{
    public class TrayMenu
    {
        private readonly NotifyIcon _notifyIcon;
        private readonly ContextMenu _contextMenu;

        public TrayMenu()
        {
            _contextMenu = new ContextMenu();

            AddItem("Open test page", OpenTestPageMenuItem_Click);
            AddItem("Open documentation", OpenDocsMenuItem_Click);
            _contextMenu.MenuItems.Add("-");
            AddItem("E&xit", ExitMenuItem_Click);

            _notifyIcon = new NotifyIcon
            {
                ContextMenu = _contextMenu,
                Text = ApplicationText.AppHint,
                Visible = true,
                Icon = new Icon(GetType(), "Resources.icon.ico")
            };
        }

        private void AddItem(string label, EventHandler eventHandler)
        {
            var menuItem = new MenuItem();
            menuItem.Text = label;
            menuItem.Click += eventHandler;
            _contextMenu.MenuItems.Add(menuItem);
        }

        private void OpenTestPageMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(ApplicationText.getTestPageUrl());
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