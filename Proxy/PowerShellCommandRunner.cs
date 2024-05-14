using System.Diagnostics;

namespace GolfRecorder.Proxy
{
    public class PowerShellCommandRunner : CommandRunner
    {
        public PowerShellCommandRunner()
        {
            
        }
        public void RunCommand(string command)
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