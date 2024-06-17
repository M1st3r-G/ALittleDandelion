using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioClipsContainer[] clips;
        [SerializeField] private AudioSource effectSrc;
        [SerializeField] private AudioSource musicSrc;
        [SerializeField] private AudioSource ambienceSrc;
        
        public static AudioManager Instance { get; private set; }
        
        public enum AudioEffect // MoneyChange | Ignored PotClicked due to Inheritance
        {
            Click, HoverUI, HoverGame, Dirt, Fertilizer, Water,  Shovel, SeedPlant, StarRating,  RemovePlant, Replant, BookPageFlip, NextDay
        }

        #region SetUp

        private void Awake()
        {
            Instance = this;
        }
        
        private void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }

        #endregion
        
        #region EffectManagement

        /// <summary>
        /// Plays a Clip of the given SoundEffect and returns its length in seconds
        /// </summary>
        /// <param name="effect">The ClipType to Play</param>
        /// <returns>The length of the played Clip</returns>
        public float PlayEffect(AudioEffect effect)
        {
            foreach (AudioClipsContainer cnt in clips)
            {
                if (cnt.Type != effect) continue;
                AudioClip clip = cnt.GetClip();
                if (clip is null)
                {
                    Debug.LogError($"Noch keine Sounds für {effect} sind importiert");
                    return -1f;
                }
                
                effectSrc.PlayOneShot(clip);
                return clip.length;
            }
            
            Debug.LogError($"Sound with {effect} Identifier not found");
            return -1f;
        }
        
        [Serializable]
        private struct AudioClipsContainer
        {
            public string name;
            [SerializeField] private AudioEffect type;
            [SerializeField] private AudioClip[] clips;

            public AudioEffect Type => type;
            
            public AudioClip GetClip()
            {
                return clips.Length == 0 ? null : clips[Random.Range(0, clips.Length)];
            }
        }

        #endregion
    }
}