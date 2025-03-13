using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alarm501_Console
{
    public class ConsoleAlarmApp
    {
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
                    case "Select A Alarm":
                      //  ShowSelectAlarmView();
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

        }

        public void ShowChangeSnoozePeriodView()
        {

        }

        public void ShowAddAlarmView()
        {
            IO.GetTaskInput("AddAlarmMainTasks");
        }

        public void ShowSelectAlarmView()
        {

            List<string> alarms = new();
            foreach (Alarm alarm in Alarm._listOfAlarms!) alarms.Add(alarm.AlarmTimeFormat);
            alarms.Add("Select Alarm Menu");

            IO.GetTaskInput("SelectAlarmTasks");
        }

    }
}
