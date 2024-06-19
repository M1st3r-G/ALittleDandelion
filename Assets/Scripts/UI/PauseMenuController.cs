using System.Collections;
using Managers;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class PauseMenuController : MonoBehaviour
    {
        [Header("MusicSliders")]
        [SerializeField] private Slider masterVolumeSlider;
        [SerializeField] private Slider musicVolumeSlider;
        [SerializeField] private Slider ambienceVolumeSlider;
        [SerializeField] private Slider effectVolumeSlider;
        
        [Header("AudioMixerAssets")]
        [SerializeField] private AudioMixer mainAudioMixer;
        
        private CanvasGroup _cg;

        private float _cooldown;
        
        public const float DefaultZeroDecibel = 0.675f;
        private const float Default15Decibel = 0.475f;
        
        public static PauseMenuController Instance { get; private set; }

        #region SetUp

        private void Awake()
        {
            Instance = this;
            
            _cg = GetComponent<CanvasGroup>();
            _cg.interactable = _cg.blocksRaycasts = false;
            _cg.alpha = 0;
        }

        private void Start()
        {
            masterVolumeSlider.value   = PlayerPrefs.GetFloat(AudioManager.MasterVolumeKey,   DefaultZeroDecibel);
            musicVolumeSlider.value    = PlayerPrefs.GetFloat(AudioManager.MusicVolumeKey,    Default15Decibel);
            ambienceVolumeSlider.value = PlayerPrefs.GetFloat(AudioManager.AmbienceVolumeKey, DefaultZeroDecibel);
            effectVolumeSlider.value   = PlayerPrefs.GetFloat(AudioManager.EffectVolumeKey,   DefaultZeroDecibel);
        }

        private void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }

        #endregion

        public void Toggle()
        {
            bool targetState = _cg.alpha < 0.5f;

            _cg.interactable = _cg.blocksRaycasts = targetState;
            _cg.alpha = targetState ? 1 : 0;
        }

        private void SaveSettings()
        {
            PlayerPrefs.SetFloat(AudioManager.MasterVolumeKey, masterVolumeSlider.value);
            PlayerPrefs.SetFloat(AudioManager.MusicVolumeKey, musicVolumeSlider.value);
            PlayerPrefs.SetFloat(AudioManager.AmbienceVolumeKey, ambienceVolumeSlider.value);
            PlayerPrefs.SetFloat(AudioManager.EffectVolumeKey, effectVolumeSlider.value);
        }

        public void OnMasterVolumeChange()
        {
            mainAudioMixer.SetFloat("MasterVolume", SliderValueToDecibel(masterVolumeSlider.value));
        }
            
        public void OnMusicVolumeChange() 
        {
            mainAudioMixer.SetFloat("MusicVolume", SliderValueToDecibel(musicVolumeSlider.value));
        }
                
        public void OnAmbienceVolumeChange()
        {
            mainAudioMixer.SetFloat("AmbienceVolume", SliderValueToDecibel(ambienceVolumeSlider.value));
        }
            
        public void OnEffectVolumeChange()
        {
            mainAudioMixer.SetFloat("EffectVolume", SliderValueToDecibel(effectVolumeSlider.value));
            
            if (!(_cooldown <= 0f)) return;
            
            _cooldown = 1f;
            AudioManager.Instance.PlayEffect(AudioManager.AudioEffect.Click);
            StartCoroutine(CountDown());
        }

        private IEnumerator CountDown()
        {
            while (_cooldown > 0f)
            {
                _cooldown -= Time.deltaTime;
                yield return null;
            }
        }
        
        public static float SliderValueToDecibel(float sliderValue) => 163.769f * Mathf.Log10(sliderValue + 0.324718f);
        
        #region ButtonFunctions

        public void ToMainMenu()
        {
            SaveSettings();
            SceneManager.LoadScene(0);
        }
        
        public void SaveAndQuit()
        {
            SaveSettings();
            Application.Quit();
        }

        #endregion
    }
}