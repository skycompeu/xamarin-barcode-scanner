using System;
using System.Collections.Generic;
using System.Text;

namespace OmniReader.Core.EMDK
{
    public interface IAudioPlayerService
    {
        void Play(string pathToAudioFile);
        void Play();
        void Pause();
        Action OnFinishedPlaying { get; set; }
    }
}
