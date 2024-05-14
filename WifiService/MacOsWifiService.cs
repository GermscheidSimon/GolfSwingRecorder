using System.Diagnostics;

namespace GolfRecorder.WifiService
{
    public class MacOsWifiService : WifiService
    {
        public static void Connect(string ssid, string password)
        {
            RunCommand("/System/Library/PrivateFrameworks/Apple80211.framework/Versions/Current/Resources/airport --scan");
            
        }
        
        private static void RunCommand(string command)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "/bin/bash",
                Arguments = $"-c \"{command}\""
            };

            Process process = new Process
            {
                StartInfo = startInfo
            };
            process.Start();
        }
    }
}