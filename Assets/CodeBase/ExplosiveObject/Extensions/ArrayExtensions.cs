using Assets.CodeBase.UserUtils;
using System;

namespace Assets.CodeBase.ExplosiveObject.Extensions
{
    public static class ArrayExtensions
    {
        public static Array Shuffle(this Array array)
        {
            for (int i = array.Length - 1; i > 0; i--)
            {
                int indexToSwap = Randomizer.GetRandomInt(0, i);

                var buffer = array.GetValue(indexToSwap);
                var elementToSwap = array.GetValue(i);

                array.SetValue(elementToSwap, indexToSwap);
                array.SetValue(buffer, i);
            }

            return array;
        }
    }
}
