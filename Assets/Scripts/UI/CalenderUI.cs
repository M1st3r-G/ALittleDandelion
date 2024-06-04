using Managers;
using TMPro;
using UnityEngine;

namespace UI
{
    public class CalenderUI : MonoBehaviour
    {
        //ComponentReferences
        [SerializeField] private TextMeshProUGUI numberOfDaysText;
        //Params
        //Temps
        //Public
     
        private void Awake()
        {
            TimeManager.OnTimeIncrease += OnTimeIncrease;
        }

        private void OnTimeIncrease()
        {
            numberOfDaysText.text = TimeManager.Instance.Days.ToString();
        }
    }
}