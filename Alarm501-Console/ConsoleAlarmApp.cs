using System;
using Alarm501_Console.Options;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alarm501_MC;

namespace Alarm501_Console
{
    public class ConsoleAlarmApp
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

        public ConsoleAlarmApp() {}
        public void Init(SendAlarmFuncWithSnoozeTime SnoozeAlarm, GetAlarmList GetAlarmsByState, SendAlarmFunc AddAlarm, SendAlarmFunc UpdateAlarm, SendAlarmFunc CheckRepeatOption, PassListOfIntFunc DeleteAlarm)
        {
            this.SnoozeAlarm = SnoozeAlarm;
            this.GetAlarmsByState = GetAlarmsByState;
            this.AddAlarm = AddAlarm;
            this.UpdateAlarm = UpdateAlarm;
            this.CheckRepeatOption = CheckRepeatOption;
            this.DeleteAlarm = DeleteAlarm;

            List<string> choices = new List<string>();
            choices.AddRange(Enum.GetValues(typeof(AlarmSound)).Cast<string>());
            choices.Add("Change Alarm Sound Menu");
            IO.TaskOptions[TaskOption.AlarmSoundChoices] = choices.ToList();
        }


        public void Start()
        {
            string TaskInput;
            while ((TaskInput = IO.GetTaskInput(TaskOption.MainMenuTasks)) != "Quit")
            {
                // IO.DisplayCurrentAlarms();
                switch (TaskInput)
                {
                    case "Add Alarm": //Future ADD Check To See If ENABLED
                        ShowAddAlarmView();
                        break;
                    case "Edit Alarm": //Future ADD Check To See If ENABLED
                        ShowEditView();
                        break;
                    case "Delete A Alarm":

                        break;

                }
                Console.WriteLine("Action DONE\n");
            }
        }

        public void ShowDeleteView()
        {

        }

        public void ShowEditView()
        {
            Alarm? alarm = ShowSelectAlarmView();
            if (alarm == null) return; //This will never run hence, the options will be disabled, later implementation.

            while(true)
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
            if(IO.GetTaskInput(TaskOption.SnoozeAlarmTasks) == "Yes") //Prompt user for the snooze period
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

        public Alarm? ShowSelectAlarmView() //This is closed till delegates are setup
        {
            if (Alarm._listOfAlarms!.Count == 0) return null;

            List<string> alarms = new();
            foreach (Alarm alarm in Alarm._listOfAlarms!) alarms.Add(alarm.AlarmTimeFormat);
            alarms.Add("Select Alarm Menu");

            IO.TaskOptions[TaskOption.SelectAlarmTasks] = alarms;

            return Alarm._listOfAlarms[Convert.ToInt32(IO.GetTaskInput(TaskOption.SelectAlarmTasks)) - 1];
        }

    }
}
