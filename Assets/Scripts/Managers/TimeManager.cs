using UnityEngine;

namespace Managers
{
    public class TimeManager : MonoBehaviour
    {
        //ComponentReferences
        //Params
        //Temps
        public int Days { get; private set; } = 1;
        //Public
        public static TimeManager Instance {get; private set; }

        public delegate void TimeIncreaseDelegate();
        public static TimeIncreaseDelegate OnTimeIncrease;
        
        private void Awake()
        {
            Instance = this;
        }
    
        private void OnDestroy()
        {
            //TODO find a Better way to work with this
            //if (Instance == this) Instance = null;
        }

        public void NextDay()
        {
            Debug.Log("Next Day");
            Days++;
            OnTimeIncrease?.Invoke();
        }
    }
}