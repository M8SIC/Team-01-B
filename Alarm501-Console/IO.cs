using Alarm501_Console.Options;
using System;
using System.Collections.Generic;
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
            [TaskOption.SnoozeAlarmTasks] = new List<string> {"Yes", "No", "Snooze Alarm Menu"},
            [TaskOption.AlarmSoundChoices] = new List<string> {}, //ON INIT
            [TaskOption.SetAlarmActiveState] = new List<string> {"On", "Off", "Alarm Status Update Menu" },
            [TaskOption.DeleteAnotherAlarmQuestion] = new List<string>{"Yes", "No", "Delete Alarm Menu"},
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
            List<string> possibleTasks = TaskOptions[taskName];

            try
            {
                //Console.Clear();
                Console.WriteLine($"You Are At {possibleTasks[possibleTasks.Count-1]}, Here Are Your Current Options: (1-{possibleTasks.Count - 1})");
                for (int i = 0; i < possibleTasks.Count - 1; i++) Console.WriteLine($"({i + 1}) {possibleTasks[i]}");

                int task = Convert.ToInt32(Console.ReadLine());
                if (task < 1 || task > possibleTasks.Count - 1) throw new Exception();

                return possibleTasks[task - 1].ToString();
            }
            catch (Exception e) { Console.WriteLine("Invalid Option\n"); return GetTaskInput(taskName); }
        }


        public static DateTime GetTimeInput() //Not Finished
        {
            //Console.Clear();
            try
            {
                Console.WriteLine("Please write the time you want to change to: (hh:mm:ss)");
                List<string> TimeTemp = Console.ReadLine()!.Split(":").ToList();

                bool has3Parts = TimeTemp.Count == 3;
                bool eachPartsLengthIs2 = TimeTemp.All(PartOfATime => PartOfATime.Length == 2);
                bool hourIsWithin1and12 = Convert.ToInt32(TimeTemp[0]) > 0 && Convert.ToInt32(TimeTemp[0]) < 13;
                bool MMandSSIsWithin0and59 = TimeTemp.All(PartOfATime => PartOfATime == TimeTemp[0] || (Convert.ToInt32(PartOfATime) >= 0 && Convert.ToInt32(PartOfATime) < 60));

                if(!(has3Parts && eachPartsLengthIs2 && hourIsWithin1and12 && MMandSSIsWithin0and59)) throw new Exception();

                string AM_PM = "";
                while(AM_PM == "")
                {
                    Console.WriteLine("Please pick AM or PM: (A)M/(P)M");
                    string response = Console.ReadLine()!.ToLower();
                    try
                    {
                        AM_PM = (response == "a") ? "AM" : (response == "b") ? "PM" : throw new Exception();
                    }
                    catch (Exception e) { Console.WriteLine("Invalid Syntax"); }
                }

                return DateTime.ParseExact($"{string.Join(":", TimeTemp)} {AM_PM}", "hh:mm:ss tt", CultureInfo.InvariantCulture);
            }
            catch (Exception e) { Console.WriteLine("Invalid Syntax\n"); return GetTimeInput(); }
        }

        public static int ChangeSnoozePeriod()
        {
            try
            {
                //Console.Clear();
                Console.WriteLine("Change the current snooze alarm in minutes (1 - 30): ");
                int input = Convert.ToInt32(Console.ReadLine());
                while (input < 1 || input > 30)
                {
                    Console.WriteLine("Change the current snooze alarm in minutes (1 - 30): ");
                    input = Convert.ToInt32(Console.ReadLine());
                }
                return input;
            }
            catch (Exception e) { Console.WriteLine("Please enter a valid snooze period in minutes\n"); return ChangeSnoozePeriod(); }
        }

        public static string GetRepeatOptionInput()
        {
            Console.Clear();
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
            Console.WriteLine("Name the Alarm: ");
            return Console.ReadLine()!;
        }

        public static string GetDeleteAnotherAlarmResponse()
        {
            Console.WriteLine("Do you want to delete another alarm?");
            return IO.GetTaskInput(TaskOption.DeleteAnotherAlarmQuestion);
        }

        /*public static List<int> DeleteAlarm()
        {

        }*/


    }
}
