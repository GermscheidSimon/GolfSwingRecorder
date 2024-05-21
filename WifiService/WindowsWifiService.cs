using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.IO;

namespace GolfRecorder.WifiServices
{
    public class WindowsWifiService :  WifiService
    {
        public WindowsWifiService() {}
        public override void Connect(string ssid, string password)
        {
            var xmlWirelessProfile = getWirelessTemplate(ssid, password);
            var tempLocation =  Path.GetTempPath();
            var tempFile = Path.Combine(tempLocation, $".\\{ssid}template.xml");

            StreamWriter writer = new StreamWriter(tempFile);
            writer.Write(xmlWirelessProfile);
            writer.Close();
            var networks = RunCommand($"netsh wlan add profile filename={tempFile}");
            Console.WriteLine(networks);
            var res = RunCommand($"netsh wlan connect name={ssid} ssid={ssid} ");
            Console.WriteLine(res);
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
        
        private string getWirelessTemplate(string ssid, string password, string auth = "WPA2PSK", string encrypt = "AES")
        {
            return $@"
                <?xml version=""1.0""?>
                 <WLANProfile xmlns=""http://www.microsoft.com/networking/WLAN/profile/v1"">
                     <name>{ssid}</name>
                     <SSIDConfig>
                         <SSID>
                             <name>{ssid}</name>
                         </SSID>
                     </SSIDConfig>
                     <connectionType>ESS</connectionType>
                     <connectionMode>manual</connectionMode>
                     <MSM>
                         <security>
                             <authEncryption>
                                 <authentication>{auth}</authentication>
                                 <encryption>{encrypt}</encryption>
                                 <useOneX>false</useOneX>
                             </authEncryption>
                             <sharedKey>
                                 <keyType>passPhrase</keyType>
                                 <protected>false</protected>
                                 <keyMaterial>{password}</keyMaterial>
                             </sharedKey>
                         </security>
                     </MSM>
                     <MacRandomization xmlns=""http://www.microsoft.com/networking/WLAN/profile/v3"">
                         <enableRandomization>false</enableRandomization>
                     </MacRandomization>
                 </WLANProfile>";
        }
    }
}