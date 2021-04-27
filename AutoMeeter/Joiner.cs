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
using OpenQA.Selenium.Chrome;

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
            this.IncorrectInput.Hide();
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
                if (nextMeeting.Subtract(currentTime) > TimeSpan.Zero)
                {
                    this.TimeUntilNextClass.Text = "Time Until Next Class: " + formatDifference(nextMeeting.Subtract(currentTime));
                } else
                {
                    if (meetings.Length > 1)
                    {
                        DateTime nextMeeting1 = DateTime.Parse(meetings[1].Split("     ")[1]);
                        this.TimeUntilNextClass.Text = "Time Until Next Class: " + formatDifference(nextMeeting1.Subtract(currentTime));
                    } else
                    {
                        this.TimeUntilNextClass.Text = "Time Until Next Class: Null";
                    }
                    JoinMeeting(meetings[0].Split("     ")[0]);
                    meetings = meetings.Skip(1).ToArray();
                    this.MeetingsList.Items.Clear();
                    foreach (string meeting in meetings)
                    {
                        this.MeetingsList.Items.Add(meeting);
                    }
                }
            }
        }

        private void AddMeetingButton_Click(object sender, EventArgs e)
        {
            DateTime timeMeeting;
            // Check if URL matches Webex/Zoom pattern and time is in proper format
            if (!(URLinput.Text.Contains("webex.com") || URLinput.Text.Contains("zoom.us")))
            {
                ShowError("Please put a proper URL!");
                return;
            }
            else if (!DateTime.TryParse(TimeInput.Text, out timeMeeting))
            {
                ShowError("Please put a proper Time!");
                return;
            }
            if (timeMeeting.CompareTo(DateTime.Now) < 0)
            {
                ShowError("Please put a Time after now!");
                return;
            }
            this.MeetingsList.Items.Add(URLinput.Text + "     " + timeMeeting.ToString());
            URLinput.Text = "";
            TimeInput.Text = "";
        }
        private void RemoveMeeting_Click(object sender, EventArgs e)
        {
            if (this.MeetingsList.SelectedItem == null)
            {
                ShowError("Select the Meeting you want to Remove!");
            } else
            {
                this.MeetingsList.Items.Remove(this.MeetingsList.SelectedItem);
                if (this.MeetingsList.Items.Count == 0)
                {
                    this.TimeUntilNextClass.Text = "Time Until Next Class: Null";
                }
            }
        }
        private void ShowError (string errorMessage)
        {
            this.IncorrectInput.Text = "Error: " + errorMessage;
            this.IncorrectInput.Show();
            this.IncorrectInput.Refresh();
            Thread.Sleep(2000);
            this.IncorrectInput.Hide();
            this.IncorrectInput.Refresh();
        }
        private void JoinMeeting(string URL)
        {
            ChromeDriver driver = new();
            driver.Navigate().GoToUrl(URL);
            if(ZoomCheck.Checked)
            {
                throw new NotImplementedException();
            } else if(WebexCheck.Checked)
            {
                throw new NotImplementedException();
            } else
            {
                ShowError("Please Check A Meeting Type!");
            }
        }
        private string formatDifference (TimeSpan dateDifference)
        {
            return string.Format("{0:D2} hrs, {1:D2} mins, {2:D2} secs", dateDifference.Hours, dateDifference.Minutes, dateDifference.Seconds);
        }

        private void ZoomCheck_CheckedChanged(object sender, EventArgs e)
        {
            if(ZoomCheck.Checked)
            {
                WebexCheck.Checked = false;
            }
        }

        private void WebexCheck_CheckedChanged(object sender, EventArgs e)
        {
            if(WebexCheck.Checked)
            {
                ZoomCheck.Checked = false;
            }
        }
    }
}