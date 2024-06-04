using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Data/FlowerData")]
    public class FlowerData : ScriptableObject
    {
        public enum FlowerType
        {
            Löwenzahn, Tulpe, Rose
        }
        
        public FlowerType FlowerName => flowerTypes; 
        [SerializeField] private FlowerType flowerTypes;

        public Environment PreferredEnvironment => preferredEnv;
        [SerializeField] private Environment preferredEnv;

        public int TimeToSprout => timeToSprout;
        [SerializeField] private int timeToSprout;

        public int TimeToBloom => timeToBloom;
        [SerializeField] private int timeToBloom;

        public int TimeToDie => timeToDie;
        [SerializeField] private int timeToDie;
        
        public int WaterFrequency => waterFrequency;
        [SerializeField] private int waterFrequency;
    }
}