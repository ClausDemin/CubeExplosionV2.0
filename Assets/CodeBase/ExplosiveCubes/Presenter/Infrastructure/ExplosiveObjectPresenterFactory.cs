using Assets.CodeBase.ExplosiveCubes.Model.Interfaces;
using Assets.CodeBase.ExplosiveCubes.Model;
using Assets.CodeBase.ExplosiveCubes.View.Interfaces;
using Assets.CodeBase.ExplosiveCubes.Presenter.Interfaces;

namespace Assets.CodeBase.ExplosiveCubes.Presenter.Infrastructure
{
    public class ExplosiveObjectPresenterFactory: IExplosiveObjectPresenterFactory
    {
        public IExplosiveObjectPresenter Create(IExplosiveObjectsFactory objectsFactory, ISeparableEntity separableEntity, IExploder exploder) 
        {
            return new ExplosiveObjectPresenter(objectsFactory, exploder, separableEntity);
        }
    }
}
