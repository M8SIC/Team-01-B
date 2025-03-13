using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alarm501_Console
{
    public static class IO
    {
        public static Dictionary<string, List<string>> TaskOptions = new Dictionary<string, List<string>>() //Last element in dictionary is the location.
        {
            ["MainMenuTasks"] =  new List<string>{ "Add Alarm", "Edit Alarm", "Select A Alarm", "Change Snooze Period", "Quit", "Main Menu"},
            ["Add/EditAlarmMainTasks"] = new List<string>{ "Change Time", "Change Repeat", "Change Sound", "Create Alarm", "Cancel", "Add/Edit Alarm Menu"},
            ["SelectAlarmTasks"] = new List<string>{}, //Manually Refreshed.
            ["SnoozeAlarmTasks"] = new List<string> {"Yes", "No", "Snooze Alarm Menu"},

        };

        public static void DisplayCurrentAlarms()
        {
            Console.WriteLine("Here Are Your Current Alarms:\n");
            
            foreach(Alarm alarm in Alarm._listOfAlarms!)
            {
                Console.WriteLine(alarm.AlarmTimeFormat);
            }

            Console.WriteLine("\n");
        }

        public static string GetTaskInput(string taskName)
        {
            List<string> possibleTasks = TaskOptions[taskName];

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


        public static DateTime GetTimeInput() //Not Finished
        {
            Console.WriteLine("Please write the time you want to change to: (hh:mm:ss)\n");
            try
            {
                List<string> TimeTemp = Console.ReadLine()!.Split(":").ToList();

                bool has3Parts = TimeTemp.Count == 3;
                bool eachPartsLengthIs2 = TimeTemp.All(PartOfATime => PartOfATime.Length == 2);
                bool hourIsWithin1and12 = Convert.ToInt32(TimeTemp[0]) > 0 && Convert.ToInt32(TimeTemp[0]) < 13;
                bool MMandSSIsWithin0and59 = TimeTemp.All(PartOfATime => PartOfATime == TimeTemp[0] || (Convert.ToInt32(PartOfATime) >= 0 && Convert.ToInt32(PartOfATime) < 59));

                if(!(has3Parts && eachPartsLengthIs2 && hourIsWithin1and12 && MMandSSIsWithin0and59)) throw new Exception();

                return DateTime.Parse(TimeTemp.ToString()!);
            }
            catch (Exception e) { Console.WriteLine("Invalid Syntax\n"); return GetTimeInput(); }
        }

        public static int ChangeSnoozePeriod()
        {
            try
            {
                Console.WriteLine("Change the current snooze alarm in minutes: ");
                return Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception e) { Console.WriteLine("Please enter a valid snooze period in minutes\n"); return ChangeSnoozePeriod(); }
        }
    

    }
}
