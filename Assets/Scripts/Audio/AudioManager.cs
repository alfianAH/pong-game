﻿using System;
using UnityConfig;
using UnityEngine;

namespace Audio
{
    public class AudioManager: MonoBehaviour
    {
        #region Singleton

        private static AudioManager instance;
        private const string LOG = nameof(AudioManager);

        public static AudioManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<AudioManager>();

                    if (instance == null)
                    {
                        Debug.LogError($"{LOG} not found");
                    }
                }

                return instance;
            }
        }


        #endregion
        
        #region Don't Destroy on Load
        
        /// <summary>
        /// Set instance and don't destroy on load
        /// </summary>
        private void SetInstance()
        {
            if (instance ==  null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
            
            DontDestroyOnLoad(gameObject);
        }

        #endregion
        
        [ArrayElementTitle("listSound")]
        public Sound[] sounds;

        #region Mono Behaviour
        
        private void Awake()
        {
            SetInstance();
            
            foreach (Sound sound in sounds)
            {
                sound.source = gameObject.AddComponent<AudioSource>();
                sound.source.clip = sound.clip;

                sound.source.volume = sound.volume;
                sound.source.pitch = sound.pitch;
                sound.source.loop = sound.loop;
            }
        }

        #endregion

        #region Audio
        
        /// <summary>
        /// Play audio
        /// To call method in scripts
        /// </summary>
        /// <param name="listSound"></param>
        public void Play(ListSound listSound)
        {
            GetAudioSource(listSound).Play();
        }
        
        /// <summary>
        /// Get audio source for enum
        /// </summary>
        /// <param name="listSound"></param>
        /// <returns></returns>
        private AudioSource GetAudioSource(ListSound listSound)
        {
            Sound s = Array.Find(sounds, sound => sound.listSound == listSound);
            
            if (s == null)
            {
                Debug.LogError($"Sound: {listSound} not found!");
                return null;
            }
            
            return s.source;
        }
        
        #endregion
    }
}