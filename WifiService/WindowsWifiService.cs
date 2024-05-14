using System.Diagnostics;

namespace GolfRecorder.WifiService
{
    public class WindowsWifiService :  WifiService
    {
        public static void Connect()
        {
            
        }
        private void RunCommand(string command)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "powershell",
                Arguments = $" \"{command}\""
            };

            Process process = new Process
            {
                StartInfo = startInfo
            };
            process.Start();
        }
    }
}