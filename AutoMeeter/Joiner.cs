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
            EditDefault.Enabled = false;
            SystemTimer.Enabled = true;
            IncorrectInput.Hide();
            foreach (string SavedMeeting in Properties.Settings.Default.DefaultList.Split(','))
            {
                if (!SavedMeeting.Equals(""))
                {
                    DefaultList.Items.Add(SavedMeeting);
                }
            }
        }

        private void EditMeetingIDS_Click(object sender, EventArgs e)
        {
            EditDefault.Enabled = true;
            EditMeetingIDs.Enabled = false;
        }

        private void EditDefault_Click(object sender, EventArgs e)
        {
            EditMeetingIDs.Enabled = true;
            EditDefault.Enabled = false;
        }

        private void MeetingsList_Select(object sender, EventArgs e)
        {
            if (MeetingsList.SelectedItem != null)
            {
                DefaultList.ClearSelected();
            }
        }

        private void DefaultList_Select(object sender, EventArgs e)
        {
            if (DefaultList.SelectedItem != null)
            {
                MeetingsList.ClearSelected();
            }
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
            if (EditDefault.Enabled)
            {
                DefaultList.Items.Add(URLinput.Text + "     " + timeMeeting.ToString());
                sort(DefaultList);
            } else
            {
                MeetingsList.Items.Add(URLinput.Text + "     " + timeMeeting.ToString());
                sort(MeetingsList);
            }
            URLinput.Text = "";
            TimeInput.Text = "";
        }
        private void RemoveMeeting_Click(object sender, EventArgs e)
        {
            if (MeetingsList.SelectedItem == null && DefaultList.SelectedItem == null)
            {
                ShowError("Select the Meeting you want to Remove!");
            } else
            {
                if (DefaultList.SelectedItem == null)
                {
                    MeetingsList.Items.Remove(this.MeetingsList.SelectedItem);
                    if (MeetingsList.Items.Count == 0)
                    {
                        TimeUntilNextClass.Text = "Time Until Next Class: Null";
                    }
                } else
                {
                    DefaultList.Items.Remove(this.DefaultList.SelectedItem);
                }
            }
        }
        private void LoadDefault_Click(object sender, EventArgs e)
        {
            foreach (Object o in DefaultList.Items)
            {
                MeetingsList.Items.Add(o);
            }
        }
        private void SaveDefault_Click(object sender, EventArgs e)
        {
            string[] meetings = new string[DefaultList.Items.Count];
            DefaultList.Items.CopyTo(meetings, 0);
            Properties.Settings.Default.DefaultList = String.Join(",", meetings);
            Properties.Settings.Default.Save();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            string[] meetings = new string[DefaultList.Items.Count];
            DefaultList.Items.CopyTo(meetings, 0);
            Properties.Settings.Default.DefaultList = String.Join(",", meetings);
            Properties.Settings.Default.Save();
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
            if (EditDefault.Enabled)
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
        private void sort (ListBox UnsortedListBox)
        {
            string[] arr = new string[UnsortedListBox.Items.Count];
            UnsortedListBox.Items.CopyTo(arr, 0);
            if (arr.Length == 0)
            {
                return;
            }
            int i, j;
            string originalkey;
            DateTime key;
            for (i = 1; i < arr.Length; i++)
            {
                originalkey = arr[i];
                key = DateTime.Parse(arr[i].Split("     ")[1]);
                j = i - 1;
                while (j >= 0 && DateTime.Parse(arr[j].Split("     ")[1]).CompareTo(key) > 0)
                {
                    arr[j + 1] = arr[j];
                    j = j - 1;
                }
                arr[j + 1] = originalkey;
            }
            UnsortedListBox.Items.Clear();
            foreach (string meeting in arr)
            {
                UnsortedListBox.Items.Add(meeting);
            }
        }
    }
}