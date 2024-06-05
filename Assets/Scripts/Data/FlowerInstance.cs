using Managers;

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
        private int _growthCounter; // How Many Days Past since start of Phase
        public float Rating => _rating;
        private int _rating;
        
        public FlowerInstance(FlowerData type, Environment potEnv)
        {
            _type = type;
            _state = GrowthState.Seed;
            _lastWater = _growthCounter = 0;

            _rating = 100 - CalculatePenalty(_type, potEnv);
            
            TimeManager.OnTimeIncrease += Grow;
        }

        private static int CalculatePenalty(FlowerData flower, Environment potEnv)
        {
            int counter = 0;
            if (flower.PreferredEnvironment.Lichtkeimer != potEnv.Lichtkeimer) counter += 10;
            if (flower.PreferredEnvironment.Fertilizer != potEnv.Fertilizer) counter += 10;
            if (flower.HatedSoil == potEnv.Soil) counter += 30;
            else if (flower.PreferredEnvironment.Soil != potEnv.Soil) counter += 10;
            return counter;
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
            if (_lastWater > _type.WaterFrequency) _rating -= (_lastWater - _type.WaterFrequency) * 5;
            
            // Dies with too much Damage
            if (_rating <= 0) _state = GrowthState.Dead;
            
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
            if (_lastWater < _type.WaterFrequency)  _rating -= (_type.WaterFrequency - _lastWater) * 5;
            _lastWater = 0;
        }
    }
}