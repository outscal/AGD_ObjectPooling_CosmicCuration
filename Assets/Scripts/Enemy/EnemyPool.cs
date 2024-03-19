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
    }
}