
namespace GolfRecorder.WifiServices
{
    public abstract class WifiService
    {
        public WifiService() {}
        public abstract void Connect(string ssid, string password);
    }
}