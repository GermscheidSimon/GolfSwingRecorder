using System;
using System.Diagnostics;

namespace GolfRecorder.Adapters
{
    public class NetworkSetupWireless
    {
        public string test =
            "/System/Library/PrivateFrameworks/Apple80211.framework/Versions/Current/Resources/airport --scan";
        

        public static void RunCommand(string command)
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