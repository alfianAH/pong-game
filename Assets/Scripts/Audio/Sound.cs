﻿﻿using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    public enum ListSound
    {
        BallHit,
        ButtonClick,
    }
    
    [System.Serializable]
    public class Sound
    {
        public ListSound listSound;
        
        public AudioClip clip;

        [Range(0, 1)]
        public float volume = 1;
        [Range(-3, 3)]
        public float pitch = 1;

        public bool loop;

        [HideInInspector]
        public AudioSource source;
    }
}