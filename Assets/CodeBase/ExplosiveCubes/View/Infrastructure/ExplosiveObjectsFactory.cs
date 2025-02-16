using Assets.CodeBase.ExplosiveCubes.Interfaces;
using Assets.CodeBase.ExplosiveCubes.Model;
using Assets.CodeBase.ExplosiveCubes.Model.Interfaces;
using Assets.CodeBase.ExplosiveCubes.Presenter;
using Assets.CodeBase.ExplosiveCubes.Presenter.Infrastructure;
using Assets.CodeBase.ExplosiveCubes.Presenter.Interfaces;
using Assets.CodeBase.ExplosiveCubes.View.Interfaces;
using Assets.Scripts.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.ExplosiveCubes.View
{
    public class ExplosiveObjectsFactory : MonoBehaviour, IExplosiveObjectsFactory, INeedInitialization
    {
        [SerializeField] private ExplosiveObject _prefab;

        [SerializeField] private List<Vector3> _prewarmedPositions = new List<Vector3>();

        private IExplosiveObjectPresenterFactory _presenterFactory;
        private ISeparableEntityFactory _entityFactory;
        private IExploderFactory _exploderFactory;

        private int _minChildCount;
        private int _maxChildCount;

        private Vector3 _baseScale;
        private int _initialGeneration;

        public bool IsInitialized { get; private set; }
        public float ScaleOverGenerationFactor { get; private set; }

        private void Start()
        {
            _baseScale = _prefab.gameObject.transform.localScale;

            CreatePrewarmedInstances();
        }

        private void CreatePrewarmedInstances()
        {
            if (IsInitialized && _prewarmedPositions.Count > 0)
            {
                foreach (var position in _prewarmedPositions)
                {
                    Create(position, _prefab.transform.rotation, _initialGeneration);
                }
            }
        }

        public void Init
        (
            ISeparableEntityFactory entityFactory,
            IExploderFactory exploderFactory,
            float scaleOverGenerationFactor,
            int minChildCount,
            int maxChildCount,
            int initialGeneration
        )
        {
            _entityFactory = entityFactory;
            _presenterFactory = new ExplosiveObjectPresenterFactory();
            _exploderFactory = exploderFactory;

            _minChildCount = minChildCount;
            _maxChildCount = maxChildCount;

            _initialGeneration = initialGeneration;

            ScaleOverGenerationFactor = scaleOverGenerationFactor;
            IsInitialized = true;
        }

        public IExplosiveObject Create(Vector3 position, Quaternion rotation, int generation)
        {
            if (IsInitialized)
            {
                GameObject instance = Instantiate(_prefab.gameObject, position, rotation);

                IExploder exploder = _exploderFactory.Create(generation);

                if (instance.TryGetComponent<IExplosiveObject>(out var explosiveObject))
                {
                    ISeparableEntity separableEntity = _entityFactory.Create(generation);

                    IExplosiveObjectPresenter presenter = _presenterFactory.Create(this, separableEntity, exploder);

                    explosiveObject.Init(presenter);

                    explosiveObject.GameObject.transform.localScale = _baseScale * (float)Math.Pow(ScaleOverGenerationFactor, generation);

                    return explosiveObject;
                }

                Destroy(instance);
            }

            return default;
        }
    }
}
