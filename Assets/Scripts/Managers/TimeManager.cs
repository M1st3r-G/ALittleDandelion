using UnityEngine;

namespace Managers
{
    public class TimeManager : MonoBehaviour
    {
        //ComponentReferences
        //Params
        //Temps
        public int Days { get; private set; }
        //Public
        public static TimeManager Instance {get; private set; }

        public delegate void TimeIncreaseDelegate();
        public static TimeIncreaseDelegate OnTimeIncrease;
        
        private void Awake()
        {
            Instance = this;
            Days = SaveGameManager.Instance.GetTimeData();
        }

        public void NextDay()
        {
            Debug.Log("Next Day");
            AudioManager.Instance.PlayEffect(AudioManager.AudioEffect.NextDay);
            TutorialManager.Instance.SetFlag(TutorialManager.TutorialFlag.NextDay);
            Days++;
            OnTimeIncrease?.Invoke();
        }
    }
}