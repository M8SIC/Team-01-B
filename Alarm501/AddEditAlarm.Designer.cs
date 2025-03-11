using System;

namespace Alarm501_GUI
{
    partial class AddEditAlarm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dateTimePicker1 = new DateTimePicker();
            UXSetAlarmBtn = new Button();
            UxCancelEditAlarmBtn = new Button();
            isOnCheckBox = new CheckBox();
            uxName = new TextBox();
            grpRepeatOptions = new GroupBox();
            uxNone = new RadioButton();
            uxDaily = new RadioButton();
            uxWeekly = new RadioButton();
            cmbSounds = new ComboBox();
            grpRepeatOptions.SuspendLayout();
            SuspendLayout();
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.CalendarFont = new Font("Microsoft Sans Serif", 22.125F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dateTimePicker1.Format = DateTimePickerFormat.Time;
            dateTimePicker1.Location = new Point(48, 80);
            dateTimePicker1.Margin = new Padding(2);
            dateTimePicker1.MinDate = new DateTime(2025, 3, 2, 0, 0, 0, 0);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(309, 31);
            dateTimePicker1.TabIndex = 0;
            dateTimePicker1.Value = DateTime.Now.AddSeconds(5);
            // 
            // UXSetAlarmBtn
            // 
            UXSetAlarmBtn.Location = new Point(167, 265);
            UXSetAlarmBtn.Margin = new Padding(2);
            UXSetAlarmBtn.Name = "UXSetAlarmBtn";
            UXSetAlarmBtn.Size = new Size(96, 58);
            UXSetAlarmBtn.TabIndex = 1;
            UXSetAlarmBtn.Text = "Set";
            UXSetAlarmBtn.UseVisualStyleBackColor = true;
            UXSetAlarmBtn.Click += OnAddAlarm;
            // 
            // UxCancelEditAlarmBtn
            // 
            UxCancelEditAlarmBtn.Location = new Point(49, 265);
            UxCancelEditAlarmBtn.Margin = new Padding(2);
            UxCancelEditAlarmBtn.Name = "UxCancelEditAlarmBtn";
            UxCancelEditAlarmBtn.Size = new Size(96, 58);
            UxCancelEditAlarmBtn.TabIndex = 2;
            UxCancelEditAlarmBtn.Text = "Cancel";
            UxCancelEditAlarmBtn.UseVisualStyleBackColor = true;
            UxCancelEditAlarmBtn.Click += OnCancel;
            // 
            // isOnCheckBox
            // 
            isOnCheckBox.AutoSize = true;
            isOnCheckBox.Location = new Point(295, 30);
            isOnCheckBox.Margin = new Padding(2);
            isOnCheckBox.Name = "isOnCheckBox";
            isOnCheckBox.Size = new Size(62, 29);
            isOnCheckBox.TabIndex = 3;
            isOnCheckBox.Text = "On";
            isOnCheckBox.UseVisualStyleBackColor = true;
            // 
            // uxName
            // 
            uxName.Location = new Point(48, 28);
            uxName.Margin = new Padding(3, 4, 3, 4);
            uxName.Name = "uxName";
            uxName.PlaceholderText = "Enter Alarm Name:";
            uxName.Size = new Size(222, 31);
            uxName.TabIndex = 0;
            // 
            // grpRepeatOptions
            // 
            grpRepeatOptions.Controls.Add(uxNone);
            grpRepeatOptions.Controls.Add(uxDaily);
            grpRepeatOptions.Controls.Add(uxWeekly);
            grpRepeatOptions.Location = new Point(48, 128);
            grpRepeatOptions.Name = "grpRepeatOptions";
            grpRepeatOptions.Size = new Size(210, 114);
            grpRepeatOptions.TabIndex = 0;
            grpRepeatOptions.TabStop = false;
            grpRepeatOptions.Text = "Repeat Options";
            // 
            // uxNone
            // 
            uxNone.Checked = true;
            uxNone.Location = new Point(10, 30);
            uxNone.Name = "uxNone";
            uxNone.Size = new Size(104, 35);
            uxNone.TabIndex = 1;
            uxNone.TabStop = true;
            uxNone.Text = "None";
            // 
            // uxDaily
            // 
            uxDaily.Location = new Point(120, 30);
            uxDaily.Name = "uxDaily";
            uxDaily.Size = new Size(84, 38);
            uxDaily.TabIndex = 0;
            uxDaily.Text = "Daily";
            // 
            // uxWeekly
            // 
            uxWeekly.Location = new Point(10, 71);
            uxWeekly.Name = "uxWeekly";
            uxWeekly.Size = new Size(104, 39);
            uxWeekly.TabIndex = 1;
            uxWeekly.Text = "Weekly";
            // 
            // cmbSounds
            // 
            cmbSounds.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSounds.Items.AddRange(new object[] {AlarmSound.Radar, AlarmSound.Beacon, AlarmSound.Chimes, AlarmSound.Circuit, AlarmSound.Reflection});
            cmbSounds.Location = new Point(270, 140);
            cmbSounds.Name = "cmbSounds";
            cmbSounds.Size = new Size(150, 33);
            cmbSounds.TabIndex = 0;
            cmbSounds.SelectedIndex = 0;

            // 
            // AddEditAlarm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(436, 342);
            Controls.Add(cmbSounds);
            Controls.Add(grpRepeatOptions);
            Controls.Add(uxName);
            Controls.Add(isOnCheckBox);
            Controls.Add(UxCancelEditAlarmBtn);
            Controls.Add(UXSetAlarmBtn);
            Controls.Add(dateTimePicker1);
            Margin = new Padding(2);
            Name = "AddEditAlarm";
            Text = "Add/Edit Alarm";
            grpRepeatOptions.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button UXSetAlarmBtn;
        private System.Windows.Forms.Button UxCancelEditAlarmBtn;
        private System.Windows.Forms.CheckBox isOnCheckBox;
        private TextBox uxName;
    
        private GroupBox grpRepeatOptions;
        private RadioButton uxNone;
        private RadioButton uxDaily;
        private RadioButton uxWeekly;
        private ComboBox cmbSounds;
    }
}