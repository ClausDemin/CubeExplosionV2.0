namespace Assets.CodeBase.ExplosiveObject.Infrastructure
{
    public class ExplosiveObjectFactory
    {
        private SeparatorFactory _separatorFactory;
        private ExploderFactory _exploderFactory;

        public ExplosiveObjectFactory(
            float baseSeparationChance, 
            float separationChanceOverGenerationFactor,
            int minShardsCount,
            int maxShardsCount,
            float baseExplosionForce,
            float baseExplosionRadius,
            float explosionForceOverGenerationFactor,
            float explosionRadiusOverGenerationFactor) 
        {
            _separatorFactory = 
                new SeparatorFactory(baseSeparationChance, separationChanceOverGenerationFactor, minShardsCount, maxShardsCount);
            
            _exploderFactory = 
                new ExploderFactory(baseExplosionForce, baseExplosionRadius, explosionForceOverGenerationFactor, explosionRadiusOverGenerationFactor);
        }

        public Model.ExplosiveObject Create(int generation) 
        {
            return new Model.ExplosiveObject(_separatorFactory.Create(generation), _exploderFactory.Create(generation), generation);
        }
    }
}
