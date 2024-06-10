using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controller.Book
{
    public class PageController : MonoBehaviour
    {
        // Component References
        [SerializeField] private TextMeshProUGUI[] hints = new TextMeshProUGUI[5];
        [SerializeField] private Image big;
        [SerializeField] private Image grey;
        [SerializeField] private TextMeshProUGUI title;

        //UnlockValue: I5|I4|I3|I2|I1|S
        public void ShowPage(PageData page, int unlockValue)
        {
            title.text = page.Title;
            if (unlockValue % 2 == 0)
            {
                big.sprite = page.Sketch;
                grey.gameObject.SetActive(false);
            }
            else
            {
                big.sprite = page.Image;
                grey.gameObject.SetActive(true);
                grey.sprite = page.Sketch;
            }

            foreach (TextMeshProUGUI hint in hints)
            {
                hint.gameObject.SetActive(false);
            }
            
            unlockValue >>= 1;
            int c = 0;
            while (unlockValue > 0)
            {
                if (unlockValue % 2 == 1)
                {
                    hints[c].gameObject.SetActive(true);
                    hints[c].text = page.Hint(c);
                }
                else
                {
                    Debug.Log("skipped");
                    
                }

                c += 1;
                unlockValue >>= 1;
            }
        }
    }
}