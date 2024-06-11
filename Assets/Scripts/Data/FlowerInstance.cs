using Managers;
using UnityEngine;

namespace Data
{
    public class FlowerInstance
    {
        #region SetUpAndFields

        public enum GrowthState
        {
            Seed, Sprout, Bloom, Flower, Dead
        }
        
        // State

        public FlowerData.FlowerType Type => _type.FlowerName;
        private readonly FlowerData _type;  // The Type of Flower
        public GrowthState State => _state;
        private GrowthState _state;         // The GrowthState
        private int _lastWater;             // Time Since Watering
        private int _growthCounter;         // How Many Days Past since start of Phase
        private int _rating;
        private bool _isReplant;
        
        // Public
        public bool IsReplantable => _state == GrowthState.Sprout;

        public int CalculateStars() => _rating switch
        {
            >= 90 => 3, 
            >= 75 => 2, 
            >= 50 => 1, 
            _ => 0
        };
        
        public delegate void OnChangeEvent();
        public OnChangeEvent OnChange;

        public FlowerInstance(FlowerData type, Environment potEnv)
        {
            _type = type;
            _state = GrowthState.Seed;
            _lastWater = _growthCounter = 0;
            _isReplant = false;

            _rating = 100 - CalculatePenalty(_type, potEnv);
            
            TimeManager.OnTimeIncrease += Grow;
        }

        #endregion
        
        #region StateChange

        public void Replant(Environment.FertilizerType fertilizer)
        {
            Debug.Log($"{_type.FlowerName} Got replant with {fertilizer} as Fertilizer");
            _isReplant = true;
            if (fertilizer != _type.Fertilizer) DecreaseRating(10);
            
            OnChange?.Invoke();
        }
        
        private void Grow()
        {
            // Increase internal Timers
            _growthCounter++;
            _lastWater++;
            
            // If too dry, The Plant takes Damage
            if (_lastWater > _type.WaterFrequency) DecreaseRating((_lastWater - _type.WaterFrequency) * 5);
            
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
                    if (_growthCounter >= _type.TimeToBloom / 2)
                    {
                        if (!_isReplant)
                        {
                            DecreaseRating((_growthCounter - _type.TimeToBloom / 2) * 5);
                        }
                        else
                        {
                            _state = GrowthState.Bloom;
                        }
                    }
                    break;
                case GrowthState.Bloom:
                    if (_growthCounter >= _type.TimeToBloom)
                    {
                        _state = GrowthState.Flower;
                        TimeManager.OnTimeIncrease -= Grow;
                    }
                    break;
                case GrowthState.Flower:
                    TimeManager.OnTimeIncrease -= Grow;
                    break;
                case GrowthState.Dead:
                default:
                    _rating = 0;
                    TimeManager.OnTimeIncrease -= Grow;
                    break;
            }
            
            OnChange?.Invoke();
        }

        public void Water()
        {
            if (_state == GrowthState.Dead) return;
            
            // To Wet -> Takes Damage
            if (_lastWater < _type.WaterFrequency) DecreaseRating((_type.WaterFrequency - _lastWater) * 5);
            _lastWater = 0;
            
            OnChange?.Invoke();
        }

        #endregion

        #region Utility

        private void DecreaseRating(int amount)
        {
            _rating -= amount;
            if (_rating <= 50) _state = GrowthState.Dead;
        }
        
        private static int CalculatePenalty(FlowerData flower, Environment potEnv)
        {
            int counter = 0;
            if (flower.PreferredEnvironment.lichtkeimer != potEnv.lichtkeimer) counter += 10;
            if (flower.HatedSoil == potEnv.soil) counter += 30;
            else if (flower.PreferredEnvironment.soil != potEnv.soil) counter += 10;
            return counter;
        }
        
        public override string ToString()
        {
            if (_state == GrowthState.Flower)
            {
                return $"Ein(e) {CalculateStars()}-Sterne {_type.FlowerName}";
            }

            string umgetopft = _isReplant ? "umgetopfte " : "";
            return $"Diese(r) {_type.FlowerName} ist ein(e) {umgetopft}{_state}\nRanking: {_rating}\nLW:{_lastWater};GC:{_growthCounter}";
        }

        #endregion
    }
}