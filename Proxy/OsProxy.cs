using System;
using System.Runtime.InteropServices;


namespace GolfRecorder.Proxy
{
    public class OsProxy
    {
        public string OsKey;
        private CommandRunner _commandRunner; 
        public OsProxy()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                OsKey = "Windows";
                _commandRunner = new PowerShellCommandRunner();
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) {
                OsKey = "Linux";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) {
                OsKey = "MacOS";
                _commandRunner = new BashCommandRunner();
            }
        }

        
    }
}