﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alarm501_GUI
{
    public partial class AddEditAlarm : Form
    {
        #region Fields/Property/Events
        private bool _isInEdit;
        private SendAlarmFunc AddAlarm;
        private SendAlarmFunc UpdateAlarm;
        #endregion

        #region Constructor/Methods
        public AddEditAlarm(Alarm? alarm, SendAlarmFunc AddAlarm, SendAlarmFunc UpdateAlarm)
        {
            InitializeComponent();
            _isInEdit = (alarm != null) ? true : false;

            this.AddAlarm = AddAlarm;
            this.UpdateAlarm = UpdateAlarm;

            if (alarm == null) return;
            uxName.Text = alarm.AlarmName;
            isOnCheckBox.Checked = alarm.IsON;
            dateTimePicker1.Value = alarm.DefaultTime;

            uxNone.Checked = alarm.RepeatOption == "None";
            uxDaily.Checked = alarm.RepeatOption == "Daily";
            uxWeekly.Checked = alarm.RepeatOption == "Weekly";

            cmbSounds.SelectedItem = alarm.AlarmSound;
        }

        private string GetChosenRepeatOption()
        {
            if (uxNone.Checked) return "None";
            if (uxDaily.Checked) return "Daily";
            return "Weekly";
        }

        private void OnAddAlarm(object sender, EventArgs e)
        {
            Alarm alarm = new Alarm(dateTimePicker1.Value, isOnCheckBox.Checked, (AlarmSound)cmbSounds.SelectedItem!, uxName.Text, GetChosenRepeatOption());

            if (!_isInEdit)
            {
                AddAlarm(alarm);
            }
            else
            {
                UpdateAlarm(alarm);
            }

            this.Close();
        }
        private void OnCancel(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

    }
}
