using System.ComponentModel;

namespace Alarm501_Console
{
    public delegate void ParameterlessFunc();
    public delegate void PassListOfIntFunc(List<int> i);
    public delegate void SetButtonEnableTo(bool enabled);
    public delegate int GetCurrentSelectedIndex();
    public delegate BindingList<Alarm>? GetAlarmList();
    public delegate void SendAlarmFunc(Alarm alarm);
    public delegate void SendAlarmFuncWithSnoozeTime(Alarm alarm, int snoozeTime);
    public class Program
    {
        public static void Main(string[] args)
        {
            ConsoleAlarmApp app = new ConsoleAlarmApp();
            app.Start();
        }
    }
}