using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alarm501_Console
{
    public class ConsoleAlarmApp
    {
        #region Fields/Property/Events
        private int snoozeTime;

        #endregion

        public ConsoleAlarmApp() { }
        public void Start()
        {
            while (true)
            {
             // IO.DisplayCurrentAlarms();
                switch (IO.GetTaskInput("MainMenuTasks"))
                {
                    case "Add Alarm":
                        ShowAddAlarmView();
                        break;
                    case "Edit Alarm":
                        ShowEditView();
                        break;
                    case "Change Snooze Period":
                        ShowChangeSnoozePeriodView();
                        break;
                    case "Quit":
                        Environment.Exit(0);
                        break;
                }
                Console.WriteLine("Action DONE\n");
            }
        }

        public void ShowEditView()
        {
            Alarm alarm = ShowSelectAlarmView();

            switch (IO.GetTaskInput("Add/EditAlarmMainTasks"))
            {
                case "Change Time":
                    alarm.AlarmDateTime = IO.GetTimeInput();
                    break;
                case "Change Repeat":
                    alarm.RepeatOption = IO.GetRepeatOptionInput();
                    break;
                case "Change Sound":
                    break;
                case "Create Alarm":
                    break;
                case "Cancel":
                    break;

            }
        }

        public void ShowChangeSnoozePeriodView()
        {
            snoozeTime = IO.ChangeSnoozePeriod();         

        }

        public void ShowAddAlarmView()
        {
            IO.GetTaskInput("AddAlarmMainTasks");
        }

        public Alarm ShowSelectAlarmView()
        {

            List<string> alarms = new();
            foreach (Alarm alarm in Alarm._listOfAlarms!) alarms.Add(alarm.AlarmTimeFormat);
            alarms.Add("Select Alarm Menu");

            IO.TaskOptions["SelectAlarmTasks"] = alarms;

            return Alarm._listOfAlarms[Convert.ToInt32(IO.GetTaskInput("SelectAlarmTasks"))-1];
        }

    }
}
