using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alarm501
{
    public partial class MultiDelete : Form
    {
        #region Fields/Property/Events
        private PassListOfIntFunc DeleteAlarm;
        #endregion

        #region Constructor/Methods
        public MultiDelete(BindingList<Alarm> alarmList, PassListOfIntFunc DeleteAlarm)
        {
            InitializeComponent();
            this.DeleteAlarm = DeleteAlarm;

            foreach (Alarm alarm in alarmList)
            {
                CheckBox checkBox = new CheckBox
                {
                    Text = alarm.AlarmTimeFormat,
                    AutoSize = true
                };

                uxAlarmToDeleteList.Controls.Add(checkBox);
            }

        }

        private void uxDeleteAlarms_Click(object sender, EventArgs e)
        {
            List<int> indexToDelete = new List<int>();

            foreach(CheckBox checkBox in uxAlarmToDeleteList.Controls)
            {
                if (checkBox.Checked) indexToDelete.Add(uxAlarmToDeleteList.Controls.IndexOf(checkBox));
            }

            DeleteAlarm(indexToDelete);

            this.Close();
        }
        #endregion
    }
}
