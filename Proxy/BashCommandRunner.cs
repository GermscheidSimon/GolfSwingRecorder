using System.Diagnostics;

namespace GolfRecorder.Proxy
{
    public class BashCommandRunner : CommandRunner
    {
        public BashCommandRunner()
        {
            
        }
        public void RunCommand(string command)
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