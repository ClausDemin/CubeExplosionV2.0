using Assets.CodeBase.ExplosionFeature.Presenter;
using UnityEngine;

namespace Assets.CodeBase.ExplosionFeature.View
{
    [RequireComponent(typeof(Rigidbody))]
    public class ExplosiveObjectView : MonoBehaviour
    {
        private ExplosiveObjectPresenter _presenter;

        private Rigidbody _body;

        public void Init(ExplosiveObjectPresenter presenter)
        {
            _presenter = presenter;
        }

        public Rigidbody Rigidbody => _body;

        private void Awake()
        {
            _body = GetComponent<Rigidbody>();
        }

        private void OnMouseUpAsButton()
        {
            _presenter.HandleClick(transform.position, transform.localScale);

            Destroy(gameObject);
        }
    }
}
