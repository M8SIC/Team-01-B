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
                    case "Add Alarm": //Future ADD Check To See If ENABLED
                        ShowAddAlarmView();
                        break;
                    case "Edit Alarm": //Future ADD Check To See If ENABLED
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
            Alarm? alarm = ShowSelectAlarmView();
            if (alarm == null) return; //This will never run hence, the options will be disabled, later implementation.

            while(true)
            {
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
                        //Add and Create Alarm

                        return;
                    case "Cancel":
                        return;
                }
            }
         
        }

        public void ShowChangeSnoozePeriodView()
        {
            snoozeTime = IO.ChangeSnoozePeriod();   
            
            //This is more than likely where you would call a delegate

        }

        public void ShowAddAlarmView()
        {
            IO.GetTaskInput("Add/EditAlarmMainTasks");
        }

        public Alarm? ShowSelectAlarmView() //This is closed till delegates are setup
        {
            if (Alarm._listOfAlarms!.Count == 0) return null;

            List<string> alarms = new();
            foreach (Alarm alarm in Alarm._listOfAlarms!) alarms.Add(alarm.AlarmTimeFormat);
            alarms.Add("Select Alarm Menu");

            IO.TaskOptions["SelectAlarmTasks"] = alarms;

            return Alarm._listOfAlarms[Convert.ToInt32(IO.GetTaskInput("SelectAlarmTasks")) - 1];
        }

    }
}
