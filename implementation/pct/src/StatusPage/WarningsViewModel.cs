using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using com.vanderlande.wpf;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Forms;

namespace Your
{
    class WarningsViewModel : ContentViewModel
    {
        private bool alarm = false;
        public bool Alarm
        {
            get { return alarm; }
            set
            {
                if (ChangeProperty(ref alarm, value) == true)
                    UpdateStatusBar();
            }
        }


        private bool warning = false;
        public bool Warning
        {
            get { return warning; }
            set
            {
                if (ChangeProperty(ref warning, value) == true)
                    UpdateStatusBar();
            }
        }


        private bool info = false;
        public bool Info
        {
            get { return info; }
            set
            {
                if (ChangeProperty(ref info, value) == true)
                    UpdateStatusBar();
            }
        }

        public override void OnCreated()
        {
            base.OnCreated();
            YourApplication.OnAlarm += OnAlarm;
            YourApplication.OnWarning += OnWarning;
            YourApplication.OnInfo += OnInfo;
        }


        public override void OnDestroy()
        {
            YourApplication.OnAlarm -= OnAlarm;
            YourApplication.OnWarning -= OnWarning;
            YourApplication.OnInfo -= OnInfo;
            Alarm = false;
            Warning = false;
            Info = false;
            base.OnDestroy();
        }


        private void OnAlarm()
        {
            MessageBox.Show("Alarm");
        }

        private void OnInfo()
        {
            MessageBox.Show("Info");
        }

        private void OnWarning()
        {
            MessageBox.Show("Warning");
        }

        private void UpdateStatusBar()
        {
            YourApplication.Alarm.IsVisible = Alarm;
            YourApplication.Warning.IsVisible = Warning;
            YourApplication.Information.IsVisible = Info;
        }

    }
}
