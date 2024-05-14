namespace GolfRecorder.Proxy.Commands
{
    public abstract class BaseCommand
    {
        public virtual string Command(string OsKey)
        {
            return "";
        }
    }
}