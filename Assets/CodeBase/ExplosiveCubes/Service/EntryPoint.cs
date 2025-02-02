using Assets.CodeBase.ExplosiveCubes.Model.Infrastructure;
using Assets.CodeBase.ExplosiveCubes.Model.Interfaces;
using Assets.CodeBase.ExplosiveCubes.Presenter.Infrastructure;
using Assets.CodeBase.ExplosiveCubes.View;
using UnityEngine;

namespace Assets.CodeBase.ExplosiveCubes.Service
{
    public class EntryPoint : MonoBehaviour
    {
        [Header("Separable Entity properties")]
        [SerializeField, Range(0, 1)] private float _baseSeparateChance;
        [SerializeField, Range(0, 1)] private float _separateOverGenerationFactor;
        [SerializeField, Range(0, 1)] private float _scaleOverGenerationFactor;
        [SerializeField, Min(1)] private int _minChildCount;
        [SerializeField] private int _maxChildCount;
        [SerializeField, Min(0)] private int _initialGeneration;

        [Header("Explosive object factory")]
        [SerializeField] private ExplosiveObjectsFactory _explosiveObjectsFactory;

        [Header("Exploder properties")]
        [SerializeField, Min(0)] private float _baseExplosionForce;
        [SerializeField, Min(0)] private float _baseExplosionRadius;
        [SerializeField, Range(1, 2)] private float ExplosionForceOverGenerationFactor;
        [SerializeField, Range(1, 2)] private float ExplosionRadiusOverGenerationFactor;

        private void Awake()
        {
            SeparableEntityFactory entityFactory 
                = new SeparableEntityFactory(_baseSeparateChance, _separateOverGenerationFactor, _minChildCount, _maxChildCount);

            IExploderFactory exploderFactory =
                new ExploderFactory(_baseExplosionRadius, _baseExplosionForce, ExplosionForceOverGenerationFactor, ExplosionRadiusOverGenerationFactor);

            _explosiveObjectsFactory
                .Init(entityFactory, exploderFactory, _scaleOverGenerationFactor, _minChildCount, _maxChildCount, _initialGeneration);
        }

        private void OnValidate()
        {
            ValidateChildCount();
        }

        private void ValidateChildCount()
        {
            if (_minChildCount > _maxChildCount)
            {
                _maxChildCount = _minChildCount;
            }
        }
    }
}
