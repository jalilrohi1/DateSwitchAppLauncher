using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace DateSwitchAppLauncher_ConsoleApp
{
    class Program
    {
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
        static void Main(string[] args)
        {
            DateTime originalDate = DateTime.Now;
            //*** Change the Date as per your requirnment ***
            DateTime newDate = new DateTime(2022, 1, 1); // Example date
            SetDateTime(newDate);
            OpenTheApp();
            SetDateTime(originalDate);
        }

        private static void OpenTheApp()
        {
            try
            {
                // Get the current directory where the WinForms app is running
                string currentFolder = Directory.GetCurrentDirectory();

                //***Specify the name of the application in the current folder***
                string appFileName = "Alokozay_Pharmacy.exe"; // Replace with the actual file name

                // Combine the current directory with the app file name
                string appPath = Path.Combine(currentFolder, appFileName);

                // Start the process (open the app)
                Process.Start(appPath);
            }
            catch (Exception ex)
            {
                // Handle any errors that might occur when trying to open the application
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetSystemTime(ref SYSTEMTIME st);
        private static void SetDateTime(DateTime dateTime)
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
                Console.WriteLine("Fiald to change System Date");
            }
            else
            {
                Console.WriteLine("System time changed successfully.");
            }
        }



    }
}