using System.Collections;
using UnityEngine;

namespace Assets.CodeBase.ExplosiveSpore.View
{
    public class ExplosionVFX: MonoBehaviour
    {
        [SerializeField] private ParticleSystem _effects;
        [SerializeField] private float _lifetime;

        private void OnEnable()
        {
            StartCoroutine(Destruct());
        }

        private IEnumerator Destruct() 
        {
            yield return new WaitForSeconds(_lifetime);

            Destroy(gameObject);
        }
    }
}
