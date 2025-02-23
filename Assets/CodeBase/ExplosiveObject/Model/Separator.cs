using Assets.CodeBase.UserUtils;

namespace Assets.CodeBase.ExplosiveObject.Model
{
    public class Separator
    {
        private float _separationChance;

        private int _minShardsCount;
        private int _maxShardsCount;

        public Separator(float separationChance, int minShardsCount, int maxShardsCount)
        {
            _separationChance = separationChance;
            _minShardsCount = minShardsCount;
            _maxShardsCount = maxShardsCount;
        }

        public bool TrySeparate(out int shardsCount)
        {
            shardsCount = 0;

            if (IsSeparated())
            {
                shardsCount = Randomizer.GetRandomInt(_minShardsCount, _maxShardsCount + 1);

                return true;
            }

            return false;
        }

        private bool IsSeparated()
        {
            return _separationChance > Randomizer.GetRandomFloat();
        }
    }
}
