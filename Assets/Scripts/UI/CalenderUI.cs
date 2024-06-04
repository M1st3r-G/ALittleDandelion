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
            numberOfDaysText.text = "0";
        }

        private void OnTimeIncrease()
        {
            numberOfDaysText.text = TimeManager.Instance.Days.ToString();
        }
    }
}