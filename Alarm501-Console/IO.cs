using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alarm501_Console
{
    public static class IO
    {

        public static string GetTaskInput()
        {
            string[] possibleTasks = { "Add Alarm", "Edit Alarm", "Select A Alarm", "Quit" };

            try
            {
                Console.WriteLine($"Please Choose What To Do Next: (1-{possibleTasks.Length})");
                for (int i = 0; i < possibleTasks.Length; i++) Console.WriteLine($"({i + 1}) {possibleTasks[i]}");

                int task = Convert.ToInt32(Console.ReadLine());
                if (task < 1 || task > possibleTasks.Length) throw new Exception();

                return possibleTasks[task - 1].ToString();
            }
            catch (Exception e) { Console.WriteLine("Invalid Option\n"); return GetTaskInput(); }
        }
    }
}
