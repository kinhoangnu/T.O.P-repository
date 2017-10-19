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
        private bool _alarm = false;
        public bool Alarm
        {
            get { return _alarm; }
            set
            {
                if (ChangeProperty(ref _alarm, value) == true)
                    UpdateStatusBar();
            }
        }


        private bool _warning = false;
        public bool Warning
        {
            get { return _warning; }
            set
            {
                if (ChangeProperty(ref _warning, value) == true)
                    UpdateStatusBar();
            }
        }


        private bool _info = false;
        public bool Info
        {
            get { return _info; }
            set
            {
                if (ChangeProperty(ref _info, value) == true)
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
