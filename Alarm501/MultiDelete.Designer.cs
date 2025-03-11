namespace Alarm501
{
    partial class MultiDelete
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
            uxAlarmToDeleteList = new FlowLayoutPanel();
            uxDeleteAlarms = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // uxAlarmToDeleteList
            // 
            uxAlarmToDeleteList.BackColor = SystemColors.Control;
            uxAlarmToDeleteList.BorderStyle = BorderStyle.FixedSingle;
            uxAlarmToDeleteList.FlowDirection = FlowDirection.TopDown;
            uxAlarmToDeleteList.Location = new Point(12, 45);
            uxAlarmToDeleteList.Name = "uxAlarmToDeleteList";
            uxAlarmToDeleteList.Size = new Size(341, 260);
            uxAlarmToDeleteList.TabIndex = 1;
            // 
            // uxDeleteAlarms
            // 
            uxDeleteAlarms.Location = new Point(12, 311);
            uxDeleteAlarms.Name = "uxDeleteAlarms";
            uxDeleteAlarms.Size = new Size(168, 34);
            uxDeleteAlarms.TabIndex = 2;
            uxDeleteAlarms.Text = "Delete";
            uxDeleteAlarms.UseVisualStyleBackColor = true;
            uxDeleteAlarms.Click += uxDeleteAlarms_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Underline);
            label1.Location = new Point(9, 14);
            label1.Name = "label1";
            label1.Size = new Size(148, 25);
            label1.TabIndex = 3;
            label1.Text = "Select To Delete";
            // 
            // MultiDelete
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(372, 357);
            Controls.Add(label1);
            Controls.Add(uxDeleteAlarms);
            Controls.Add(uxAlarmToDeleteList);
            Name = "MultiDelete";
            Text = "MultiDelete";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private FlowLayoutPanel uxAlarmToDeleteList;
        private Button uxDeleteAlarms;
        private Label label1;
    }
}