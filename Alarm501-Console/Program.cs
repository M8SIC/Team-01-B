using Alarm501_MC;
using System.ComponentModel;

namespace Alarm501_Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ConsoleAlarmApp app = new ConsoleAlarmApp();
            AlarmCore alarmCore = new AlarmCore(IO.DisplayCurrentAlarms, app.SetAddOptionEnabledTo, app.getCurrentSelectedIndex, app.ShowAlarmRingView);
            app.Init(alarmCore.SnoozeAlarm, alarmCore.GetAlarmsByState, alarmCore.AddAlarm, alarmCore.UpdateAlarm, alarmCore.CheckRepeatOption, alarmCore.ToggleActiveState, alarmCore.DeleteAlarm);
            alarmCore.Init();
            
            app.Start();

        }
    }
}