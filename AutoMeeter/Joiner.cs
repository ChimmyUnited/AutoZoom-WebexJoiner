using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace AutoMeeter
{
    public partial class Joiner : Form
    {
        public Joiner()
        {
            InitializeComponent();
        }

        private void Joiner_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.Sizable;
            MaximizeBox = false; // ensure
            StopButton.Enabled = false;
            SystemTimer.Enabled = true;
        }

        private static string GetDefaultBrowserPath()
        {
            string key = @"htmlfile\shell\open\command";
            RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(key, false);
            return ((string)registryKey.GetValue(null, null)).Split('"')[1];
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            StopButton.Enabled = true;
            StartButton.Enabled = false;
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            StartButton.Enabled = true;
            StopButton.Enabled = false;
        }

        private void SystemTimer_Tick(object sender, EventArgs e)
        {
            DateTime currentTime = DateTime.Now;
            SystemTimeLabel.Text = currentTime.ToString()[currentTime.ToString().IndexOf(" ")..];
            string[] meetings = new string[this.MeetingsList.Items.Count];
            this.MeetingsList.Items.CopyTo(meetings, 0);
            if (meetings.Length > 0)
            {
                DateTime nextMeeting = DateTime.Parse(meetings[0].Split("     ")[1]);
                this.TimeUntilNextClass.Text = "Time Until Next Class: " + nextMeeting.Subtract(currentTime).ToString();
            }
        }

        private void AddMeetingButton_Click(object sender, EventArgs e)
        {
            DateTime timeMeeting;
            // Check if URL matches Webex pattern and time is in proper format
            if (!(URLinput.Text.Contains("webex.com")))
            {
                showError("Please put a proper URL!");
                return;
            }
            else if (!DateTime.TryParse(TimeInput.Text, out timeMeeting))
            {
                showError("Please put a proper Time!");
                return;
            }
            this.MeetingsList.Items.Add(URLinput.Text + "     " + timeMeeting.ToString());
        }
        private void showError (string errorMessage)
        {
            this.IncorrectInput.Text = "Error: " + errorMessage;
            this.IncorrectInput.Show();
            this.IncorrectInput.Refresh();
            Thread.Sleep(2000);
            this.IncorrectInput.Hide();
            this.IncorrectInput.Refresh();
        }
    }
}
