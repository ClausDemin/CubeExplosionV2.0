using Assets.CodeBase.ExplosiveObject.Presenter;
using Assets.CodeBase.ExplosiveObject.View;

namespace Assets.CodeBase.ExplosiveObject.Infrastructure
{
    public class ExplosiveObjectPresenterFactory
    {
        private ExplosiveObjectFactory _factory;

        public ExplosiveObjectPresenterFactory(
            float baseSeparationChance,
            float separationChanceOverGenerationFactor,
            int minShardsCount,
            int maxShardsCount,
            float baseExplosionForce,
            float baseExplosionRadius,
            float explosionForceOverGenerationFactor,
            float explosionRadiusOverGenerationFactor)
        {
            _factory = new ExplosiveObjectFactory(
                baseSeparationChance,
                separationChanceOverGenerationFactor,
                minShardsCount,
                maxShardsCount,
                baseExplosionForce,
                baseExplosionRadius,
                explosionForceOverGenerationFactor,
                explosionRadiusOverGenerationFactor);
        }

        public ExplosiveObjectPresenter Create(ExplosiveObjectViewFactory viewFactory, ExplosionVFXFactory explosionVFXFactory, int generation)
        {
            return new ExplosiveObjectPresenter(_factory, viewFactory, explosionVFXFactory, generation);
        }
    }
}
