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

        public enum SoilType
        {
            Lehmerde, Sanderde, Rosenerde
        }

        public enum FertilizerType
        {
            Pflanzenjauche, Knochendünger, Pferdedünger
        }
        
        public FlowerType FlowerName => flowerTypes; 
        [SerializeField] private FlowerType flowerTypes;

        public bool IsLichtkeimer => lichtkeimer;
        [SerializeField] private bool lichtkeimer;

        public SoilType Soil => soilType;
        [SerializeField] private SoilType soilType;

        public FertilizerType Fertilizer => fertilizerType;
        [SerializeField] private FertilizerType fertilizerType;

        public int TimeToSprout => timeToSprout;
        [SerializeField] private int timeToSprout;

        public int TimeToBloom => timeToBloom;
        [SerializeField] private int timeToBloom;

        public int WaterFrequency => waterFrequency;
        [SerializeField] private int waterFrequency;
    }
}