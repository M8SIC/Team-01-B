namespace Alarm501
{
    partial class Alarm501
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
            UxEditBtn = new Button();
            UxAddBtn = new Button();
            UxAlarmList = new ListBox();
            uxSnoozeTime = new NumericUpDown();
            SnoozeLabel = new Label();
            uxChangeActiveView = new Button();
            uxActiveViewLabel = new Label();
            uxDeleteBtn = new Button();
            ((System.ComponentModel.ISupportInitialize)uxSnoozeTime).BeginInit();
            SuspendLayout();
            // 
            // UxEditBtn
            // 
            UxEditBtn.Location = new Point(41, 32);
            UxEditBtn.Margin = new Padding(2);
            UxEditBtn.Name = "UxEditBtn";
            UxEditBtn.Size = new Size(120, 72);
            UxEditBtn.TabIndex = 0;
            UxEditBtn.Text = "Edit";
            UxEditBtn.UseVisualStyleBackColor = true;
            UxEditBtn.Click += OnEditButtonClick;
            // 
            // UxAddBtn
            // 
            UxAddBtn.Font = new Font("Microsoft Sans Serif", 13.875F, FontStyle.Regular, GraphicsUnit.Point, 0);
            UxAddBtn.Location = new Point(302, 32);
            UxAddBtn.Margin = new Padding(2);
            UxAddBtn.Name = "UxAddBtn";
            UxAddBtn.Size = new Size(117, 72);
            UxAddBtn.TabIndex = 1;
            UxAddBtn.Text = "+";
            UxAddBtn.UseVisualStyleBackColor = true;
            UxAddBtn.Click += UxAddBtn_Click;
            // 
            // UxAlarmList
            // 
            UxAlarmList.Font = new Font("Microsoft Sans Serif", 16.125F, FontStyle.Regular, GraphicsUnit.Point, 0);
            UxAlarmList.FormattingEnabled = true;
            UxAlarmList.ItemHeight = 37;
            UxAlarmList.Location = new Point(41, 142);
            UxAlarmList.Margin = new Padding(2);
            UxAlarmList.Name = "UxAlarmList";
            UxAlarmList.ScrollAlwaysVisible = true;
            UxAlarmList.Size = new Size(378, 263);
            UxAlarmList.TabIndex = 2;
            UxAlarmList.SelectedValueChanged += UpdateEditButtonStatus;
            // 
            // uxSnoozeTime
            // 
            uxSnoozeTime.Location = new Point(41, 519);
            uxSnoozeTime.Maximum = new decimal(new int[] { 30, 0, 0, 0 });
            uxSnoozeTime.Name = "uxSnoozeTime";
            uxSnoozeTime.Size = new Size(200, 31);
            uxSnoozeTime.TabIndex = 0;
            uxSnoozeTime.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // SnoozeLabel
            // 
            SnoozeLabel.AutoSize = true;
            SnoozeLabel.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            SnoozeLabel.Location = new Point(20, 494);
            SnoozeLabel.Margin = new Padding(0);
            SnoozeLabel.Name = "SnoozeLabel";
            SnoozeLabel.Size = new Size(221, 22);
            SnoozeLabel.TabIndex = 0;
            SnoozeLabel.Text = "   Snooze Period (Minutes)";
            SnoozeLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // uxChangeActiveView
            // 
            uxChangeActiveView.Location = new Point(41, 415);
            uxChangeActiveView.Name = "uxChangeActiveView";
            uxChangeActiveView.Size = new Size(172, 34);
            uxChangeActiveView.TabIndex = 3;
            uxChangeActiveView.Text = "Show Inactive";
            uxChangeActiveView.UseVisualStyleBackColor = true;
            uxChangeActiveView.Click += UxChangeActiveView_Click;
            // 
            // uxActiveViewLabel
            // 
            uxActiveViewLabel.AutoSize = true;
            uxActiveViewLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            uxActiveViewLabel.Location = new Point(41, 115);
            uxActiveViewLabel.Name = "uxActiveViewLabel";
            uxActiveViewLabel.Size = new Size(66, 25);
            uxActiveViewLabel.TabIndex = 4;
            uxActiveViewLabel.Text = "Active";
            // 
            // uxDeleteBtn
            // 
            uxDeleteBtn.Location = new Point(247, 415);
            uxDeleteBtn.Name = "uxDeleteBtn";
            uxDeleteBtn.Size = new Size(172, 34);
            uxDeleteBtn.TabIndex = 5;
            uxDeleteBtn.Text = "Multi Delete";
            uxDeleteBtn.UseVisualStyleBackColor = true;
            uxDeleteBtn.Click += uxDeleteBtn_Click;
            // 
            // Alarm501
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(456, 577);
            Controls.Add(uxDeleteBtn);
            Controls.Add(uxActiveViewLabel);
            Controls.Add(uxChangeActiveView);
            Controls.Add(SnoozeLabel);
            Controls.Add(uxSnoozeTime);
            Controls.Add(UxAlarmList);
            Controls.Add(UxAddBtn);
            Controls.Add(UxEditBtn);
            Margin = new Padding(2);
            Name = "Alarm501";
            Text = "Alarm501";
            ((System.ComponentModel.ISupportInitialize)uxSnoozeTime).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button UxEditBtn;
        private System.Windows.Forms.Button UxAddBtn;
        private System.Windows.Forms.ListBox UxAlarmList;

        private TextBox txtAlarmInput;
        private NumericUpDown uxSnoozeTime;
        private Label SnoozeLabel;
        private Button uxChangeActiveView;
        private Label uxActiveViewLabel;
        private Button uxDeleteBtn;
    }
}

