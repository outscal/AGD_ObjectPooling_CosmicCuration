using UnityEngine;

namespace CosmicCuration.Enemy
{
    public class RapidDashEnemy : EnemyController
    {
        private float dashSpeed;

        public RapidDashEnemy(EnemyData enemyData) : base(enemyData)
        {
            dashSpeed = enemyData.dashSpeed;
        }

        public override void Configure(Vector3 positionToSet, EnemyOrientation enemyOrientation)
        {
            base.Configure(positionToSet, enemyOrientation);
            StartDash();
        }

        protected void StartDash()
        {
            currentEnemyState = EnemyState.Moving;
            speed = dashSpeed;
        }
    }
}
