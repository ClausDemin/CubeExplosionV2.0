using UnityEngine;

namespace Assets.CodeBase.ExplosiveCubes.View.Interfaces
{
    public interface IExplosiveObjectsFactory
    {
        public float ScaleOverGenerationFactor { get; }

        public IExplosiveObject Create(Vector3 position, Quaternion rotation, int generation);
    }
}
