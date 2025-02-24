using Assets.CodeBase.ExplosionFeature.Model;
using Assets.CodeBase.ExplosionFeature.Presenter;
using Assets.CodeBase.ExplosionFeature.View;

namespace Assets.CodeBase.ExplosionFeature.Infrastructure
{
    public class ExplosiveObjectPresenterFactory
    {
        private Separator _separator;
        private Exploder _exploder;

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
            _separator =
                new Separator(baseSeparationChance, separationChanceOverGenerationFactor, minShardsCount, maxShardsCount);

            _exploder =
                new Exploder(baseExplosionForce, baseExplosionRadius, explosionForceOverGenerationFactor, explosionRadiusOverGenerationFactor);
        }

        public ExplosiveObjectPresenter Create(ExplosiveObjectViewFactory viewFactory, ExplosionVFXFactory explosionVFXFactory, int generation)
        {
            ExplosiveObject explosiveObject = new ExplosiveObject(generation);

            return new ExplosiveObjectPresenter(explosiveObject, _separator, _exploder, viewFactory, explosionVFXFactory);
        }
    }
}
