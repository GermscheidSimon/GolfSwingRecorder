using System;
using System.Diagnostics;
using System.Threading.Tasks;
using DynamicData;

namespace GolfRecorder.WifiServices
{
    public class MacOsWifiService : WifiService
    {
        public MacOsWifiService()
        {
            
        }
        public override async void Connect(string ssid, string password)
        {
            int retry = 0;
            int timeout = 30;
            bool ssidFound = false;
            while (retry <=timeout)
            {
                var scanResult = RunCommand("/System/Library/PrivateFrameworks/Apple80211.framework/Versions/Current/Resources/airport --scan");
                string[] scanResLines = scanResult.Split(Environment.NewLine);
                int indexOfSsidRow = scanResLines[0].IndexOf("SSID") + 4;

                if (scanResLines.Length <= 0)
                {
                    Console.WriteLine("No Networks Detected");
                    return;
                }
            
                for (int i = 1; i < scanResLines.Length; i++)
                {
                    scanResLines[i] = scanResLines[i].Substring(0, indexOfSsidRow).Trim();
                }

                if (scanResLines.IndexOf(ssid) > 0)
                {
                    ssidFound = true;
                    break;
                }

                await Task.Delay(2000);
                retry++;
            }

            if (!ssidFound)
            {
                Console.WriteLine("Failed To Find SSID");
                return;
            }
            // determine network interface currently in use. below is layout of interface item
            //
            // Hardware Port: Wi-Fi
            // Device: en0
            // Ethernet Address: 00:aa:44:11:11:11
            //
            var interfaceScan = RunCommand("networksetup -listallhardwareports");
            string[] interfaceScanList = interfaceScan.Split(Environment.NewLine);
            int wifiInterfaceLineIndex = -1;

            for (int i = 0; i < interfaceScanList.Length; i++)
            {
                if (interfaceScanList[i].IndexOf("Wi-Fi") > 0)
                {
                    wifiInterfaceLineIndex = i++;
                    break;
                }
            }
            
            if (wifiInterfaceLineIndex < 0)
            {
                Console.WriteLine("Failed To Find Interfaces");
                return;
            }
            var interfaceId = interfaceScanList[wifiInterfaceLineIndex].Replace("Device: ", "");

            Console.WriteLine(interfaceId);

            RunCommand($"networksetup -setairportnetwork '{interfaceId}' '{ssid}' '{password}");
            
            // Current Wi-Fi Network: <ssidName>
            var currentNetworkScan = RunCommand($"networksetup -getairportnetwork {interfaceId}");
            var currentNetworkSsid = currentNetworkScan.Replace("Current Wi-Fi Network: ", "");
            if (currentNetworkSsid != ssid)
            {
                Console.WriteLine("Failed to connect to network");
            }
        }
        
        private static string RunCommand(string command)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "/bin/bash",
                Arguments = $"-c \"{command}\"",
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