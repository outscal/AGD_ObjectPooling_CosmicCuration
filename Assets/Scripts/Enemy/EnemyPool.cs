using System.Collections.Generic;

namespace CosmicCuration.Enemy
{
    public class PooledEnemy
    {
        public EnemyController Enemy;
        public bool IsUsed;
    }
    
    public class EnemyPool
    {
        private EnemyView enemyView;
        private EnemyScriptableObject enemyScriptableObject;
        private List<PooledEnemy> pooledEnemies = new();

        public EnemyPool(EnemyView enemyView, EnemyScriptableObject enemyScriptableObject)
        {
            this.enemyView = enemyView;
            this.enemyScriptableObject = enemyScriptableObject;
        }

        public EnemyController GetEnemy()
        {
            if (pooledEnemies.Count > 0)
            {
                PooledEnemy pooledEnemy = pooledEnemies.Find(item => !item.IsUsed);

                if (pooledEnemy != null)
                {
                    pooledEnemy.IsUsed = true;
                    return pooledEnemy.Enemy;
                }
            }

            return CreateNewPooledEnemy();
        }

        private EnemyController CreateNewPooledEnemy()
        {
            PooledEnemy pooledEnemy = new PooledEnemy();
            pooledEnemy.Enemy = new EnemyController(enemyView, enemyScriptableObject.enemyData);
            pooledEnemy.IsUsed = false;
            return pooledEnemy.Enemy;
        }
    }
}