using UnityEngine;

namespace Managers
{
    public class TimeManager : MonoBehaviour
    {
        //ComponentReferences
        //Params
        //Temps
        public int Days => _days;
        private int _days = 1;
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
            if (Instance == this) Instance = null;
        }

        public void NextDay()
        {
            Debug.Log("Next Day");
            _days++;
            OnTimeIncrease?.Invoke();
        }
    }
}