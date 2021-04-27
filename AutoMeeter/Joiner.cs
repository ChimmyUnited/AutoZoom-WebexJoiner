using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

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
            IncorrectInput.Hide();
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
            string[] meetings = new string[MeetingsList.Items.Count];
            MeetingsList.Items.CopyTo(meetings, 0);
            if (meetings.Length > 0)
            {
                DateTime nextMeeting = DateTime.Parse(meetings[0].Split("     ")[1]);
                if (nextMeeting.Subtract(currentTime) > TimeSpan.Zero)
                {
                    TimeUntilNextClass.Text = "Time Until Next Class: " + formatDifference(nextMeeting.Subtract(currentTime));
                } else
                {
                    if (meetings.Length > 1)
                    {
                        DateTime nextMeeting1 = DateTime.Parse(meetings[1].Split("     ")[1]);
                        TimeUntilNextClass.Text = "Time Until Next Class: " + formatDifference(nextMeeting1.Subtract(currentTime));
                    } else
                    {
                        TimeUntilNextClass.Text = "Time Until Next Class: Null";
                    }
                    string url = meetings[0].Split("     ")[0];
                    Task.Factory.StartNew(() => { JoinMeeting(url); });
                    meetings = meetings.Skip(1).ToArray();
                    MeetingsList.Items.Clear();
                    foreach (string meeting in meetings)
                    {
                        MeetingsList.Items.Add(meeting);
                    }
                }
            }
        }

        private void AddMeetingButton_Click(object sender, EventArgs e)
        {
            DateTime timeMeeting;
            // Check if URL matches Webex/Zoom pattern and time is in proper format
            if (!((URLinput.Text.Contains("webex.com") || URLinput.Text.Contains("zoom.us")) && (URLinput.Text.StartsWith("https://") || URLinput.Text.StartsWith("http://"))))
            {
                ShowError("Please put a proper URL that starts with http:// or https://!");
                return;
            }
            else if (!DateTime.TryParse(TimeInput.Text, out timeMeeting))
            {
                ShowError("Please put a proper Time!");
                return;
            }
            else if (timeMeeting.CompareTo(DateTime.Now) < 0)
            {
                ShowError("Please put a Time after now!");
                return;
            }
            MeetingsList.Items.Add(URLinput.Text + "     " + timeMeeting.ToString());
            URLinput.Text = "";
            TimeInput.Text = "";
        }
        private void RemoveMeeting_Click(object sender, EventArgs e)
        {
            if (MeetingsList.SelectedItem == null)
            {
                ShowError("Select the Meeting you want to Remove!");
            } else
            {
                MeetingsList.Items.Remove(this.MeetingsList.SelectedItem);
                if (MeetingsList.Items.Count == 0)
                {
                    TimeUntilNextClass.Text = "Time Until Next Class: Null";
                }
            }
        }
        private async void ShowError (string errorMessage)
        {
            IncorrectInput.Text = "Error: " + errorMessage;
            IncorrectInput.Show();
            IncorrectInput.Refresh();
            await Task.Delay(2000);
            IncorrectInput.Hide();
            IncorrectInput.Refresh();
        }
        private void JoinMeeting(string URL) 
        {
            if (StopButton.Enabled)
            {
                return;
            }
            ProcessStartInfo psInfo = new ProcessStartInfo
            {
                FileName = URL,
                UseShellExecute = true
            };
            Process.Start(psInfo);
        }
        private string formatDifference (TimeSpan dateDifference)
        {
            return string.Format("{0:D2} hrs, {1:D2} mins, {2:D2} secs", dateDifference.Hours, dateDifference.Minutes, dateDifference.Seconds);
        }
    }
}