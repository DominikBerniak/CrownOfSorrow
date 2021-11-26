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
        private AudioClip[] _weaponEquippedClips;
        private AudioClip[] _armorEquippedClips;
        private AudioClip[] _itemPickedUpClips;
        private AudioClip[] _playerDeathClips;
        private AudioClip[] _fightMusic;
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
            _musicClips = Resources.LoadAll<AudioClip>("AudioClips/backgroundMusic/background");
            _playerHitClips = Resources.LoadAll<AudioClip>("AudioClips/playerHit");
            _monsterHitClips = Resources.LoadAll<AudioClip>("AudioClips/monsterHit");
            _elixirDrinkClips = Resources.LoadAll<AudioClip>("AudioClips/elixirDrink");
            _armorEquippedClips = Resources.LoadAll<AudioClip>("AudioClips/armorEquipped");
            _weaponEquippedClips = Resources.LoadAll<AudioClip>("AudioClips/weaponEquipped");
            _itemPickedUpClips = Resources.LoadAll<AudioClip>("AudioClips/itemPickedUp");
            _playerDeathClips = Resources.LoadAll<AudioClip>("AudioClips/playerDeath");
            _fightMusic = Resources.LoadAll<AudioClip>("AudioClips/fightMusic");
            _volume = 0.4f;
            _musicAudioSource.loop = true;
        }

        public bool IsMusicPlaying()
        {
            return _musicAudioSource.isPlaying;
        }
        public void PlayBackgroundMusic()
        {
            _musicAudioSource.Stop();
            _musicAudioSource.clip = _musicClips[Utilities.Random.Next(_musicClips.Length)];
            _musicAudioSource.volume = _volume * 1/6;
            _musicAudioSource.Play();
        }

        public void PlayFightMusic()
        {
            _musicAudioSource.Stop();
            _musicAudioSource.clip = _fightMusic[Utilities.Random.Next(_fightMusic.Length)];
            _musicAudioSource.volume = _volume * 1/10;
            _musicAudioSource.Play();
        }

        public void PlayBossMusic()
        {
            _musicAudioSource.Stop();
            _musicAudioSource.clip = Resources.Load<AudioClip>("AudioClips/bossFight/boss_fight");
            _musicAudioSource.volume = _volume * 1/10;
            _musicAudioSource.Play();
        }
        
        public void PlayEndGameMusic()
        {
            _musicAudioSource.Stop();
            _musicAudioSource.clip = Resources.Load<AudioClip>("AudioClips/endGameMusic/end_game");
            _musicAudioSource.volume = _volume * 1/6;
            _musicAudioSource.Play();
        }
        public void PlayChristmasMusic()
        {
            _musicAudioSource.Stop();
            _musicAudioSource.clip = Resources.Load<AudioClip>("AudioClips/backgroundMusic/christmasbells");
            _musicAudioSource.volume = _volume * 1/6;
            _musicAudioSource.Play();
        }

        public void StopBackgroundMusic()
        {
            _musicAudioSource.Stop();
        }
        public void ToggleSoundMute()
        {
            _musicAudioSource.mute = !_musicAudioSource.mute;
            _soundsAudioSource.mute = !_soundsAudioSource.mute;
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

        public void PlayWeaponEquippedSound()
        {
            _soundsAudioSource.clip = _weaponEquippedClips[Utilities.Random.Next(_weaponEquippedClips.Length)];
            _soundsAudioSource.volume = _volume * 6/10;
            _soundsAudioSource.Play();
        }
        
        public void PlayArmorEquippedSound()
        {
            _soundsAudioSource.clip = _armorEquippedClips[Utilities.Random.Next(_armorEquippedClips.Length)];
            _soundsAudioSource.volume = _volume * 6/10;
            _soundsAudioSource.Play();
        }

        public void PlayItemPickedUpSound()
        {
            _soundsAudioSource.clip = _itemPickedUpClips[Utilities.Random.Next(_itemPickedUpClips.Length)];
            _soundsAudioSource.volume = _volume * 6/10;
            _soundsAudioSource.Play();
        }

        public void PlayPlayerDeathSound()
        {
            _soundsAudioSource.clip = _playerDeathClips[Utilities.Random.Next(_playerDeathClips.Length)];
            _soundsAudioSource.volume = _volume * 3;
            _soundsAudioSource.Play();
        }
        public void PlayGameOverSound()
        {
            _soundsAudioSource.clip = Resources.Load<AudioClip>("AudioClips/game_over");
            _soundsAudioSource.volume = _volume * 3;
            _soundsAudioSource.Play();
        }
    }
}