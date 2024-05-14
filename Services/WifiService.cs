using GolfRecorder.Adapters;

namespace GolfRecorder.Services
{
    public class WifiService
    {
        public static void setup()
        {
            NetworkSetupWireless.RunCommand("ls");
        }
        
    }
}