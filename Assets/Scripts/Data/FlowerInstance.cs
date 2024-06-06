using System.Linq.Expressions;
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

        public bool IsReplantable => _state == GrowthState.Sprout;
        
        private readonly FlowerData _type;  // The Type of Flower
        private GrowthState _state;         // The GrowthState
        private int _lastWater;             // Time Since Watering
        private int _growthCounter;         // How Many Days Past since start of Phase

        private int Rating
        {
            get => _rating;
            set
            {
                _rating = value;
                if (_rating <= 50) _state = GrowthState.Dead;
            }
        }
        private int _rating;

        private bool _isReplant;
        
        public FlowerInstance(FlowerData type, Environment potEnv)
        {
            _type = type;
            _state = GrowthState.Seed;
            _lastWater = _growthCounter = 0;
            _isReplant = false;

            _rating = 100 - CalculatePenalty(_type, potEnv);
            
            TimeManager.OnTimeIncrease += Grow;
        }

        public int CalculateStars() => _rating switch
        {
            >= 90 => 3, 
            >= 75 => 2, 
            >= 50 => 1, 
            _ => 0
        };
        
        private static int CalculatePenalty(FlowerData flower, Environment potEnv)
        {
            int counter = 0;
            if (flower.PreferredEnvironment.lichtkeimer != potEnv.lichtkeimer) counter += 10;
            if (flower.HatedSoil == potEnv.soil) counter += 30;
            else if (flower.PreferredEnvironment.soil != potEnv.soil) counter += 10;
            return counter;
        }

        /// <summary>
        /// Only if Plant is Sprout
        /// </summary>
        /// <param name="fertilizer">The Fertilizer in the New Environment</param>
        public void Replant(Environment.FertilizerType fertilizer)
        {
            Debug.Log($"{_type.FlowerName} Got replant with {fertilizer} as Fertilizer");
            _isReplant = true;
            if (fertilizer != _type.Fertilizer) Rating -= 10;
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
            if (_lastWater > _type.WaterFrequency) Rating -= (_lastWater - _type.WaterFrequency) * 5;
            
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
                        if (!_isReplant)
                        {
                            Rating -= (_growthCounter - _type.TimeToBloom) * 5;
                        }
                        else
                        {
                            _state = GrowthState.Flower;
                            _growthCounter = 0;
                        }
                    }
                    break;
                case GrowthState.Flower:
                    break;
                case GrowthState.Dead:
                default:
                    _rating = 0;
                    TimeManager.OnTimeIncrease -= Grow;
                    break;
            }
        }

        /// <summary>
        /// Waters the Plant
        /// </summary>
        public void Water()
        {
            // To Wet -> Takes Damage
            if (_lastWater < _type.WaterFrequency)  Rating -= (_type.WaterFrequency - _lastWater) * 5;
            _lastWater = 0;
        }

        public override string ToString()
        {
            return $"Diese {_type.FlowerName} ist ein(e) {_state}\nRanking: {_rating}\nLW:{_lastWater};GC:{_growthCounter}";
        }
    }
}