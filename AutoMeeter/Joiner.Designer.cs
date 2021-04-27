
using System.Windows.Forms;

namespace AutoMeeter
{
    partial class Joiner
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Joiner));
            this.StartButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.StopButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SystemTimeLabel = new System.Windows.Forms.Label();
            this.SystemTimer = new System.Windows.Forms.Timer(this.components);
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.TimeUntilNextClass = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.URLinput = new System.Windows.Forms.TextBox();
            this.TimeInput = new System.Windows.Forms.TextBox();
            this.URLLabel = new System.Windows.Forms.Label();
            this.TimeLabel = new System.Windows.Forms.Label();
            this.AddMeetingButton = new System.Windows.Forms.Button();
            this.IncorrectInput = new System.Windows.Forms.Label();
            this.MeetingsList = new System.Windows.Forms.ListBox();
            this.RemoveMeeting = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(30, 603);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(124, 35);
            this.StartButton.TabIndex = 6;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.Location = new System.Drawing.Point(0, -2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(337, 643);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(179, 603);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(124, 35);
            this.StopButton.TabIndex = 5;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(49)))), ((int)(((byte)(54)))));
            this.label1.Font = new System.Drawing.Font("Yu Gothic UI", 27.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(54, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(227, 50);
            this.label1.TabIndex = 4;
            this.label1.Text = "Meeting IDs";
            // 
            // SystemTimeLabel
            // 
            this.SystemTimeLabel.AutoSize = true;
            this.SystemTimeLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.SystemTimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 71.99999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.SystemTimeLabel.ForeColor = System.Drawing.Color.White;
            this.SystemTimeLabel.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.SystemTimeLabel.Location = new System.Drawing.Point(343, 45);
            this.SystemTimeLabel.Name = "SystemTimeLabel";
            this.SystemTimeLabel.Size = new System.Drawing.Size(213, 108);
            this.SystemTimeLabel.TabIndex = 3;
            this.SystemTimeLabel.Text = "Null";
            // 
            // SystemTimer
            // 
            this.SystemTimer.Interval = 1000;
            this.SystemTimer.Tick += new System.EventHandler(this.SystemTimer_Tick);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.pictureBox2.Location = new System.Drawing.Point(333, -2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(777, 196);
            this.pictureBox2.TabIndex = 8;
            this.pictureBox2.TabStop = false;
            // 
            // TimeUntilNextClass
            // 
            this.TimeUntilNextClass.AutoSize = true;
            this.TimeUntilNextClass.BackColor = System.Drawing.Color.Transparent;
            this.TimeUntilNextClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.TimeUntilNextClass.ForeColor = System.Drawing.SystemColors.Control;
            this.TimeUntilNextClass.Location = new System.Drawing.Point(343, 207);
            this.TimeUntilNextClass.Name = "TimeUntilNextClass";
            this.TimeUntilNextClass.Size = new System.Drawing.Size(326, 29);
            this.TimeUntilNextClass.TabIndex = 2;
            this.TimeUntilNextClass.Text = "Time Until Next Class: Null";
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Gray;
            this.pictureBox3.Location = new System.Drawing.Point(1002, 546);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(107, 103);
            this.pictureBox3.TabIndex = 1;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox4.BackgroundImage")));
            this.pictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox4.Location = new System.Drawing.Point(1013, 558);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(79, 76);
            this.pictureBox4.TabIndex = 0;
            this.pictureBox4.TabStop = false;
            // 
            // URLinput
            // 
            this.URLinput.Location = new System.Drawing.Point(419, 612);
            this.URLinput.Name = "URLinput";
            this.URLinput.Size = new System.Drawing.Size(292, 23);
            this.URLinput.TabIndex = 10;
            this.URLinput.TabStop = false;
            // 
            // TimeInput
            // 
            this.TimeInput.Location = new System.Drawing.Point(793, 612);
            this.TimeInput.Name = "TimeInput";
            this.TimeInput.Size = new System.Drawing.Size(122, 23);
            this.TimeInput.TabIndex = 11;
            this.TimeInput.TabStop = false;
            // 
            // URLLabel
            // 
            this.URLLabel.AutoSize = true;
            this.URLLabel.BackColor = System.Drawing.Color.Transparent;
            this.URLLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.URLLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.URLLabel.Location = new System.Drawing.Point(717, 606);
            this.URLLabel.Name = "URLLabel";
            this.URLLabel.Size = new System.Drawing.Size(80, 29);
            this.URLLabel.TabIndex = 12;
            this.URLLabel.Text = "Time:";
            // 
            // TimeLabel
            // 
            this.TimeLabel.AutoSize = true;
            this.TimeLabel.BackColor = System.Drawing.Color.Transparent;
            this.TimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.TimeLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.TimeLabel.Location = new System.Drawing.Point(343, 606);
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Size = new System.Drawing.Size(70, 29);
            this.TimeLabel.TabIndex = 13;
            this.TimeLabel.Text = "URL:";
            // 
            // AddMeetingButton
            // 
            this.AddMeetingButton.Location = new System.Drawing.Point(921, 612);
            this.AddMeetingButton.Name = "AddMeetingButton";
            this.AddMeetingButton.Size = new System.Drawing.Size(75, 22);
            this.AddMeetingButton.TabIndex = 14;
            this.AddMeetingButton.TabStop = false;
            this.AddMeetingButton.Text = "Add";
            this.AddMeetingButton.UseVisualStyleBackColor = true;
            this.AddMeetingButton.Click += new System.EventHandler(this.AddMeetingButton_Click);
            // 
            // IncorrectInput
            // 
            this.IncorrectInput.AutoSize = true;
            this.IncorrectInput.BackColor = System.Drawing.Color.Transparent;
            this.IncorrectInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.IncorrectInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(78)))), ((int)(((byte)(78)))));
            this.IncorrectInput.Location = new System.Drawing.Point(513, 582);
            this.IncorrectInput.Name = "IncorrectInput";
            this.IncorrectInput.Size = new System.Drawing.Size(69, 24);
            this.IncorrectInput.TabIndex = 15;
            this.IncorrectInput.Text = "Error: ";
            this.IncorrectInput.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MeetingsList
            // 
            this.MeetingsList.BackColor = System.Drawing.SystemColors.Menu;
            this.MeetingsList.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.MeetingsList.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.MeetingsList.FormattingEnabled = true;
            this.MeetingsList.HorizontalScrollbar = true;
            this.MeetingsList.ItemHeight = 20;
            this.MeetingsList.Location = new System.Drawing.Point(13, 67);
            this.MeetingsList.Name = "MeetingsList";
            this.MeetingsList.Size = new System.Drawing.Size(314, 484);
            this.MeetingsList.TabIndex = 16;
            this.MeetingsList.TabStop = false;
            // 
            // RemoveMeeting
            // 
            this.RemoveMeeting.Location = new System.Drawing.Point(104, 558);
            this.RemoveMeeting.Name = "RemoveMeeting";
            this.RemoveMeeting.Size = new System.Drawing.Size(124, 35);
            this.RemoveMeeting.TabIndex = 17;
            this.RemoveMeeting.TabStop = false;
            this.RemoveMeeting.Text = "Remove";
            this.RemoveMeeting.UseVisualStyleBackColor = true;
            this.RemoveMeeting.Click += new System.EventHandler(this.RemoveMeeting_Click);
            // 
            // Joiner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1104, 641);
            this.Controls.Add(this.RemoveMeeting);
            this.Controls.Add(this.MeetingsList);
            this.Controls.Add(this.IncorrectInput);
            this.Controls.Add(this.TimeUntilNextClass);
            this.Controls.Add(this.SystemTimeLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.URLinput);
            this.Controls.Add(this.TimeInput);
            this.Controls.Add(this.URLLabel);
            this.Controls.Add(this.TimeLabel);
            this.Controls.Add(this.AddMeetingButton);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1120, 680);
            this.MinimumSize = new System.Drawing.Size(1120, 680);
            this.Name = "Joiner";
            this.Text = "Auto Meeting Joiner";
            this.Load += new System.EventHandler(this.Joiner_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label SystemTimeLabel;
        private System.Windows.Forms.Timer SystemTimer;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label TimeUntilNextClass;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.TextBox URLinput;
        private System.Windows.Forms.TextBox TimeInput;
        private System.Windows.Forms.Label URLLabel;
        private System.Windows.Forms.Label TimeLabel;
        private System.Windows.Forms.Button AddMeetingButton;
        private System.Windows.Forms.Label IncorrectInput;
        private System.Windows.Forms.ListBox MeetingsList;
        private Button RemoveMeeting;
    }
}

