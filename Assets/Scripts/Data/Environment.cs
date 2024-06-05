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

        public SoilType Soil => soil;
        [SerializeField] private SoilType soil;
        public FertilizerType Fertilizer => fertilizer;
        [SerializeField] private FertilizerType fertilizer;
        public bool Lichtkeimer => lichtkeimer;
        [SerializeField] private bool lichtkeimer;
    }
}