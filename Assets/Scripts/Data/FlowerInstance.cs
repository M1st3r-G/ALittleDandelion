using Managers;

namespace Data
{
    public class FlowerInstance
    {
        public enum GrowthState
        {
            Seed, Sprout, Flower, Dead
        }

        public FlowerData Type => _type;
        private readonly FlowerData _type;   // The Type of Flower
        public GrowthState State => _state;
        private GrowthState _state; // The GrowthState
        private int _lastWater;     // Time Since Watering
        private int _growthCounter; // How Many Days Past since start of Phase
        public float Rating => _rating;
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

        private static int CalculatePenalty(FlowerData flower, Environment potEnv)
        {
            int counter = 0;
            if (flower.PreferredEnvironment.Lichtkeimer != potEnv.Lichtkeimer) counter += 10;
            if (flower.HatedSoil == potEnv.Soil) counter += 30;
            else if (flower.PreferredEnvironment.Soil != potEnv.Soil) counter += 10;
            return counter;
        }

        /// <summary>
        /// Only if Plant is Sprout
        /// </summary>
        /// <param name="fertilizer">The Fertilizer in the New Environment</param>
        public void Replant(Environment.FertilizerType fertilizer)
        {
            _isReplant = true;
            if (fertilizer != _type.Fertilizer) _rating -= 10;
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
                        if (!_isReplant)
                        {
                            _rating -= (_growthCounter - _type.TimeToBloom) * 5;
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
            if (_lastWater < _type.WaterFrequency)  _rating -= (_type.WaterFrequency - _lastWater) * 5;
            _lastWater = 0;
        }

        public override string ToString()
        {
            return $"Diese {_type.FlowerName} ist ein(e) {_state}\nRanking: {_rating}\nLW:{_lastWater};GC:{_growthCounter}";
        }
    }
}