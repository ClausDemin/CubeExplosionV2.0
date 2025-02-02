using Assets.CodeBase.ExplosiveCubes.Presenter;
using System;
using UnityEngine;

namespace Assets.CodeBase.ExplosiveCubes.View.Interfaces
{
    public interface IExplosiveObject
    {
        public GameObject gameObject { get; }
        public void Init(IExplosiveObjectPresenter presenter);
    }
}
