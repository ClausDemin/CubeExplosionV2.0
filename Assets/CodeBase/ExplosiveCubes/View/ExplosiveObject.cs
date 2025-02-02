using Assets.CodeBase.ExplosiveCubes.Interfaces;
using Assets.CodeBase.ExplosiveCubes.Presenter;
using Assets.CodeBase.ExplosiveCubes.View.Interfaces;
using Assets.CodeBase.ExplosiveSpore.View;
using UnityEngine;

namespace Assets.CodeBase.ExplosiveCubes.View
{
    public class ExplosiveObject : MonoBehaviour, IExplosiveObject, INeedInitialization
    {
        [SerializeField] private ExplosionVFX _explosionVFX;
        
        private IExplosiveObjectPresenter _presenter;

        public bool IsInitialized { get; private set; }

        private void OnMouseUpAsButton()
        {
            OnClicked();
        }

        private void OnClicked()
        {
            if (IsInitialized)
            {
                if (_presenter.TryExplode(this) == false)
                {
                    Instantiate(_explosionVFX.gameObject, transform.position, Quaternion.identity);
                }
            }
        }

        public void Init(IExplosiveObjectPresenter presenter) 
        {
            if (IsInitialized == false) 
            {
                _presenter = presenter;

                IsInitialized = true;
            }
        }
    }
}
