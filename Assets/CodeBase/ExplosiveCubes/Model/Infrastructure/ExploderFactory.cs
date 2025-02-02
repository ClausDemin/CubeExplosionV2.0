using Assets.CodeBase.ExplosiveCubes.Model.Interfaces;
using System;

namespace Assets.CodeBase.ExplosiveCubes.Model.Infrastructure
{
    public class ExploderFactory: IExploderFactory
    {
        private float _baseExplosionRadius;
        private float _baseExplosionForce;

        private float _explosionForceOverGenerationFactor;
        private float _explosionRadiusOverGenerationFactor;

        public ExploderFactory
        (
            float baseExplosionRadius, 
            float baseExplosionForce, 
            float explosionForceOverGenerationFactor, 
            float explosionRadiusOverGenerationFactor
        ) 
        { 
            _baseExplosionRadius = baseExplosionRadius;
            _baseExplosionForce = baseExplosionForce;
            _explosionForceOverGenerationFactor = explosionForceOverGenerationFactor;
            _explosionRadiusOverGenerationFactor = explosionRadiusOverGenerationFactor;
        }

        public IExploder Create(int generation) 
        {
            float explosionForce = _baseExplosionForce * (float) Math.Pow(_explosionForceOverGenerationFactor, generation);
            float explosionRadius = _baseExplosionRadius * (float)Math.Pow(_explosionRadiusOverGenerationFactor, generation);

            return new Exploder(explosionForce, explosionRadius);
        }
    }
}
