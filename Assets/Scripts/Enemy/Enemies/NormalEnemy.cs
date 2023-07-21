using CosmicCuration.Bullets;
using UnityEngine;

namespace CosmicCuration.Enemy
{
    public class NormalEnemy : EnemyController
    {
        public NormalEnemy(EnemyData enemyData) : base(enemyData) { }

        public override void Configure(Vector3 positionToSet, EnemyOrientation enemyOrientation, BulletPool bulletPool=null)
        {
            base.Configure(positionToSet, enemyOrientation);
        }
    }
}
