using Managers;
using TMPro;
using UnityEngine;

namespace UI
{
    public class CalenderUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI numberOfDaysText;
        
        private void Awake()
        {
            TimeManager.OnTimeIncrease += OnTimeIncrease;
            numberOfDaysText.text = SaveGameManager.Instance.GetTimeData().ToString();
        }

        private void OnTimeIncrease() => numberOfDaysText.text = TimeManager.Instance.Days.ToString();
    }
}