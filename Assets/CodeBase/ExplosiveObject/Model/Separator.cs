using Assets.CodeBase.UserUtils;
using System;

namespace Assets.CodeBase.ExplosionFeature.Model
{
    public class Separator
    {
        private float _baseSeparationChance;
        private float _separationChanceOverGenerationFactor;

        private int _minShardsCount;
        private int _maxShardsCount;

        public Separator(float baseSeparationChance, float separationOverGenerationFactor, int minShardsCount, int maxShardsCount)
        {
            _baseSeparationChance = baseSeparationChance;
            _separationChanceOverGenerationFactor = separationOverGenerationFactor;
            _minShardsCount = minShardsCount;
            _maxShardsCount = maxShardsCount;
        }

        public bool TrySeparate(int generation, out int shardsCount)
        {
            shardsCount = 0;

            if (IsSeparated(generation))
            {
                shardsCount = Randomizer.GetRandomInt(_minShardsCount, _maxShardsCount + 1);

                return true;
            }

            return false;
        }

        private bool IsSeparated(int generation)
        {
            return CalculateSeparationChance(generation) > Randomizer.GetRandomFloat();
        }

        private float CalculateSeparationChance(int generation)
        {
            return _baseSeparationChance * (float)Math.Pow(_separationChanceOverGenerationFactor, generation);
        }
    }
}
