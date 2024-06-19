using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Data/FlowerData")]
    public class FlowerData : ScriptableObject
    {
        public enum FlowerType
        {
            Löwenzahn, Tulpe, Rose, Lilie, Hyazinthe, Santini, WeißerLöwenzahn
        }
        
        public FlowerType FlowerName => flowerTypes; 
        [SerializeField] private FlowerType flowerTypes;

        public Environment PreferredEnvironment => preferredEnv;
        [SerializeField] private Environment preferredEnv;

        public Environment.FertilizerType Fertilizer => fertilizer;
        [SerializeField] private Environment.FertilizerType fertilizer;
        
        public Environment.SoilType HatedSoil => hSoil;
        [SerializeField] private Environment.SoilType hSoil;
        
        public int TimeToSprout => timeToSprout;
        [SerializeField] private int timeToSprout;

        public int TimeToBloom => timeToBloom;
        [SerializeField] private int timeToBloom;

        public int WaterFrequency => waterFrequency;
        [SerializeField] private int waterFrequency;
    }
}