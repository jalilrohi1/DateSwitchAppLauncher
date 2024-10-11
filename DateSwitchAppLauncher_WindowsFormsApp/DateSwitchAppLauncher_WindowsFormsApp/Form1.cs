using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace DateSwitchAppLauncher_WindowsFormsApp
{
    public partial class Form1 : Form
    {
        DateTime orignaldatetime;
        public Form1()
        {
            InitializeComponent();
            orignaldatetime= DateTime.Now;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEMTIME
        {
            public ushort wYear;
            public ushort wMonth;
            public ushort wDayOfWeek;
            public ushort wDay;
            public ushort wHour;
            public ushort wMinute;
            public ushort wSecond;
            public ushort wMilliseconds;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetSystemTime(ref SYSTEMTIME st);
        
        private void OpenTheApp_Click(object sender, EventArgs e)
        {
            DateTime selectedDateTime = new DateTime(2021, 10, 9, 12, 30, 45);
            SetDateTime(selectedDateTime);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetDateTime(orignaldatetime);
        }
        private void SetDateTime(DateTime dateTime)
        {
            // Convert to UTC before setting the system time
            DateTime utcTime = dateTime.ToUniversalTime();

            SYSTEMTIME st = new SYSTEMTIME
            {
                wYear = (ushort)utcTime.Year,
                wMonth = (ushort)utcTime.Month,
                wDay = (ushort)utcTime.Day,
                wHour = (ushort)utcTime.Hour,
                wMinute = (ushort)utcTime.Minute,
                wSecond = (ushort)utcTime.Second,
                wMilliseconds = (ushort)utcTime.Millisecond
            };

            // Call the SetSystemTime function to change the system time
            if (!SetSystemTime(ref st))
            {
                MessageBox.Show("Failed to set the system time. Ensure you have administrative privileges.");
            }
            else
            {
                MessageBox.Show("System time changed successfully.");
            }
        }

    }
}
