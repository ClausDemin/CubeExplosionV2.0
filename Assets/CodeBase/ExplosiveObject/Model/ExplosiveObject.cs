using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.ExplosionFeature.Model
{
    public class ExplosiveObject
    {
        public ExplosiveObject(int generation)
        {
            Generation = generation;
        }

        public int Generation { get; }
    }
}
