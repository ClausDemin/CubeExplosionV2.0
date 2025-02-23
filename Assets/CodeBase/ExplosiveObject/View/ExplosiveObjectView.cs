using Assets.CodeBase.ExplosiveObject.Presenter;
using UnityEngine;

namespace Assets.CodeBase.ExplosiveObject.View
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

        private void Awake()
        {
            _body = GetComponent<Rigidbody>();
        }

        private void OnMouseUpAsButton()
        {
            _presenter.HandleClick(transform.position, transform.localScale);

            Destroy(gameObject);
        }

        public Rigidbody GetRigidbody()
        { 
            return _body;
        }
    }
}
