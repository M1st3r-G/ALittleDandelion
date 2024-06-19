using System.Collections;
using Managers;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(AudioSource))]
    public class MainMenuController : MonoBehaviour
    {
        [Header("MusicSliders")]
        [SerializeField] private Slider masterVolumeSlider;
        [SerializeField] private Slider ambienceVolumeSlider;
        [SerializeField] private Slider effectVolumeSlider;

        [Header("References")] 
        [SerializeField] private GameObject sliders;
        [SerializeField] private AudioSource effectSource;
        [SerializeField] private AudioMixer mainAudioMixer;

        [Header("SoundClips")] 
        [SerializeField] private AudioClip click;
        [SerializeField] private AudioClip hover;
        
        private float _cooldown;
        
        private void Start()
        {
            masterVolumeSlider.value   = PlayerPrefs.GetFloat(AudioManager.MasterVolumeKey,   PauseMenuController.DefaultZeroDecibel);
            ambienceVolumeSlider.value = PlayerPrefs.GetFloat(AudioManager.AmbienceVolumeKey, PauseMenuController.DefaultZeroDecibel);
            effectVolumeSlider.value   = PlayerPrefs.GetFloat(AudioManager.EffectVolumeKey,   PauseMenuController.DefaultZeroDecibel);
        }

        #region SoundEffect

        public void PlayHover() => effectSource.PlayOneShot(hover);
        private float PlayClick()
        {
            effectSource.PlayOneShot(click);
            return click.length;
        }

        #endregion
        
        #region AudioManagement

        private void SaveSettings()
        {
            PlayerPrefs.SetFloat(AudioManager.MasterVolumeKey, masterVolumeSlider.value);
            PlayerPrefs.SetFloat(AudioManager.AmbienceVolumeKey, ambienceVolumeSlider.value);
            PlayerPrefs.SetFloat(AudioManager.EffectVolumeKey, effectVolumeSlider.value);
        }
        
        public void OnMasterVolumeChange()
        {
            mainAudioMixer.SetFloat("MasterVolume", PauseMenuController.SliderValueToDecibel(masterVolumeSlider.value));
        }

        public void OnAmbienceVolumeChange()
        {
            mainAudioMixer.SetFloat("AmbienceVolume", PauseMenuController.SliderValueToDecibel(ambienceVolumeSlider.value));
        }
            
        public void OnEffectVolumeChange()
        {
            mainAudioMixer.SetFloat("EffectVolume", PauseMenuController.SliderValueToDecibel(effectVolumeSlider.value));
            
            if (!(_cooldown <= 0f)) return;
            
            _cooldown = 1f;
            PlayClick();
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

        #endregion

        #region Buttons

        public void LoadGame()
        {
            SaveSettings();
            StartCoroutine(DoAfterTime(PlayClick(), () => { SceneManager.LoadScene(1); }));
        }
        
        public void NewGame()
        {
            PlayerPrefs.SetString(SaveGameManager.PrefSaveKey, "");
            LoadGame();
        }
        
        public void Quit()
        {
            SaveSettings();
            
            StartCoroutine(DoAfterTime(PlayClick(), Application.Quit));
        }
        
        public void ToggleSettings()
        {
            SaveSettings();
            StartCoroutine(DoAfterTime(PlayClick(), () => { sliders.SetActive(!sliders.activeSelf); }));
        }

        #endregion

        #region Utils

        private delegate void DoAfterTimeDelegate();
        
        private static IEnumerator DoAfterTime(float time, DoAfterTimeDelegate method)
        {
            yield return new WaitForSeconds(time);
            method?.Invoke();
        }

        #endregion
    }
}