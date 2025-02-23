using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.CodeBase.ExplosiveObject.Model
{
    public class Exploder
    {
        private float _explosionForce;
        private float _explosionRadius;

        public Exploder(float force, float radius)
        {
            _explosionForce = force;
            _explosionRadius = radius;
        }

        public void Explode(Vector3 epicenterPosition, IEnumerable<Rigidbody> involvedBodies = null)
        {
            if (involvedBodies == null)
            {
                involvedBodies = FindInvolvedBodies(epicenterPosition);
            }

            if (involvedBodies != null)
            {
                foreach (Rigidbody body in involvedBodies)
                {
                    body.AddExplosionForce(_explosionForce, epicenterPosition, _explosionRadius);
                }
            }
        }

        private IEnumerable<Rigidbody> FindInvolvedBodies(Vector3 position)
        {
            return Physics.OverlapSphere(position, _explosionRadius)
                .Select(collider => collider.attachedRigidbody)
                .Where(rigidbody => rigidbody != null);
        }
    }
}
