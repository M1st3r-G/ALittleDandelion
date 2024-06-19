using System.Collections;
using Managers;
using TMPro;
using UnityEngine;

namespace Controller
{
    [RequireComponent(typeof(CanvasGroup))]
    public class PlantRemoval : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI button1Text;
        [SerializeField] private TextMeshProUGUI button2Text;
        [SerializeField] private TextMeshProUGUI mainText;
        
        private CanvasGroup _canvasGroup;
        
        public delegate void CallbackType();

        private CallbackType _callBackMethod;
        private bool _isDead;
        
        public static PlantRemoval Instance { get; private set; }
        
        private void Awake()
        {
            Instance = this;
            _canvasGroup = GetComponent<CanvasGroup>();
            SetVisibility(false);
            button2Text.transform.parent.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            if(Instance == this) Instance = null;
        }

        // When the Callback is Triggered, it Removes the Flower from its spot.
        public void WaitForRemoval(bool isDead, int stars, CallbackType newCallBack)
        {
            _callBackMethod = newCallBack;
            CInputManager.Instance.SetNavigation(false);

            SetVisibility(true);
            
            if (!isDead)
            {
                mainText.text = $"Sehr gut, du hast eine {stars}-Sterne Blume gezüchtet. Was möchtst du mit ihr tun?";
                StartCoroutine(ConsecutiveStarSounds(stars));
                
                button1Text.text = "Wegschmeißen";
                button2Text.transform.parent.gameObject.SetActive(true);
                int freeSpace = -1; //TODO
                button2Text.text = $"Beahlten ({freeSpace} freie Plätze)";
            }
            else button1Text.text = "Wegschmeißen";
        }

        public void OnButton1Pressed()
        {
            AudioManager.Instance.PlayEffect(AudioManager.AudioEffect.Click);
            End();
        }
        
        public void OnButton2Pressed()
        {
            AudioManager.Instance.PlayEffect(AudioManager.AudioEffect.Click);
            //Add FlowerInstance, Env to Save TODO
            End();
        }

        private void End()
        {
            TutorialManager.Instance.SetFlag(TutorialManager.TutorialFlag.DecidedForPlant);
            button2Text.transform.parent.gameObject.SetActive(false);
            SetVisibility(false);
            CInputManager.Instance.SetNavigation(true);
            _callBackMethod?.Invoke();
        }

        private void SetVisibility(bool state)
        {
            _canvasGroup.alpha = state ? 1 : 0;
            _canvasGroup.interactable = _canvasGroup.blocksRaycasts = state;
        }

        private static IEnumerator ConsecutiveStarSounds(int stars)
        {
            for (int i = 0; i < stars; i++)
            {
                yield return new WaitForSeconds(AudioManager.Instance.PlayEffect(AudioManager.AudioEffect.StarRating));
                yield return new WaitForSeconds(0.25f);
            }
        }
    }
}