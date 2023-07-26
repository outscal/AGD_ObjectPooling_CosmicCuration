using System;
using UnityEngine;

namespace CosmicCuration.Audio
{
    public class SoundService
    {
        private SoundScriptableObject soundScriptableObject;
        private AudioSource audioEffects;
        private AudioSource backgroundMusic;

        public bool isSoundEnabled;
        public bool isMusicEnabled;

        public SoundService(SoundScriptableObject soundScriptableObject, AudioSource audioEffectSource, AudioSource bgMusicSource)
        {
            this.soundScriptableObject = soundScriptableObject;
            audioEffects = audioEffectSource;
            backgroundMusic = bgMusicSource;
            PlaybackgroundMusic(SoundType.BackgroundMusic, true);
            isSoundEnabled = true;
            isMusicEnabled = true;
        }

        public void PlaySoundEffects(SoundType soundType, bool loopSound = false)
        {
            if (!isSoundEnabled)
                return;

            AudioClip clip = GetSoundClip(soundType);
            if (clip != null)
            {
                audioEffects.loop = loopSound;
                audioEffects.clip = clip;
                audioEffects.PlayOneShot(clip);
            }
            else
                Debug.LogError("No Audio Clip selected.");
        }

        private void PlaybackgroundMusic(SoundType soundType, bool loopSound = false)
        {
            AudioClip clip = GetSoundClip(soundType);
            if (clip != null)
            {
                backgroundMusic.loop = loopSound;
                backgroundMusic.clip = clip;
                backgroundMusic.Play();
            }
            else
                Debug.LogError("No Audio Clip selected.");
        }

        private AudioClip GetSoundClip(SoundType soundType)
        {
            Sounds st = Array.Find(soundScriptableObject.audioList, item => item.soundType == soundType);
            if (st.audio != null)
                return st.audio;
            return null;
        }

        public void SetSoundState()
        {
            if (isSoundEnabled)
            {
                audioEffects.Stop();
                isSoundEnabled = false;
            }
            else
            {
                audioEffects.Play();
                isSoundEnabled = true;
            }
        }

        public void SetMusicState()
        {
            if (isMusicEnabled)
            {
                backgroundMusic.Stop();
                isMusicEnabled = false;
            }
            else
            {
                backgroundMusic.Play();
                isMusicEnabled = true;
            }
        }
    }
}