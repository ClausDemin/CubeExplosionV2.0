using Assets.CodeBase.ExplosiveObject.Model;
using System;

namespace Assets.CodeBase.ExplosiveObject.Infrastructure
{
    public class ExploderFactory
    {
        private float _baseExplosionForce;
        private float _baseExplosionRadius;

        private float _explosionForceOverGenerationFactor;
        private float _explosionRadiusOverGenerationFactor;

        public ExploderFactory(
            float baseExplosionForce,
            float baseExplosionRadius,
            float explosionForceOverGenerationFactor,
            float explosionRadiusOverGenerationFactor)
        {
            _baseExplosionForce = baseExplosionForce;
            _baseExplosionRadius = baseExplosionRadius;
            _explosionForceOverGenerationFactor = explosionForceOverGenerationFactor;
            _explosionRadiusOverGenerationFactor = explosionRadiusOverGenerationFactor;
        }

        public Exploder Create(int generation = 0) 
        {
            return new Exploder(CalculateExplosionForce(generation), CalculateExplosionRadius(generation));
        }

        private float CalculateExplosionForce(int generation) 
        { 
            return _baseExplosionForce * (float) Math.Pow(_explosionForceOverGenerationFactor, generation);
        }

        private float CalculateExplosionRadius(int generation)
        {
            return _baseExplosionRadius * (float)Math.Pow(_explosionRadiusOverGenerationFactor, generation);
        }
    }
}
