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

        public static PauseMenuController Instance { get; private set; }

        #region SetUp

        private void Awake()
        {
            Instance = this;
            
            _cg = GetComponent<CanvasGroup>();
            _cg.interactable = _cg.blocksRaycasts = false;
            _cg.alpha = 0;
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
            Debug.Log("SaveAudioSettings");
        }
        
        public void OnMasterVolumeChange()
        {
            mainAudioMixer.SetFloat("MasterVolume", SliderValueToDecibel(masterVolumeSlider.value));
        }
            
        public void OnMusicVolumeChange() 
        {
            mainAudioMixer.SetFloat("MusicVolume", SliderValueToDecibel(musicVolumeSlider.value, -15f));
        }
                
        public void OnAmbienceVolumeChange()
        {
            mainAudioMixer.SetFloat("AmbienceVolume", SliderValueToDecibel(ambienceVolumeSlider.value));
        }
            
        public void OnEffectVolumeChange()
        {
            mainAudioMixer.SetFloat("EffectVolume", SliderValueToDecibel(effectVolumeSlider.value));
        }
        
        private float SliderValueToDecibel(float sliderValue, float defaultDecibel = 0, int minDecibel = -80, int maxDecibel = 20)
        {
            if (sliderValue < 0.75f)
            {
                return (-minDecibel + defaultDecibel) / 0.75f * sliderValue - 80;
            }
            return (maxDecibel - defaultDecibel) / 0.25f * (sliderValue - 0.75f) + defaultDecibel;
        }
        
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