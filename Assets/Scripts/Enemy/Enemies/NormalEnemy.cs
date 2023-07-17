using UnityEngine;

namespace CosmicCuration.Enemy
{
    public class NormalEnemy : EnemyController
    {
        public NormalEnemy(EnemyData enemyData) : base(enemyData) { }

        public override void Configure(Vector3 positionToSet, EnemyOrientation enemyOrientation)
        {
            base.Configure(positionToSet, enemyOrientation);
        }
    }
}
