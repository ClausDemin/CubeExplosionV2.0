using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.ExplosiveObject.Model
{
    public class ExplosiveObject
    {
        private Separator _separator;
        private Exploder _exploder;

        public ExplosiveObject(Separator separator, Exploder exploder, int generation)
        {
            _separator = separator;
            _exploder = exploder;
            Generation = generation;
        }

        public int Generation { get; }

        public bool TrySeparate(out int shardsCount)
        {
            return _separator.TrySeparate(out shardsCount);
        }

        public void Explode(Vector3 position, IEnumerable<Rigidbody> involvedBodies = null)
        {
            _exploder.Explode(position, involvedBodies);
        }
    }
}
