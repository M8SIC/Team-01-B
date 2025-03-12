using Alarm501_GUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alarm501_GUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Alarm501 alarm501 = new Alarm501();
            AlarmCore alarmCore = new AlarmCore(alarm501.RefreshList, alarm501.SetAddBtnEnableTo, alarm501.GetCurrentSelectedIndex, alarm501.NotifyAlarmRing);

            alarm501.Init(alarmCore.SnoozeAlarm, alarmCore.GetAlarmsByState, alarmCore.AddAlarm, alarmCore.UpdateAlarm, alarmCore.CheckRepeatOption, alarmCore.ToggleActiveState, alarmCore.DeleteAlarm);
            alarmCore.Init();

            Application.Run(alarm501);
        }
    }
}
