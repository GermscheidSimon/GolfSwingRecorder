namespace GolfRecorder.Proxy.Commands
{
    public class ScanSsids : BaseCommand
    {
        public static string Command(string osKey)
        {
            if (osKey == "Windows") return "start ms-availablenetworks";
            if (osKey == "MacOs") return ""
        }
    }
}