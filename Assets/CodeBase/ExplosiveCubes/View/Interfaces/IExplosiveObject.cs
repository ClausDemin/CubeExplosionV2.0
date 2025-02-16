using Assets.CodeBase.ExplosiveCubes.Presenter;
using System;
using UnityEngine;

namespace Assets.CodeBase.ExplosiveCubes.View.Interfaces
{
    public interface IExplosiveObject
    {
        public GameObject GameObject { get; }
        public void Init(IExplosiveObjectPresenter presenter);
    }
}
