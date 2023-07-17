using CosmicCuration.Utilities;
using System;

namespace CosmicCuration.Enemy
{
    public class EnemyPool : GenericObjectPool<EnemyController>
    {
        private EnemyData dataToUse;

        public EnemyController GetEnemy<T>(EnemyData enemyData) where T : EnemyController
        {
            dataToUse = enemyData;
            return GetItem<T>();
        }

        protected override EnemyController CreateItem<T>()
        {
            if (typeof(T) == typeof(ShootingEnemy))
                return new ShootingEnemy(dataToUse);
            else if (typeof(T) == typeof(RapidDashEnemy))
                return new RapidDashEnemy(dataToUse);
            else if (typeof(T) == typeof(ContinousChaseEnemy))
                return new ContinousChaseEnemy(dataToUse);
            else if (typeof(T) == typeof(NormalEnemy))
                return new NormalEnemy(dataToUse);
            else
                throw new NotSupportedException($"Enemy type '{typeof(T)}' is not supported.");
        }
    }
}
