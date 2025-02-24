using Assets.CodeBase.ExplosionFeature.Extensions;
using Assets.CodeBase.ExplosionFeature.Infrastructure;
using Assets.CodeBase.ExplosionFeature.Model;
using Assets.CodeBase.ExplosionFeature.View;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.CodeBase.ExplosionFeature.Presenter
{
    public class ExplosiveObjectPresenter
    {
        private ExplosiveObject _explosiveObject;
        private Separator _separator;
        private Exploder _exploder;

        private ExplosiveObjectViewFactory _explosiveObjectViewFactory;
        private ExplosionVFXFactory _explosionVFXFactory;

        public ExplosiveObjectPresenter(
            ExplosiveObject explosiveObject,
            Separator separator,
            Exploder exploder,
            ExplosiveObjectViewFactory viewFactory,
            ExplosionVFXFactory explosionVFXFactory)
        {
            _explosiveObjectViewFactory = viewFactory;
            _explosionVFXFactory = explosionVFXFactory;

            _explosiveObject = explosiveObject;
            _separator = separator;
            _exploder = exploder;
        }

        public void HandleClick(Vector3 position, Vector3 localScale)
        {
            List<Rigidbody> involvedBodies = null;

            int generation = _explosiveObject.Generation;

            if (_separator.TrySeparate(generation, out int shardsCount))
            {
                List<ExplosiveObjectView> shards
                    = CreateShards(shardsCount, position, localScale, generation + 1);

                involvedBodies = shards.Select(component => component.Rigidbody).ToList();
            }

            _exploder.Explode(generation, position);

            _explosionVFXFactory.Create(position);
        }

        private List<ExplosiveObjectView> CreateShards(int shardsCount, Vector3 position, Vector3 localScale, int generation)
        {
            Vector3[] positionsToPlace =
                PositionCalculator
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
