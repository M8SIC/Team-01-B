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
            do
            {
                switch(IO.GetTaskInput())
                {
                    case "Add Alarm":
                        break;
                    case "Quit":
                        Environment.Exit(0);
                        break;
                } 
                Console.WriteLine("Action DONE\n");
            } while (true);
        }

        public void ShowEditView()
        {

        }

        public void ShowAddAlarmView()
        {

        }

    }
}
