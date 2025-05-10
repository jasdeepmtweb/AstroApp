using System;

namespace AstroApp
{
    public partial class Countdown : System.Web.UI.Page
    {
        // Set your target date and time here
        private static DateTime targetDate = DateTime.Now.AddMinutes(1); // 5-minute countdown
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UpdateCountdown();
            }
        }
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            UpdateCountdown();
        }

        private void UpdateCountdown()
        {
            // Calculate remaining time
            TimeSpan remainingTime = targetDate - DateTime.Now;

            if (remainingTime.TotalSeconds > 0)
            {
                lblCountdown.Text = $"{remainingTime.Minutes:D2}:{remainingTime.Seconds:D2}";
            }
            else
            {
                lblCountdown.Text = "Time's up!";
                Timer1.Enabled = false; // Stop the timer
            }
        }
    }
}