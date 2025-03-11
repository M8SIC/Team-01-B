using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alarm501
{
    public delegate void ParameterlessFunc();
    public delegate void PassListOfIntFunc(List<int> i);
    public delegate void SetButtonEnableTo(bool enabled);
    public delegate int GetCurrentSelectedIndex();
    public delegate BindingList<Alarm>? GetAlarmList();
    public delegate void SendAlarmFunc(Alarm alarm);
    public delegate void SendAlarmFuncWithSnoozeTime(Alarm alarm, int snoozeTime);

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

            alarm501.Init(alarmCore.SnoozeAlarm, alarmCore.GetAlarms, alarmCore.AddAlarm, alarmCore.UpdateAlarm, alarmCore.CheckRepeatOption, alarmCore.ToggleActiveState, alarmCore.DeleteAlarm, alarmCore.GetAllAlarms);
            alarmCore.Init();

            Application.Run(alarm501);
        }
    }
}
