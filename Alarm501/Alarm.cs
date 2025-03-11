﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Timers;
using Newtonsoft.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using System.ComponentModel;

namespace Alarm501
{
    public class Alarm : EventArgs, INotifyPropertyChanged
    {
        #region Fields/Property/Events
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string RepeatOption = "None";
        public DateTime DefaultTime {  get; set; }

        private string _alarmName;
        public string AlarmName
        {
            get => _alarmName;
            set
            {
                _alarmName = value;
                OnPropertyChanged(nameof(AlarmName));
                OnPropertyChanged(nameof(AlarmTimeFormat));
            }
        }

        private DateTime _alarmDateTime;
        public DateTime AlarmDateTime {
            get => _alarmDateTime;
            set
            {
                _alarmDateTime = value;
                OnPropertyChanged(nameof(AlarmDateTime));
                OnPropertyChanged(nameof(AlarmTimeFormat));
            }
        }

        private bool _isON;
        public bool IsON { 
            get => _isON;
            set
            {
                _isON = value;
                OnPropertyChanged(nameof(IsON));
                OnPropertyChanged(nameof(AlarmTimeFormat));
            }
        }

        private AlarmSound _alarmSound;
        public AlarmSound AlarmSound
        {
            get => _alarmSound;
            set
            {
                _alarmSound = value;
                OnPropertyChanged(nameof(AlarmSound));
            }
        }
        #endregion

        #region Methods
        public Alarm(DateTime dateTime, bool isOn, AlarmSound alarmSound, string alarmName, string repeatOption) {
            DefaultTime = dateTime;
            AlarmDateTime = dateTime;
            IsON = isOn;
            AlarmSound = alarmSound;
            AlarmName = alarmName;
            RepeatOption = repeatOption;
        }

        public string AlarmTimeFormat
        {
            get
            {
                string isOnText = (IsON) ? "On" : "Off";

                return $"{AlarmName}   " + AlarmDateTime.ToString($"hh:mm tt ") + isOnText;
            }
        }
        #endregion
    }
}
