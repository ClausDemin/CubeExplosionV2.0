using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.CodeBase.ExplosionFeature.Model
{
    public class Exploder
    {
        private float _baseExplosionForce;
        private float _baseExplosionRadius;

        private float _explosionForceOverGenerationFactor;
        private float _explosionRadiusOverGenerationFactor;

        public Exploder(
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

        public void Explode(int generation, Vector3 epicenterPosition, IEnumerable<Rigidbody> involvedBodies = null)
        {
            if (involvedBodies == null)
            {
                involvedBodies = FindInvolvedBodies(generation, epicenterPosition);
            }

            if (involvedBodies != null)
            {
                foreach (Rigidbody body in involvedBodies)
                {
                    body.AddExplosionForce(CalculateExplosionForce(generation), epicenterPosition, CalculateExplosionRadius(generation));
                }
            }
        }

        private IEnumerable<Rigidbody> FindInvolvedBodies(int generation, Vector3 position)
        {
            return Physics.OverlapSphere(position, CalculateExplosionRadius(generation))
                .Select(collider => collider.attachedRigidbody)
                .Where(rigidbody => rigidbody != null);
        }

        private float CalculateExplosionForce(int generation)
        {
            return _baseExplosionForce * (float)Math.Pow(_explosionForceOverGenerationFactor, generation);
        }

        private float CalculateExplosionRadius(int generation)
        {
            return _baseExplosionRadius * (float)Math.Pow(_explosionRadiusOverGenerationFactor, generation);
        }
    }
}
