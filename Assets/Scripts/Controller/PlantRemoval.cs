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
        public void WaitForRemoval(bool isDead, CallbackType newCallBack)
        {
            _callBackMethod = newCallBack;
            CInputManager.Instance.SetNavigation(false);

            SetVisibility(true);
            AudioManager.Instance.PlayEffect(AudioManager.AudioEffect.StarRating);
            
            if (!isDead)
            {
                int value = -1;
                button1Text.text = $"Verkaufen für {value}";
                button2Text.transform.parent.gameObject.SetActive(true);
                int freeSpace = -1;
                button2Text.text = $"Beahlten ({freeSpace} freie Plätze)";
            }
            else button1Text.text = "Wegschmeißen";
        }

        public void OnButton1Pressed()
        {
            AudioManager.Instance.PlayEffect(AudioManager.AudioEffect.Click);
            
            if (!_isDead) {}//AddMoney(Value)
            End();
        }
        
        public void OnButton2Pressed()
        {
            AudioManager.Instance.PlayEffect(AudioManager.AudioEffect.Click);
            //Add FlowerInstance, Env to Save
            End();
        }

        private void End()
        {
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
    }
}