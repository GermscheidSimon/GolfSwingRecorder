﻿using System;
using Avalonia.Controls;
using LibVLCSharp.Shared;
using GolfRecorder.Services;

namespace GolfRecorder.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IDisposable
    {
        private readonly LibVLC _libVlc = new LibVLC();
        
        public MediaPlayer MediaPlayer { get; }
        
        public MainWindowViewModel()
        {
            MediaPlayer = new MediaPlayer(_libVlc);
            WifiService.setup();
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