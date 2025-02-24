using Assets.CodeBase.ExplosiveSpore.View;
using UnityEngine;

namespace Assets.CodeBase.ExplosionFeature.View
{
    public class ExplosionVFXFactory
    {
        private ExplosionVFX _prefab;

        public ExplosionVFXFactory(ExplosionVFX prefab)
        {
            _prefab = prefab;
        }

        public ExplosionVFX Create(Vector3 position)
        {
            return GameObject.Instantiate(_prefab, position, Quaternion.identity);
        }
    }
}
