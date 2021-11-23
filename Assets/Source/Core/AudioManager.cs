using System;
using UnityEngine;

namespace DungeonCrawl.Core
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Singleton { get; private set; }

        private GameObject _musicGameObject;
        private AudioSource _musicAudioSource;
        private GameObject _soundsGameObject;
        private AudioSource _soundsAudioSource;
        private AudioClip[] _musicClips;
        private AudioClip[] _playerHitClips;
        private AudioClip[] _monsterHitClips;
        private AudioClip[] _elixirDrinkClips;
        private float _volume;
        
        
        private void Awake()
        {
            if (Singleton != null)
            {
                Destroy(this);
                return;
            }
            
            Singleton = this;
            _musicGameObject = new GameObject("BackgroundMusic");
            _musicAudioSource = _musicGameObject.AddComponent<AudioSource>();
            _soundsGameObject = new GameObject("Sounds");
            _soundsAudioSource = _soundsGameObject.AddComponent<AudioSource>();
            _musicClips = Resources.LoadAll<AudioClip>("AudioClips");
            _playerHitClips = Resources.LoadAll<AudioClip>("AudioClips/playerHit");
            _monsterHitClips = Resources.LoadAll<AudioClip>("AudioClips/monsterHit");
            _elixirDrinkClips = Resources.LoadAll<AudioClip>("AudioClips/elixirDrink");
            _volume = 0.4f;
            _musicAudioSource.loop = true;
        }
        public void PlayBackgroundMusic()
        {
            _musicAudioSource.Stop();
            _musicAudioSource.clip = _musicClips[1];
            _musicAudioSource.volume = _volume * 1/2;
            _musicAudioSource.Play();
        }

        public void PlayChristmasMusic()
        {
            _musicAudioSource.Stop();
            _musicAudioSource.clip = _musicClips[2];
            _musicAudioSource.Play();
        }
        public void ToggleSoundMute()
        {
            _musicAudioSource.mute = !_musicAudioSource.mute;
        }

        public void PlayPlayerHitSound()
        {
            _soundsAudioSource.clip = _playerHitClips[Utilities.Random.Next(_playerHitClips.Length)];
            _soundsAudioSource.volume = _volume;
            _soundsAudioSource.Play();
        }
        
        public void PlayMonsterHitSound()
        {
            _soundsAudioSource.clip = _monsterHitClips[Utilities.Random.Next(_monsterHitClips.Length)];
            _soundsAudioSource.volume = _volume * 6/10;
            _soundsAudioSource.Play();
        }

        public void PlayElixirDrinkSound()
        {
            _soundsAudioSource.clip = _elixirDrinkClips[Utilities.Random.Next(_elixirDrinkClips.Length)];
            _soundsAudioSource.volume = _volume * 6/10;
            _soundsAudioSource.Play();
        }
        
    }
}