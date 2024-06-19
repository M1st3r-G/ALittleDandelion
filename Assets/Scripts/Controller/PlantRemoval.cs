using System.Collections;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controller
{
    [RequireComponent(typeof(CanvasGroup))]
    public class PlantRemoval : MonoBehaviour
    {
        [Header("Component References")]
        [SerializeField] private TextMeshProUGUI mainText;
        [Header("Stars")] 
        [SerializeField] private Image[] images;
        [SerializeField] private Sprite activeStar;
        [SerializeField] private Sprite emptyStar;
        
        
        private CanvasGroup _canvasGroup;
        private bool _isDead;
        
        public delegate void CallbackType();
        private CallbackType _callBackMethod;
        
        public static PlantRemoval Instance { get; private set; }
        
        private void Awake()
        {
            Instance = this;
            _canvasGroup = GetComponent<CanvasGroup>();
            SetVisibility(false);
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
                mainText.text = $"Sehr gut, du hast eine {stars}-Sterne Blume gezüchtet. Dein Fortschritt wurde im Blumenbuch vermerkt. Weiter so!";
                StartCoroutine(ConsecutiveStarSounds(stars));
            }
            else
            {
                mainText.text = "Leider ist die Pflanze gestorben. Versuch es doch nochmal und probiere anders aus.";
            }
        }

        public void OnButtonPressed()
        {
            AudioManager.Instance.PlayEffect(AudioManager.AudioEffect.Click);
            TutorialManager.Instance.SetFlag(TutorialManager.TutorialFlag.DecidedForPlant);
            SetVisibility(false);
            CInputManager.Instance.SetNavigation(true);
            _callBackMethod?.Invoke();
        }

        private void SetVisibility(bool state)
        {
            if (state) foreach (Image star in images) star.sprite = emptyStar;
            
            _canvasGroup.alpha = state ? 1 : 0;
            _canvasGroup.interactable = _canvasGroup.blocksRaycasts = state;
        }

        private IEnumerator ConsecutiveStarSounds(int stars)
        {
            for (int i = 0; i < stars; i++)
            {
                images[i].sprite = activeStar;
                yield return new WaitForSeconds(AudioManager.Instance.PlayEffect(AudioManager.AudioEffect.StarRating));
                yield return new WaitForSeconds(0.25f);
            }
        }
    }
}