using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Newtonsoft.Json;
using System.Threading;
using System.Collections.Concurrent;
using System.Security.Claims;


namespace Alarm501_GUI
{
    public partial class Alarm501 : Form
    {
        #region Fields/Property/Events
        private SendAlarmFuncWithSnoozeTime SnoozeAlarm;

        private GetAlarmList GetAlarmsByState;

        private SendAlarmFunc AddAlarm;
        private SendAlarmFunc UpdateAlarm;
        private SendAlarmFunc CheckRepeatOption;
        private PassListOfIntFunc DeleteAlarm;

        private ParameterlessFunc toggleActiveState;
        #endregion

        #region Constructor/Methods
        public Alarm501()
        {
            InitializeComponent();
            UpdateEditButtonStatus(null!, null!);
        }

        public void Init(SendAlarmFuncWithSnoozeTime SnoozeAlarm, GetAlarmList GetAlarmsByState, SendAlarmFunc AddAlarm, SendAlarmFunc UpdateAlarm, SendAlarmFunc CheckRepeatOption, ParameterlessFunc ToggleActiveState, PassListOfIntFunc DeleteAlarm)
        {
            this.SnoozeAlarm = SnoozeAlarm;
            this.GetAlarmsByState = GetAlarmsByState;
            this.AddAlarm = AddAlarm;
            this.UpdateAlarm = UpdateAlarm;
            this.CheckRepeatOption = CheckRepeatOption;
            this.toggleActiveState = ToggleActiveState;
            this.DeleteAlarm = DeleteAlarm;
        }

        public int GetCurrentSelectedIndex() => UxAlarmList.SelectedIndex;
        public void SetAddBtnEnableTo(bool Enabled)
        {
            UxAddBtn.Enabled = Enabled;
        }
        public void UpdateEditButtonStatus(object sender, EventArgs e)
        {
            UxEditBtn.Enabled = (UxAlarmList.SelectedIndex != -1);
        }

        private void OnEditButtonClick(object sender, EventArgs e)
        {
            AddEditAlarm addEditAlarm = new AddEditAlarm(GetAlarmsByState()![UxAlarmList.SelectedIndex], AddAlarm, UpdateAlarm);
            addEditAlarm.ShowDialog();
        }

        public void NotifyAlarmRing(Alarm alarm)
        {
            DialogResult AlarmRingNotify = MessageBox.Show($"Sound: {alarm.AlarmSound}\n\nDo you want to snooze?", $"Your {alarm.AlarmName} Alarm Just Rang!", MessageBoxButtons.YesNo);
            if (AlarmRingNotify == DialogResult.Yes)
            {
                SnoozeAlarm(alarm, (int)uxSnoozeTime.Value);
            }
            else
            {
                CheckRepeatOption(alarm);
            }
        }

        public void RefreshList()
        {
            UxAlarmList.DataSource = null;
            UxAlarmList.DataSource = GetAlarmsByState();
            UxAlarmList.DisplayMember = "AlarmTimeFormat";
        }

        private void UxAddBtn_Click(object sender, EventArgs e)
        {
            AddEditAlarm addEditAlarm = new AddEditAlarm(null, AddAlarm, UpdateAlarm);
            addEditAlarm.ShowDialog();
        }

        private void UxChangeActiveView_Click(object sender, EventArgs e)
        {
            uxActiveViewLabel.Text = (uxActiveViewLabel.Text == "Active") ? "Inactive" : "Active";
            uxChangeActiveView.Text = (uxChangeActiveView.Text == "Show Active") ? "Show Inactive" : "Show Active";

            toggleActiveState();

            RefreshList();
        }

        private void uxDeleteBtn_Click(object sender, EventArgs e)
        {
            MultiDelete multiDeleteMenu = new MultiDelete(Alarm._listOfAlarms!, DeleteAlarm);
            multiDeleteMenu.ShowDialog();
        }

        #endregion
    }
}
