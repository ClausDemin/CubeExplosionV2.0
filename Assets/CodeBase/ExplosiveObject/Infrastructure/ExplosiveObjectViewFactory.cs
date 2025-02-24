using Assets.CodeBase.ExplosiveSpore.View;
using Assets.CodeBase.ExplosionFeature.Presenter;
using Assets.CodeBase.ExplosionFeature.View;
using Assets.CodeBase.UserUtils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.ExplosionFeature.Infrastructure
{
    public class ExplosiveObjectViewFactory : MonoBehaviour
    {
        [SerializeField] private ExplosiveObjectView _prefab;

        [Header("Separation preferences")]
        [SerializeField, Range(0, 1)] private float _baseSeparationChance;
        [SerializeField, Range(0, 1)] private float _separationChanceOverGenerationFactor;

        [SerializeField, Min(0)] private int _minShardsCount;
        [SerializeField, Min(0)] private int _maxShardsCount;

        [Header("Explosion preferences")]
        [SerializeField, Min(0)] private float _baseExplosionForce;
        [SerializeField, Min(0)] private float _baseExplosionRadius;

        [SerializeField, Min(0)] private float _explosionForceOverGenerationFactor;
        [SerializeField, Min(0)] private float _explosionRadiusOverGenerationFactor;

        [Header("Scale preferences")]
        [SerializeField, Min(0)] private float _scaleOverGenerationFactor;

        [Header("Initial generation")]
        [SerializeField, Min(0)] private int _initialGeneration;

        [Header("Prewarmed positions")]
        [SerializeField] private List<Vector3> _prewarmedPositions = new List<Vector3>();

        [Header("VFX")]
        [SerializeField] private ExplosionVFX _explosionVFXPrefab;

        private Vector3 _initialScale;

        private ExplosiveObjectPresenterFactory _presenterFactory;
        private ExplosionVFXFactory _explosionVFXFactory;

        private void Awake()
        {
            _initialScale = _prefab.transform.localScale;

            _presenterFactory =
                new ExplosiveObjectPresenterFactory(
                    _baseSeparationChance,
                    _separationChanceOverGenerationFactor,
                    _minShardsCount,
                    _maxShardsCount,
                    _baseExplosionForce,
                    _baseExplosionRadius,
                    _explosionForceOverGenerationFactor,
                    _explosionRadiusOverGenerationFactor);

            _explosionVFXFactory = new ExplosionVFXFactory(_explosionVFXPrefab);
        }

        private void Start()
        {
            foreach (var position in _prewarmedPositions)
            {
                Create(position, _initialGeneration);
            }
        }

        private void OnValidate()
        {
            if (_maxShardsCount < _minShardsCount)
            {
                _maxShardsCount = _minShardsCount;
            }
        }

        public ExplosiveObjectView Create(Vector3 position, int generation)
        {
            ExplosiveObjectView instance = Instantiate(_prefab, position, Quaternion.identity);

            InitInstance(instance, generation);

            return instance;
        }

        public float GetScaleOverGenerationFactor()
        {
            return _scaleOverGenerationFactor;
        }

        private void InitInstance(ExplosiveObjectView instance, int generation)
        {
            ExplosiveObjectPresenter presenter = _presenterFactory.Create(this, _explosionVFXFactory, generation);

            instance.Init(presenter);

            instance.transform.localScale = CalculateScale(generation);

            if (instance.TryGetComponent<MeshRenderer>(out var renderer))
            {
                renderer.material.color = SelectRandomColor();
            }
        }

        private Vector3 CalculateScale(int generation)
        {
            return _initialScale * (float)Math.Pow(_scaleOverGenerationFactor, generation);
        }

        private Color SelectRandomColor()
        {
            return new Color(Randomizer.GetRandomFloat(), Randomizer.GetRandomFloat(), Randomizer.GetRandomFloat());
        }
    }
}
