using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using Keys = OpenQA.Selenium.Keys;
using System.Collections;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace AutoMeeter
{
    public partial class Joiner : Form
    {
        ArrayList drivers = new ArrayList();
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
                if (!SavedMeeting.Equals(string.Empty))
                {
                    DateTime savedDate = DateTime.Parse(SavedMeeting.Split("     ")[1]);
                    DateTime now = DateTime.Now;
                    DateTime updated = new(now.Year, now.Month, now.Day, savedDate.Hour, savedDate.Minute, 0);
                    DefaultList.Items.Add(SavedMeeting.Split("     ")[0] + "     " + updated.ToString());
                }
            }
            NameInput.Text = Properties.Settings.Default.Name;
            EmailInput.Text = Properties.Settings.Default.Email;
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
                    TimeUntilNextClass.Text = "Time Until Next Class: " + FormatDifference(nextMeeting.Subtract(currentTime));
                } else
                {
                    if (meetings.Length > 1)
                    {
                        DateTime nextMeeting1 = DateTime.Parse(meetings[1].Split("     ")[1]);
                        TimeUntilNextClass.Text = "Time Until Next Class: " + FormatDifference(nextMeeting1.Subtract(currentTime));
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
            if (!((URLinput.Text.Contains("webex.com") || URLinput.Text.Contains("zoom.us"))))
            {
                ShowError("Please put a zoom or webex URL!");
                return;
            }
            else if (!DateTime.TryParse(TimeInput.Text, out timeMeeting))
            {
                ShowError("Please put a proper Time!");
                return;
            }
            else if (!Uri.IsWellFormedUriString(URLinput.Text, UriKind.Absolute))
            {
                ShowError("Please put a proper URL!");
                return;
            }
            else if ((timeMeeting.CompareTo(DateTime.Now) < 0) && EditMeetingIDs.Enabled)
            {
                ShowError("Please put a Time after now!");
                return;
            }
            if (EditDefault.Enabled)
            {
                DefaultList.Items.Add(URLinput.Text + "     " + timeMeeting.ToString());
                Sort(DefaultList);
            } else
            {
                MeetingsList.Items.Add(URLinput.Text + "     " + timeMeeting.ToString());
                Sort(MeetingsList);
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
            foreach (string s in DefaultList.Items)
            {
                DateTime savedMeeting = DateTime.Parse(s.Split("     ")[1]);
                if (savedMeeting.CompareTo(DateTime.Now) > 0)
                {
                    MeetingsList.Items.Add(s);
                }
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
            Properties.Settings.Default.Name = NameInput.Text;
            Properties.Settings.Default.Email = EmailInput.Text;
            Properties.Settings.Default.Save();
            foreach (ChromeDriver driver in drivers)
            {
                // driver.Quit();
            }
        }
        private async void ShowError(string errorMessage)
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
            if (URL.Contains("webex"))
            {
                ChromeDriverService commandlinehider = ChromeDriverService.CreateDefaultService();
                commandlinehider.HideCommandPromptWindow = true;
                ChromeOptions chrome_options = new ChromeOptions();
                chrome_options.AddUserProfilePreference("download_restrictions", 3);
                chrome_options.AddArgument("use-fake-ui-for-media-stream");
                ChromeDriver browser = new ChromeDriver(commandlinehider, chrome_options);
                browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                browser.Manage().Window.Maximize();
                drivers.Add(browser);
                try
                {
                    browser.Navigate().GoToUrl(URL);
                    ((IJavaScriptExecutor)browser).ExecuteScript("window.open();");
                    browser.SwitchTo().Window(browser.WindowHandles.First());
                    browser.Close();
                    browser.SwitchTo().Window(browser.WindowHandles.First());
                    browser.Navigate().GoToUrl(URL);
                    browser.FindElementByCssSelector("#push_download_join_by_browser").Click();
                    browser.SwitchTo().Frame(browser.FindElementByCssSelector("#pb-app-container > iframe"));
                    browser.FindElementByCssSelector("#meetingSimpleContainer > div.style-box-2gTpv > div.style-name-input-19PlX > input").SendKeys(NameInput.Text);
                    browser.FindElementByCssSelector("#meetingSimpleContainer > div.style-box-2gTpv > div.style-email-input-1yF5y > input").SendKeys(EmailInput.Text);
                    browser.FindElementByCssSelector("#guest_next-btn").Click();
                    browser.FindElementByCssSelector("#meetingSimpleContainer > div.style-content-container-YOy1G > div.style-control-bar-2vCte > div:nth-child(1) > div > button").Click();
                    browser.FindElementByCssSelector("#meetingSimpleContainer > div.style-content-container-YOy1G > div.style-control-bar-2vCte > div:nth-child(2) > div > button").Click();
                    browser.FindElementByCssSelector("#interstitial_join_btn").Click();
                }
                catch (Exception)
                {
                    drivers.Remove(browser);
                }
            }
            else
            {
                ProcessStartInfo psInfo = new()
                {
                    FileName = URL,
                    UseShellExecute = true
                };
                Process.Start(psInfo);
            }
        }
        private static string FormatDifference(TimeSpan dateDifference)
        {
            return string.Format("{0:D2} hrs, {1:D2} mins, {2:D2} secs", dateDifference.Hours, dateDifference.Minutes, dateDifference.Seconds);
        }
        private static void Sort(ListBox UnsortedListBox)
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
                    j--;
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