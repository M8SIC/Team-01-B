using System;
using Alarm501_Console.Options;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alarm501_MC;
using System.Diagnostics;

namespace Alarm501_Console
{
    public class ConsoleAlarmApp
    {
        #region Fields/Property/Events
        private bool alarmRanged = false;
        private int lastSelectedIndex = -1; //return from universal set of ALARMS, get the indexof inside of inactive.

        private SendAlarmFuncWithSnoozeTime SnoozeAlarm;
        private GetAlarmList GetAlarmsByState;

        private SendAlarmFunc AddAlarm;
        private SendAlarmFunc UpdateAlarm;
        private SendAlarmFunc CheckRepeatOption;
        private PassListOfIntFunc DeleteAlarm;

        private ParameterlessFunc toggleActiveState;
        #endregion

        public ConsoleAlarmApp() {}
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

        public int getCurrentSelectedIndex() => (Alarm._listOfActiveAlarms.IndexOf(Alarm._listOfAlarms![lastSelectedIndex]) != -1) ? 
            Alarm._listOfActiveAlarms.IndexOf(Alarm._listOfAlarms![lastSelectedIndex]) : 
            Alarm._listOfInactiveAlarms.IndexOf(Alarm._listOfAlarms![lastSelectedIndex]);

        public void Start()
        {
            string TaskInput;
            while ((TaskInput = IO.GetTaskInput(TaskOption.MainMenuTasks)) != "Quit")
            {
                IO.DisplayCurrentAlarms();
                switch (TaskInput)
                {
                    case "Add Alarm": //Future ADD Check To See If ENABLED
                        ShowAddAlarmView();
                        break;
                    case "Edit Alarm": //Future ADD Check To See If ENABLED
                        ShowEditView();
                        break;
                    case "Delete A Alarm":
                        ShowDeleteView();
                        break;

                }
            }
        }

        public void SetAddOptionEnabledTo(bool enabled){}

        public void ShowDeleteView()
        {
            do
            {
                int AlarmIndexSelected = ShowSelectAlarmView();
                if (AlarmIndexSelected == -1) { IO.Display("You can't delete anything, no alarms exist."); return; }

                DeleteAlarm(new List<int>() { AlarmIndexSelected });
                IO.DisplayCurrentAlarms();

            } while (Alarm._listOfAlarms!.Count != 0 && IO.GetYesOrNoInput("Do you want to delete another alarm?"));

        }

        public void ShowEditView()
        {

            int AlarmIndexSelected = ShowSelectAlarmView();
            if (AlarmIndexSelected == -1) { IO.Display("You can't edit, no alarms exist."); return; }

            Alarm? alarm = Alarm._listOfAlarms![AlarmIndexSelected];

            if(alarm.IsON && GetAlarmsByState() != Alarm._listOfActiveAlarms) toggleActiveState();
            if(!alarm.IsON && GetAlarmsByState() != Alarm._listOfInactiveAlarms) toggleActiveState(); //UMMM pls ignore cuz this fixes the extra credit issue.

            while (true)
            {
                switch (IO.GetTaskInput(TaskOption.Add_EditAlarmMainTasks))
                {
                    case "Update Alarm Time":
                        alarm.AlarmDateTime = IO.GetTimeInput();
                        break;
                    case "Update Alarm Repeat Option":
                        alarm.RepeatOption = IO.GetRepeatOptionInput();
                        break;
                    case "Update Alarm Sound":
                        alarm.AlarmSound = IO.GetAlarmSound();
                        break;
                    case "Update Alarm Name":
                        alarm.AlarmName = IO.GetAlarmName();
                        break;
                    case "Update Alarm Active State":
                        alarm.IsON = (IO.GetTaskInput(TaskOption.SetAlarmActiveState) == "On") ? true : false;
                        break;
                    case "Publish Alarm":
                        UpdateAlarm(alarm);

                        IO.DisplayCurrentAlarms();
                        return;
                    case "Cancel":
                        return;
                }
            }
         
        }

        public int ShowChangeSnoozePeriodView()
        {
            return IO.ChangeSnoozePeriod();
        }

        public void ShowAlarmRingView(Alarm alarm)
        {
            //Implement the NEW CONSOLE HERE

            if (IO.GetYesOrNoInput($"{alarm} just went off.\nDo you want to snooze your alarm.")) //Prompt user for the snooze period
            {
                SnoozeAlarm(alarm, ShowChangeSnoozePeriodView());
            }
            else //Go based on the alarms repeat option
            {
                CheckRepeatOption(alarm);
            }
        }

        public void ShowAddAlarmView()
        {
            if(Alarm._listOfAlarms!.Count >= 5) { IO.Display("You can't add more alarms, 5 is the max."); return; }

            DateTime dateTime = DateTime.Now;
            bool isOn = false;
            AlarmSound alarmSound = AlarmSound.Radar;
            string alarmName = "N/A";
            string repeatOption = "None";

            while (true)
            {
                switch (IO.GetTaskInput(TaskOption.Add_EditAlarmMainTasks))
                {
                    case "Update Alarm Time":
                        dateTime = IO.GetTimeInput();
                        break;
                    case "Update Alarm Repeat Option":
                        repeatOption = IO.GetRepeatOptionInput();
                        break;
                    case "Update Alarm Sound":
                        alarmSound = IO.GetAlarmSound();
                        break;
                    
                    case "Update Alarm Name":
                        alarmName = IO.GetAlarmName();
                        break;
                    case "Update Alarm Active State":
                        isOn = (IO.GetTaskInput(TaskOption.SetAlarmActiveState) == "On") ? true :false ;
                        break;

                    case "Publish Alarm":
                        Alarm? alarm = new Alarm(dateTime, isOn, alarmSound, alarmName, repeatOption);
                        AddAlarm(alarm);

                        return;
                    case "Cancel":
                        return;
                }
            }
        }

        public int ShowSelectAlarmView() //This is closed till delegates are setup
        {
            if (Alarm._listOfAlarms!.Count == 0) return -1;
            IO.TaskOptions[TaskOption.SelectAlarmTasks] = Alarm._listOfAlarms.Select(alarm => alarm.AlarmTimeFormat).Concat(new[] { "Select Alarm Menu" }).ToList();

            return lastSelectedIndex = IO.TaskOptions[TaskOption.SelectAlarmTasks].IndexOf(IO.GetTaskInput(TaskOption.SelectAlarmTasks));
        }

    }
}
