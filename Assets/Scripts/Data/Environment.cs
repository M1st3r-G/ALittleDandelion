using System;

namespace Data
{
    [Serializable]
    public struct Environment
    {
        public enum SoilType
        {
            Lehmerde, Sanderde, Rosenerde
        }

        public enum FertilizerType
        {
            Pflanzenjauche, Knochendünger, Pferdedünger
        }
        
        private SoilType _soil;
        private FertilizerType _fertilizer;
        private bool _lichtkeimer;

        public int Compare(Environment other)
        {
            int counter = 0;
            if (_soil == other._soil) counter++;
            if (_fertilizer == other._fertilizer) counter++;
            if (_lichtkeimer == other._lichtkeimer) counter++;
            return counter;
        }
    }
}