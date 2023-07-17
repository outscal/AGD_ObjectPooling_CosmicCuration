using UnityEngine;

namespace CosmicCuration.Enemy
{
    public class ContinousChaseEnemy : EnemyController
    {
        public ContinousChaseEnemy(EnemyData enemyData) : base(enemyData) { }

        public override void UpdateMotion()
        {
            base.UpdateMotion();
            ChasePlayer();
        }

        protected void ChasePlayer()
        {
            Vector3 playerPosition = GameService.Instance.GetPlayerService().GetPlayerPosition();
            Vector3 direction = playerPosition - enemyView.transform.position;
            enemyView.transform.up = direction.normalized;
        }
    }
}
