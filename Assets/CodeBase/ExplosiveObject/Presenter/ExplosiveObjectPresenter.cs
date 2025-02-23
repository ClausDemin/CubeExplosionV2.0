using Assets.CodeBase.ExplosiveObject.Extensions;
using Assets.CodeBase.ExplosiveObject.Infrastructure;
using Assets.CodeBase.ExplosiveObject.Model;
using Assets.CodeBase.ExplosiveObject.View;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.CodeBase.ExplosiveObject.Presenter
{
    public class ExplosiveObjectPresenter
    {
        private Model.ExplosiveObject _explosiveObject;
        private ExplosiveObjectFactory _explosiveObjectFactory;
        private ExplosiveObjectViewFactory _explosiveObjectViewFactory;
        private ExplosionVFXFactory _explosionVFXFactory;

        public ExplosiveObjectPresenter(
            ExplosiveObjectFactory explosiveObjectFactory,
            ExplosiveObjectViewFactory viewFactory,
            ExplosionVFXFactory explosionVFXFactory,
            int generation)
        {
            _explosiveObjectFactory = explosiveObjectFactory;
            _explosiveObjectViewFactory = viewFactory;
            _explosionVFXFactory = explosionVFXFactory;

            _explosiveObject = _explosiveObjectFactory.Create(generation);
        }

        public void HandleClick(Vector3 position, Vector3 localScale)
        {
            List<Rigidbody> involvedBodies = new List<Rigidbody>();

            if (_explosiveObject.TrySeparate(out int shardsCount))
            {
                List<ExplosiveObjectView> shards
                    = CreateShards(shardsCount, position, localScale, _explosiveObject.Generation + 1);

                involvedBodies = shards.Select(component => component.GetRigidbody()).ToList();

                _explosiveObject.Explode(position, involvedBodies);
            }
            else
            {
                _explosiveObject.Explode(position);
            }

            _explosionVFXFactory.Create(position);
        }

        private List<ExplosiveObjectView> CreateShards(int shardsCount, Vector3 position, Vector3 localScale, int generation)
        {
            Vector3[] positionsToPlace =
                (Vector3[])PositionCalculator
                .GetPositionsFromCubeArea(position, localScale, _explosiveObjectViewFactory.GetScaleOverGenerationFactor())
                .Shuffle();

            List<ExplosiveObjectView> shards = new();

            for (int i = 0; i < shardsCount; i++)
            {
                shards.Add(_explosiveObjectViewFactory.Create(positionsToPlace[i], generation));
            }

            return shards;
        }
    }
}
