using System;
using Data;
using UnityEngine;
using UnityEngine.UI;
using Environment = Data.Environment;

namespace UI
{
    public class PotButtons : MonoBehaviour
    {
        private Button[] _buttons;
        
        private void Awake()
        {
            _buttons = GetComponentsInChildren<Button>();

            foreach (Button b in _buttons)
            {
                b.gameObject.SetActive(false);
            }
        }

        public void SetAmountActive(Tuple<FlowerInstance, Environment>[] flowers)
        {
            for (var i = 0; i < flowers.Length; i++)
            {
                _buttons[i].gameObject.SetActive(true);
            }
            
            for (var i = flowers.Length; i < 12; i++)
            {
                _buttons[i].gameObject.SetActive(false);
            }
        }
    }
}