using System;
using UnityEngine;

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
        
        [SerializeField] private SoilType soil;
        [SerializeField] private FertilizerType fertilizer;
        [SerializeField] private bool lichtkeimer;

        public int Compare(Environment other)
        {
            int counter = 0;
            if (soil == other.soil) counter++;
            if (fertilizer == other.fertilizer) counter++;
            if (lichtkeimer == other.lichtkeimer) counter++;
            return counter;
        }
    }
}