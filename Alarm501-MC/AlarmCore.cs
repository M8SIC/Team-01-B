using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Alarm501_MC
{
    public class AlarmCore
    {
        #region Fields/Property/Events
        private bool _activeState = true;

        private ParameterlessFunc refreshList;
        private SetButtonEnableTo setAddBtnEnableTo;
        private GetCurrentSelectedIndex getCurrentSelectedIndex;
        private SendAlarmFunc notifyAlarmRing;
        #endregion

        #region Constructor/Methods
        public AlarmCore(ParameterlessFunc refreshList, SetButtonEnableTo setAddBtnEnableTo, GetCurrentSelectedIndex getCurrentSelectedIndex, SendAlarmFunc notifyAlarmRing)
        {
            this.refreshList = refreshList;
            this.setAddBtnEnableTo = setAddBtnEnableTo;
            this.getCurrentSelectedIndex = getCurrentSelectedIndex;
            this.notifyAlarmRing = notifyAlarmRing;
        }

        public void Init()
        {
            LoadAlarms();
            refreshList();
        }

        public void ToggleActiveState() => _activeState = !_activeState;

        public BindingList<Alarm>? GetAlarmsByState() => (_activeState) ? Alarm._listOfActiveAlarms : Alarm._listOfInactiveAlarms;
        public void UpdateAlarm(Alarm alarm)
        {
            BindingList<Alarm>? alarmListToAddTo = (alarm.IsON) ? Alarm._listOfActiveAlarms : Alarm._listOfInactiveAlarms;

            if (_activeState && alarmListToAddTo != Alarm._listOfActiveAlarms)
            {
                Alarm._listOfActiveAlarms.RemoveAt(getCurrentSelectedIndex());
                Alarm._listOfInactiveAlarms.Add(alarm);
            }
            else if(!_activeState && alarmListToAddTo == Alarm._listOfActiveAlarms)
            {
                Alarm._listOfInactiveAlarms.RemoveAt(getCurrentSelectedIndex());
                Alarm._listOfActiveAlarms.Add(alarm);
            }
            else
            {
                int b = getCurrentSelectedIndex();
                alarmListToAddTo![getCurrentSelectedIndex()] = alarm;
            }
           
            SaveAlarms();
            InitAlarm(alarm);
        }
      
        public void CheckIfAlarmClockIsAtLimit()
        {
            setAddBtnEnableTo(Alarm._listOfAlarms!.Count < 5);
        }
     
        private void LoadAlarms()
        {
            if (File.Exists("alarms.txt"))
            {
                string json = File.ReadAllText("alarms.txt");

                Alarm._listOfAlarms = JsonConvert.DeserializeObject<BindingList<Alarm>>(json)!;

                if (Alarm._listOfAlarms != null)
                {
                    CheckIfAlarmClockIsAtLimit();
                    foreach (Alarm alarm in Alarm._listOfAlarms)
                    {
                        if (!alarm.IsON) Alarm._listOfInactiveAlarms.Add(alarm);
                        else Alarm._listOfActiveAlarms.Add(alarm);

                        InitAlarm(alarm);
                    }
                }
            }

            if (Alarm._listOfAlarms == null)
            {
                Alarm._listOfAlarms = new BindingList<Alarm>();
            }
        }
        private void SaveAlarms()
        {
            Alarm._listOfAlarms!.Clear();
            foreach(Alarm alarm in Alarm._listOfActiveAlarms) Alarm._listOfAlarms.Add(alarm);
            foreach (Alarm alarm in Alarm._listOfInactiveAlarms) Alarm._listOfAlarms.Add(alarm);

            File.WriteAllText("alarms.txt", JsonConvert.SerializeObject(Alarm._listOfAlarms));
        }

        public void DeleteAlarm(List<int> alarmIndex)
        {
            List<Alarm> alarmsToDelete = new List<Alarm>();
            foreach(int index in alarmIndex) { alarmsToDelete.Add(Alarm._listOfAlarms![index]); };

            foreach (Alarm alarm in alarmsToDelete)
            {
                BindingList<Alarm>? alarmListStoredIn= (alarm.IsON) ? Alarm._listOfActiveAlarms : Alarm._listOfInactiveAlarms;
                alarmListStoredIn.Remove(alarm);
            }
         
            SaveAlarms();
        }

        public void SnoozeAlarm(Alarm alarm, int snoozeTime)
        {
            alarm.AlarmDateTime = DateTime.Now.AddMinutes(snoozeTime);
            InitAlarm(alarm);
        }

        public void CheckRepeatOption(Alarm alarm)
        {
            if (alarm.RepeatOption == "None") return;

            int daysToAdd = (alarm.RepeatOption == "Daily") ? 1 : 7;

            alarm.DefaultTime = alarm.DefaultTime.AddDays(daysToAdd);
            alarm.AlarmDateTime = alarm.DefaultTime;
            InitAlarm(alarm);
        }

        public bool InitAlarm(Alarm alarm)
        {
            double deltaTime = (alarm.AlarmDateTime - DateTime.Now).TotalMilliseconds;
            if (deltaTime < 0 || !alarm.IsON) return false;

            System.Timers.Timer alarmTimer = new System.Timers.Timer(deltaTime);
            alarmTimer.Elapsed += (sender, e) => notifyAlarmRing(alarm);
            alarmTimer.AutoReset = false;
            alarmTimer.Start();

            return true;
        }

        public void AddAlarm(Alarm alarm)
        {
            BindingList<Alarm>? alarmList = (alarm.IsON) ? Alarm._listOfActiveAlarms : Alarm._listOfInactiveAlarms;

            Alarm._listOfAlarms!.Add(alarm);
            alarmList!.Add(alarm);
            SaveAlarms();

            CheckIfAlarmClockIsAtLimit();
            InitAlarm(alarm);
            refreshList();
        }
        #endregion
    }
}
