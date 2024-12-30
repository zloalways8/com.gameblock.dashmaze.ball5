using Configs;
using UnityEngine;

namespace Services
{
    public class AudioService : IAudioService
    {
        private readonly AudioSource _music;
        private readonly AudioSource _sound;
        private readonly AudioConfig _config;
    
        private bool _isSound;

        public AudioService(AudioSource music, AudioSource sound, AudioConfig config)
        {
            _music = music;
            _sound = sound;
            _config = config;
            _isSound = true;
        }

        public bool IsSound()
        {
            _isSound = !_isSound;
            _music.volume = _isSound ? 0.5f : 0;
            _sound.volume = _isSound ? 1 : 0;
            return _isSound;
        }

        public void PlaySound(int soundId)
        {
            _sound.PlayOneShot(_config.Clips[soundId]);
        }
    }

    public interface IAudioService
    {
        bool IsSound();
        void PlaySound(int id);
    }
}