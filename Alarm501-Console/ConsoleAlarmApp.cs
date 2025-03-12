using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alarm501_Console
{
    public class ConsoleAlarmApp
    {
        public ConsoleAlarmApp() { }
        public void Start()
        {
            switch (IO.GetTaskInput())
            {
                case "Add Alarm":
                    break;
                case "Quit":
                    Environment.Exit(0);
                    break;
            }
            Console.WriteLine("Action DONE\n");

            Start();
        }

        public void ShowEditView()
        {

        }

        public void ShowAddAlarmView()
        {

        }

    }
}
