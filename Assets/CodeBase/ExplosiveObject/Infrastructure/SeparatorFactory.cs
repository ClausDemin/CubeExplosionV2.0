using Assets.CodeBase.ExplosiveObject.Model;
using System;

namespace Assets.CodeBase.ExplosiveObject.Infrastructure
{
    public class SeparatorFactory
    {
        private float _baseSeparationChance;
        private float _separationChanceOverGenerationFactor;

        private int _minShardsCount;
        private int _maxShardsCount;

        public SeparatorFactory(
            float baseSeparationChance,
            float separationChanceOverGenerationFactor,
            int minShardsCount,
            int maxShardsCount)
        {
            _baseSeparationChance = baseSeparationChance;
            _separationChanceOverGenerationFactor = separationChanceOverGenerationFactor;
            _minShardsCount = minShardsCount;
            _maxShardsCount = maxShardsCount;
        }

        public Separator Create(int generation = 0)
        {
            return new Separator(CalculateSeparationChance(generation), _minShardsCount, _maxShardsCount);
        }

        private float CalculateSeparationChance(int generation)
        {
            return _baseSeparationChance * (float)Math.Pow(_separationChanceOverGenerationFactor, generation);
        }
    }
}
