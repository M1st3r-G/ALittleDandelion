using System;

namespace Data
{
    [Serializable]
    public struct Environment
    {
        public enum SoilType
        {
            None, Lehmerde, Sanderde, Rosenerde
        }

        public enum FertilizerType
        {
            Pflanzenjauche, Knochendünger, Pferdedünger
        }

        public SoilType soil;
        public bool lichtkeimer;
    }
}