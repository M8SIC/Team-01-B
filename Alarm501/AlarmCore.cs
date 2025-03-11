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

namespace Alarm501
{
    public class AlarmCore
    {
        private BindingList<Alarm>? _listOfAlarms = null;

        private BindingList<Alarm> _listOfActiveAlarms = new BindingList<Alarm>();
        private BindingList<Alarm> _listOfInactiveAlarms = new BindingList<Alarm>();

        private bool _activeState = true;

        private ParameterlessFunc refreshList;
        private SetButtonEnableTo setAddBtnEnableTo;
        private GetCurrentSelectedIndex getCurrentSelectedIndex;
        private SendAlarmFunc notifyAlarmRing;

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

        public BindingList<Alarm>? GetAllAlarms() => _listOfAlarms;
        public BindingList<Alarm>? GetAlarms() => (_activeState) ? _listOfActiveAlarms : _listOfInactiveAlarms;
        public void UpdateAlarm(Alarm alarm)
        {
            BindingList<Alarm>? alarmListToAddTo = (alarm.IsON) ? _listOfActiveAlarms : _listOfInactiveAlarms;
            int newIndex = getCurrentSelectedIndex();

            if (_activeState && alarmListToAddTo != _listOfActiveAlarms)
            {
                _listOfActiveAlarms.RemoveAt(getCurrentSelectedIndex());
                _listOfInactiveAlarms.Add(alarm);
                newIndex = _listOfInactiveAlarms.Count - 1;
            }
            else if(!_activeState && alarmListToAddTo == _listOfActiveAlarms)
            {
                _listOfInactiveAlarms.RemoveAt(getCurrentSelectedIndex());
                _listOfActiveAlarms.Add(alarm);
                newIndex = _listOfActiveAlarms.Count - 1;

            }
            else
            {
                alarmListToAddTo![getCurrentSelectedIndex()] = alarm;
            }
           
            SaveAlarms();
            InitAlarm(alarmListToAddTo[newIndex]);
        }
      
        public void CheckIfAlarmClockIsAtLimit()
        {
            setAddBtnEnableTo(_listOfAlarms!.Count < 5);
        }
     
        private void LoadAlarms()
        {
            if (File.Exists("alarms.txt"))
            {
                string json = File.ReadAllText("alarms.txt");

                _listOfAlarms = JsonConvert.DeserializeObject<BindingList<Alarm>>(json)!;

                if (_listOfAlarms != null)
                {
                    CheckIfAlarmClockIsAtLimit();
                    foreach (Alarm alarm in _listOfAlarms)
                    {
                        if (!alarm.IsON) _listOfInactiveAlarms.Add(alarm);
                        else _listOfActiveAlarms.Add(alarm);

                        InitAlarm(alarm);
                    }
                }
            }

            if (_listOfAlarms == null)
            {
                _listOfAlarms = new BindingList<Alarm>();
            }
        }
        private void SaveAlarms()
        {
            _listOfAlarms!.Clear();
            foreach(Alarm alarm in _listOfActiveAlarms) _listOfAlarms.Add(alarm);
            foreach (Alarm alarm in _listOfInactiveAlarms) _listOfAlarms.Add(alarm);

            File.WriteAllText("alarms.txt", JsonConvert.SerializeObject(_listOfAlarms));
        }

        public void DeleteAlarm(List<int> alarmIndex)
        {
            List<Alarm> alarmsToDelete = new List<Alarm>();
            foreach(int index in alarmIndex) { alarmsToDelete.Add(_listOfAlarms![index]); };

            foreach (Alarm alarm in alarmsToDelete)
            {
                BindingList<Alarm>? alarmListStoredIn= (alarm.IsON) ? _listOfActiveAlarms : _listOfInactiveAlarms;
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
            BindingList<Alarm>? alarmList = (alarm.IsON) ? _listOfActiveAlarms : _listOfInactiveAlarms;

            _listOfAlarms!.Add(alarm);
            alarmList!.Add(alarm);
            SaveAlarms();

            CheckIfAlarmClockIsAtLimit();
            InitAlarm(alarm);
            refreshList();
        }

    }
}
