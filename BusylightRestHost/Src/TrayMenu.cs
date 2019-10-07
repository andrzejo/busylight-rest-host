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
        private readonly Autostart _autostart;

        public TrayMenu()
        {
            _contextMenu = new ContextMenu();
            _contextMenu.Popup += MenuPopup;
            _autostart = new Autostart();

            AddItem(ApplicationText.GetAppHint(), AboutMenuItem_Click);
            AddItem("-");
            AddItem("Open test page", OpenTestPageMenuItem_Click);
            AddItem("Open documentation", OpenDocsMenuItem_Click);
            AddItem("-");
            AddItem("Start app with Windows", SetupAutostartMenuItem_Click, "autostart");
            AddItem("-");
            AddItem("E&xit", ExitMenuItem_Click);

            _notifyIcon = new NotifyIcon
            {
                ContextMenu = _contextMenu,
                Text = ApplicationText.GetAppHint(),
                Visible = true,
                Icon = new Icon(GetType(), "Resources.icon.ico")
            };
            
            _notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
            EventBus.GetInstance()
                .Bind(Events.SHOW_TIP_EVENT, (s, dictionary) => { SetBalloonTip(dictionary["text"]); });
        }

        private void SetBalloonTip(string text)
        {
            _notifyIcon.BalloonTipTitle = ApplicationText.GetAutostartText();
            _notifyIcon.BalloonTipText = text;
            _notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
            _notifyIcon.ShowBalloonTip(5000);
        }

        private void MenuPopup(System.Object sender, System.EventArgs e)
        {
            foreach (MenuItem item in _contextMenu.MenuItems)
            {
                if (item.Name == "autostart")
                {
                    item.Checked = _autostart.IsEnabled();
                }
            }
        }

        private void AboutMenuItem_Click(object sender, EventArgs e)
        {
            Dialogs.Info(ApplicationText.GetAbout());
        }

        private void AddItem(string label, EventHandler eventHandler = null, string name = null)
        {
            var menuItem = new MenuItem {Text = label, Name = name};
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

        private void SetupAutostartMenuItem_Click(object sender, EventArgs e)
        {
            _autostart.Toggle();
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            _notifyIcon.Visible = false;
            Application.Exit();
        }
    }
}