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
        private readonly Settings _settings;

        public TrayMenu()
        {
            _contextMenu = new ContextMenu();
            _contextMenu.Popup += MenuPopup;
            _autostart = new Autostart();
            _settings = new Settings();

            AddItem(ApplicationText.GetAppHint(), AboutMenuItem_Click);
            AddItem("-");
            AddItem("Open test page", OpenTestPageMenuItem_Click);
            AddItem("Open documentation", OpenDocsMenuItem_Click);
            AddItem("-");
            AddItem("Start app with Windows", SetupAutostartMenuItem_Click, "autostart");
            AddItem("Show notifications", SetupShowNotificationsMenuItem_Click, "show_notifications");
            AddItem("-");
            AddItem("E&xit", ExitMenuItem_Click);

            _notifyIcon = new NotifyIcon
            {
                ContextMenu = _contextMenu,
                Text = ApplicationText.GetAppHint(),
                Visible = true,
                Icon = new Icon(GetType(), "Resources.icon.ico"),
                BalloonTipIcon = ToolTipIcon.Info
            };

            EventBus.GetInstance()
                .Bind(Events.SHOW_TIP_EVENT, (s, dictionary) =>
                {
                    if (_settings.EnableNotifications)
                    {
                        var icon = dictionary["type"] == "error" ? ToolTipIcon.Error : ToolTipIcon.Info;
                        SetBalloonTip(dictionary["text"], icon);
                    }
                });
        }


        private void SetBalloonTip(string text, ToolTipIcon icon)
        {
            _notifyIcon.Visible = true;
            _notifyIcon.BalloonTipTitle = ApplicationText.GetAutostartText();
            _notifyIcon.BalloonTipText = text;
            _notifyIcon.BalloonTipIcon = icon;
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

                if (item.Name == "show_notifications")
                {
                    item.Checked = _settings.EnableNotifications;
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

        private void SetupShowNotificationsMenuItem_Click(object sender, EventArgs e)
        {
            _settings.EnableNotifications = !_settings.EnableNotifications;
            _settings.Save();
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            _notifyIcon.Visible = false;
            Application.Exit();
        }
    }
}