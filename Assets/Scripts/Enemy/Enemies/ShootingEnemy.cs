using UnityEngine;

namespace CosmicCuration.Enemy
{
    public class ShootingEnemy : EnemyController
    {
        private float shootingDistance;
        private float shootingCooldown;
        private float nextShotTime;

        public ShootingEnemy(EnemyData enemyData) : base(enemyData)
        {
            shootingDistance = enemyData.shootingDistance;
            shootingCooldown = enemyData.shootingCooldown;
            nextShotTime = 0f;
        }

        public override void Configure(Vector3 positionToSet, EnemyOrientation enemyOrientation)
        {
            base.Configure(positionToSet, enemyOrientation);
            StartShooting();
        }

        protected void StartShooting()
        {
            currentEnemyState = EnemyState.Moving;
            speed = enemyData.maximumSpeed;
        }

        public override void UpdateMotion()
        {
            base.UpdateMotion();
            if (currentEnemyState == EnemyState.Moving && Time.time >= nextShotTime)
            {
                if (IsPlayerInShootingRange())
                {
                    ShootPlayer();
                    nextShotTime = Time.time + shootingCooldown;
                }
            }
        }

        protected bool IsPlayerInShootingRange()
        {
            Vector3 playerPosition = GameService.Instance.GetPlayerService().GetPlayerPosition();
            float distanceToPlayer = Vector3.Distance(enemyView.transform.position, playerPosition);
            return distanceToPlayer <= shootingDistance;
        }

        protected void ShootPlayer()
        {
            // Perform shooting logic here
        }
    }
}
