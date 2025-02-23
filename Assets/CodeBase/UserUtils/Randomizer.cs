using System;

namespace Assets.CodeBase.UserUtils
{
    public static class Randomizer
    {
        private static Random s_random = new Random();

        public static int GetRandomInt(int minValue = int.MinValue, int maxValue = int.MaxValue)
        {
            return s_random.Next(minValue, maxValue);
        }

        public static float GetRandomFloat(float minValue = 0, float maxValue = 1)
        {
            return (float)s_random.NextDouble() * (maxValue - minValue) + minValue;
        }
    }
}
