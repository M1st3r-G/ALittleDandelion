using System;
using Managers;
using UnityEngine;

namespace Data
{
    public class FlowerInstance
    {
        private enum GrowthState
        {
            Seed, Sprout, Flower, Dead
        }
        
        private readonly FlowerData _type;   // The Type of Flower
        private GrowthState _state; // The GrowthState
        private int _lastWater;     // Time Since Watering
        private int _damage;        // When Watered Wrong the plant dies
        private int _growthCounter; // How Many Days Past since start of Phase

        public FlowerInstance(FlowerData type)
        {
            _type = type;
            _lastWater = _damage = _growthCounter = 0;
            _state = GrowthState.Seed;
            
            TimeManager.OnTimeIncrease += Grow;
        }

        public float Rating(Environment potEnvironment)
        {
            // 0 Worst, 3 Best;
            int envPoints = potEnvironment.Compare(_type.PreferredEnvironment);
            
            // 0 Worst, 5 Best;
            int damagePoints = 5 - Mathf.Clamp(_damage, 0, 5);
            
            // 0 Worst, 3 Best
            int growthPoints = (int)_state;
            if (_state is GrowthState.Dead) growthPoints = 0;

            return (envPoints + growthPoints + damagePoints) / 11f;
        }
        
        /// <summary>
        /// As Long as the Plant object exist, this is called at the Start of each day
        /// </summary>
        private void Grow()
        {
            // Increase internal Timers
            _growthCounter++;
            _lastWater++;
            
            // If too dry, The Plant takes Damage
            if (_lastWater > _type.WaterFrequency) _damage++;
            
            // Dies with to much Damage
            if (Mathf.Abs(_damage) > 5) _state = GrowthState.Dead;
            
            // Grows
            switch (_state)
            {
                case GrowthState.Seed:
                    if (_growthCounter >= _type.TimeToSprout)
                    {
                        _state = GrowthState.Sprout;
                        _growthCounter = 0;
                    }
                    break;
                case GrowthState.Sprout:
                    if (_growthCounter >= _type.TimeToBloom)
                    {
                        _state = GrowthState.Flower;
                        _growthCounter = 0;
                    }
                    break;
                case GrowthState.Flower:
                    if (_growthCounter >= _type.TimeToDie)
                    {
                        _state = GrowthState.Dead;
                    }
                    break;
                case GrowthState.Dead:
                default:
                    _state = GrowthState.Dead;
                    break;
            }
        }

        /// <summary>
        /// Waters the Plant
        /// </summary>
        public void Water()
        {
            // To Wet -> Takes Damage
            if (_lastWater < _type.WaterFrequency) _damage++;
            _lastWater = 0;
        }
    }
}