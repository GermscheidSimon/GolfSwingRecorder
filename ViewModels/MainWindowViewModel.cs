using System;
using Avalonia.Controls;
using LibVLCSharp.Shared;
using GolfRecorder.WifiServices;
using System.Runtime.InteropServices;

namespace GolfRecorder.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IDisposable
    {
        private readonly LibVLC _libVlc = new LibVLC();
        private WifiService wifi;

        public MediaPlayer MediaPlayer { get; }
        
        public MainWindowViewModel()
        {
            MediaPlayer = new MediaPlayer(_libVlc);
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                wifi = new WindowsWifiService();
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                wifi = new MacOsWifiService();
            }

            wifi.Connect("wifi", "123");
        }

        public void Play()
        {
            if (Design.IsDesignMode)
            {
                return;
            }
            
            using var media = new Media(_libVlc, new Uri("http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4"));
            MediaPlayer.Play(media);
        }
        
        public void Stop()
        {            
            MediaPlayer.Stop();
        }
   
        public void Dispose()
        {
            MediaPlayer.Dispose();
            _libVlc.Dispose();
        }
    }
}