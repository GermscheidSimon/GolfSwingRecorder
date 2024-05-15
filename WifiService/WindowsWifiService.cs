using System;
using System.Diagnostics;

namespace GolfRecorder.WifiServices
{
    public class WindowsWifiService :  WifiService
    {
        public WindowsWifiService() {}
        public override void Connect(string ssid, string password)
        {
            var scanResult = RunCommand("/System/Library/PrivateFrameworks/Apple80211.framework/Versions/Current/Resources/airport --scan");
            string[] scanResLines = scanResult.Split(Environment.NewLine);
            foreach (var line in scanResLines)
            {
                Console.WriteLine(line);
            }
        }
        private static string RunCommand(string command)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "powershell",
                Arguments = $" \"{command}\"",
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };

            Process process = new Process
            {
                StartInfo = startInfo
            }; 
            process.Start();
            return process.StandardOutput.ReadToEnd();
        }
    }
}