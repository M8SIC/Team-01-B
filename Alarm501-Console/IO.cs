using Alarm501_Console.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Alarm501_Console
{
    public static class IO
    {
        public static Dictionary<TaskOption, List<string>> TaskOptions = new Dictionary<TaskOption, List<string>>() //Last element in dictionary is the location.
        {

            [TaskOption.MainMenuTasks] = new List<string>{ "Add Alarm", "Edit Alarm", "Delete A Alarm", "Quit", "Main Menu"},
            [TaskOption.Add_EditAlarmMainTasks] = new List<string>{ "Update Alarm Name", "Update Alarm Active State", "Update Alarm Time", "Update Alarm Sound", "Update Alarm Repeat Option", "Publish Alarm", "Cancel", "Add/Edit Alarm Menu", },
            [TaskOption.SelectAlarmTasks] = new List<string>{}, //Manually Refreshed.
            [TaskOption.AlarmSoundChoices] = Enum.GetValues(typeof(AlarmSound)).Cast<AlarmSound>().Select(sound => sound.ToString()).Concat(new[] { "Change Alarm Sound Menu" }).ToList(),
            [TaskOption.SetAlarmActiveState] = new List<string> {"On", "Off", "Alarm Status Update Menu" },
        };

        public static void DisplayCurrentAlarms()
        {
            Console.Clear();
            Console.WriteLine("Here Are Your Current Alarms:\n");
            foreach(Alarm alarm in Alarm._listOfAlarms!)
            {
                Console.WriteLine(alarm.AlarmTimeFormat);
            }

            Console.WriteLine("\n");
        }

        public static string GetTaskInput(TaskOption taskName)
        {
            List<string> possibleTasks = TaskOptions[taskName].ToList();

            try
            {
                Console.WriteLine($"You Are At {possibleTasks[possibleTasks.Count-1]}, Here Are Your Current Options: (1-{possibleTasks.Count - 1})");
                for (int i = 0; i < possibleTasks.Count - 1; i++) Console.WriteLine($"({i + 1}) {possibleTasks[i]}");

                int task = Convert.ToInt32(Console.ReadLine());
                if (task < 1 || task > possibleTasks.Count - 1) throw new Exception();

                return possibleTasks[task - 1].ToString();
            }
            catch (Exception e) { Console.WriteLine("Invalid Option\n"); return GetTaskInput(taskName); }
        }

        public static bool GetYesOrNoInput(string theQuestionToAsk)
        {
            try
            {
                Console.WriteLine($"{theQuestionToAsk}\n(1) Yes\n(2) No");

                int task = Convert.ToInt32(Console.ReadLine());
                if (task < 1 || task > 2) throw new Exception();

                return task == 1;
            }
            catch (Exception e) { Console.WriteLine("Invalid Option\n"); return GetYesOrNoInput(theQuestionToAsk); }
        }

        public static void Display(string txt) => Console.WriteLine(txt);
        public static DateTime GetTimeInput()
        {
            try
            {
                Console.WriteLine("Please write the time you want to change to: (hh:mm:ss) or (C)ancel");
                List<string> TimeTemp = Console.ReadLine()!.Split(":").ToList();
                if (TimeTemp[0].ToLower() == "c") return new DateTime(1);
                
                bool has3Parts = TimeTemp.Count == 3;
                bool eachPartsLengthIs2 = TimeTemp.All(PartOfATime => PartOfATime.Length == 2);
                bool hourIsWithin1and12 = Convert.ToInt32(TimeTemp[0]) > 0 && Convert.ToInt32(TimeTemp[0]) < 13;
                bool MMandSSIsWithin0and59 = TimeTemp.All(PartOfATime => PartOfATime == TimeTemp[0] || (Convert.ToInt32(PartOfATime) >= 0 && Convert.ToInt32(PartOfATime) < 60));

                if(!(has3Parts && eachPartsLengthIs2 && hourIsWithin1and12 && MMandSSIsWithin0and59)) throw new Exception();

                string AM_PM = "";
                while(AM_PM == "")
                {
                    Console.WriteLine("Please pick AM or PM: (A)M/(P)M or (C)ancel");
                    string response = Console.ReadLine()!.ToLower();
                    if (response == "c") return new DateTime(1);

                    try
                    {
                        AM_PM = (response == "am") ? "AM" : (response == "pm") ? "PM" : (response == "a") ? "AM" : (response == "p") ? "PM" : throw new Exception();
                    }
                    catch (Exception e) { Console.WriteLine("Invalid Syntax"); }
                }

                return DateTime.ParseExact($"{string.Join(":", TimeTemp)} {AM_PM}", "hh:mm:ss tt", CultureInfo.InvariantCulture);
            }
            catch (Exception e) { Console.WriteLine("Invalid Syntax\n"); return GetTimeInput(); }
        }

        public static int SetSnoozePeriod() //Renamed
        {
            try
            {
                int input = -1;
                do
                {
                    Console.Write((input != -1) ? "Invalid Snooze Range\n" : "");
                    Console.WriteLine("How long do you want to snooze your alarm in minutes (1 - 30): ");
                } while (( input = Convert.ToInt32(Console.ReadLine()) ) > 30 || input < 1);

                return input;
            }
            catch (Exception e) { Console.WriteLine("Invalid Syntax, Not A Number\n"); return SetSnoozePeriod(); }
        }

        public static string GetRepeatOptionInput()
        {
            Console.WriteLine("Choose the avaliable repeating options: \n None  (1) \n Daily (2) \n Weekly (3)");
            try
            {
                int OptionChosen = Convert.ToInt32(Console.ReadLine());
                return (OptionChosen==1) ? "None" : (OptionChosen == 2) ? "Daily" : (OptionChosen == 3) ? "Weekly" : throw new Exception();
            }
            catch (Exception e) { Console.WriteLine("Invalid Input\n"); return GetRepeatOptionInput(); }
        }

        public static AlarmSound GetAlarmSound() => Enum.Parse<AlarmSound>(GetTaskInput(TaskOption.AlarmSoundChoices));
        public static string GetAlarmName()
        {
            Console.Write("Name the Alarm: ");
            return Console.ReadLine()!;
        }

    }
}
