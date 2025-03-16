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
        private int alarmRungIndex = -1;
        private int lastSelectedIndex = -1; //return from universal set of ALARMS

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
                    case "Take Action On The Alarm That Just Rang":
                        ShowAlarmRingView();
                        break;
                    case "Add Alarm": 
                        ShowAddAlarmView();
                        break;
                    case "Edit Alarm":
                        ShowEditView();
                        break;
                    case "Delete A Alarm":
                        ShowDeleteView();
                        break;
                }

                UpdateSnoozeOption();
            }
        }

        public void SetAddOptionEnabledTo(bool enabled){} //THIS IS A PLACE HOLDER SINCE CONSOLE DONT HAVE "BUTTON"

        public void ShowDeleteView()
        {
            do
            {
                int AlarmIndexSelected = ShowSelectAlarmView();
                if (AlarmIndexSelected == -1) { IO.Display("You can't delete anything, no alarms exist."); return; }
                if (AlarmIndexSelected == -2) { IO.DisplayCurrentAlarms(); return; } //-2 is cancel

                DeleteAlarm(new List<int>() { AlarmIndexSelected });
                alarmRungIndex = (AlarmIndexSelected == alarmRungIndex) ? -1 : (AlarmIndexSelected < alarmRungIndex) ? alarmRungIndex-1 : alarmRungIndex;

                IO.DisplayCurrentAlarms();

            } while (Alarm._listOfAlarms!.Count != 0 && IO.GetYesOrNoInput("Do you want to delete another alarm?"));

        }

        public void ShowEditView()
        {

            int AlarmIndexSelected = ShowSelectAlarmView();
            if (AlarmIndexSelected == -1) { IO.Display("You can't edit, no alarms exist."); return; }
            if (AlarmIndexSelected == -2) { IO.DisplayCurrentAlarms(); return; } //-2 is cancel

            Alarm? alarm = Alarm._listOfAlarms![AlarmIndexSelected];

            if(alarm.IsON && GetAlarmsByState() != Alarm._listOfActiveAlarms) toggleActiveState();
            if(!alarm.IsON && GetAlarmsByState() != Alarm._listOfInactiveAlarms) toggleActiveState(); //UMMM pls ignore cuz this fixes the extra credit issue.

            while (true)
            {
                switch (IO.GetTaskInput(TaskOption.Add_EditAlarmMainTasks))
                {
                    case "Update Alarm Time":
                        DateTime timeToSet = IO.GetTimeInput();
                        if (timeToSet.Ticks == 1) break;

                        alarm.AlarmDateTime = timeToSet;
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

                        IO.DisplayCurrentAlarms();
                        return;
                }
            }
         
        }
        public int ShowSetSnoozePeriodView() => IO.SetSnoozePeriod(); //Method Renamed

        public void ShowAlarmRingView() // Task-N/A when alarmRungIndex is -1
        {
            Alarm alarm = Alarm._listOfAlarms![alarmRungIndex];
            alarmRungIndex = -1;
            UpdateSnoozeOption();

            if (IO.GetYesOrNoInput($"Do you want to snooze your {alarm.AlarmName} alarm.")){
                SnoozeAlarm(alarm, ShowSetSnoozePeriodView()); //Prompt user for the snooze period
            }
            else //Go based on the alarms repeat option
            {
                CheckRepeatOption(alarm);
            }
        }

        public void UpdateSnoozeOption()
        {
            if(alarmRungIndex != -1){
                Alarm alarm = Alarm._listOfAlarms![alarmRungIndex];
                IO.TaskOptions[TaskOption.MainMenuTasks] = new List<string> { "Take Action On The Alarm That Just Rang", "Add Alarm", "Edit Alarm", "Delete A Alarm", "Quit", "Main Menu" };
            }
            else
            {
                IO.TaskOptions[TaskOption.MainMenuTasks] = new List<string> { "Add Alarm", "Edit Alarm", "Delete A Alarm", "Quit", "Main Menu" };
            }
          
        }
        public void NotifyAlarmRingView(Alarm alarm) //Display that a alarm ringed
        {
            alarmRungIndex = Alarm._listOfAlarms!.IndexOf(alarm);

            Console.WriteLine("------------");
            Console.WriteLine($"Your {alarm.AlarmName} Alarm Just Went Off!\nPlaying {alarm.AlarmSound} Sound\nGo Back To Menu/Refresh To Take Action!");
            Console.WriteLine("------------");

            UpdateSnoozeOption();
        }
        public void ShowAddAlarmView()
        {
            if(Alarm._listOfAlarms!.Count >= 5) { IO.Display("You can't add more alarms, 5 is the max."); return; }

            DateTime dateTime = DateTime.Now.AddSeconds(10);
            bool isOn = true;
            AlarmSound alarmSound = AlarmSound.Radar;
            string alarmName = "No Name";
            string repeatOption = "None";

            while (true)
            {
                switch (IO.GetTaskInput(TaskOption.Add_EditAlarmMainTasks))
                {
                    case "Update Alarm Time":
                        DateTime timeToSet = IO.GetTimeInput();
                        if (timeToSet.Ticks == 1) break;

                        dateTime = timeToSet;
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
                        IO.DisplayCurrentAlarms();
                        return;
                }
            }
        }

        public int ShowSelectAlarmView()
        {
            if (Alarm._listOfAlarms!.Count == 0) return -1;
            IO.TaskOptions[TaskOption.SelectAlarmTasks] = Alarm._listOfAlarms.Select(alarm => alarm.AlarmTimeFormat).Concat(new[] { "Cancel", "Select Alarm Menu" }).ToList();

            lastSelectedIndex = IO.TaskOptions[TaskOption.SelectAlarmTasks].IndexOf(IO.GetTaskInput(TaskOption.SelectAlarmTasks));
            return (lastSelectedIndex == Alarm._listOfAlarms!.Count) ? -2: lastSelectedIndex;
        }

    }
}
