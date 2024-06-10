using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Data/Page")]
    public class PageData : ScriptableObject
    {
        public string Title => title;
        [SerializeField] private string title;
        public Sprite Sketch => sketch;
        [SerializeField] private Sprite sketch;
        public Sprite Image => image;
        [SerializeField] private Sprite image;
        public string Hint(int i) => hints[i];
        [SerializeField] private string[] hints = new string[5];
    }
}