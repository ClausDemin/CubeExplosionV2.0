using Assets.CodeBase.ExplosiveCubes.Model;
using Assets.CodeBase.ExplosiveCubes.Model.Interfaces;
using Assets.CodeBase.ExplosiveCubes.View.Interfaces;
using Assets.Scripts.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.ExplosiveCubes.Presenter
{
    public class ExplosiveObjectPresenter : IExplosiveObjectPresenter
    {
        private IExplosiveObjectsFactory _objectsFactory;
        private IExploder _exploder;
        private ISeparableEntity _separableEntity;

        public ExplosiveObjectPresenter(IExplosiveObjectsFactory objectsFactory, IExploder exploder, ISeparableEntity separableEntity)
        {
            _objectsFactory = objectsFactory;
            _exploder = exploder;
            _separableEntity = separableEntity;
        }

        public bool TryExplode(IExplosiveObject explosiveObject)
        {
            bool separated = _separableEntity.TrySeparate(out int childCount);
            Vector3 position = explosiveObject.GameObject.transform.position;

            if (separated)
            {
                Placer placer = new Placer();

                Vector3[] positions = placer.GetPositionsFromCubeArea
                (
                    explosiveObject.GameObject.transform.position,
                    explosiveObject.GameObject.transform.localScale,
                    _objectsFactory.ScaleOverGenerationFactor
                );

                UserUtils.Shuffle(positions);

                for (int i = 0; i < childCount; i++)
                {
                    IExplosiveObject child = _objectsFactory.Create(positions[i], Quaternion.identity, _separableEntity.Generation + 1);
                }
            }
            else
            {
                _exploder.Explode(position, GetInvolvedObjects(position, _exploder));
            }

            GameObject.Destroy(explosiveObject.GameObject);

            return separated;
        }

        private List<Rigidbody> GetInvolvedObjects(Vector3 explosionPosition, IExploder exploder)
        {
            List<Rigidbody> involvedObjects = new List<Rigidbody>();

            foreach (Collider collider in Physics.OverlapSphere(explosionPosition, exploder.ExplosionRadius))
            {
                if (collider.gameObject.TryGetComponent(out Rigidbody rigidbody))
                {
                    involvedObjects.Add(rigidbody);
                }
            }

            return involvedObjects;
        }
    }
}
