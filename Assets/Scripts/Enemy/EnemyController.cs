using UnityEngine;
using CosmicCuration.Audio;
using CosmicCuration.VFX;
using CosmicCuration.Player;
using CosmicCuration.Bullets;

namespace CosmicCuration.Enemy
{
    public class EnemyController
    {
        // Dependencies:
        protected EnemyView enemyView;
        protected EnemyData enemyData;
        private BulletPool bulletPool;

        // Variables:
        protected EnemyState currentEnemyState;
        protected int currentHealth;
        protected float speed;
        protected float movementTimer;
        protected Quaternion targetRotation;
        private ShootingState currentShootingState;
        private float currentRateOfFire;
        private bool isShooting;

        public EnemyController(EnemyData enemyData)
        {
            enemyView = Object.Instantiate(enemyData.enemyPrefab);
            enemyView.SetController(this);
            this.enemyData = enemyData;
        }

        public virtual void Configure(Vector3 positionToSet, EnemyOrientation enemyOrientation, BulletPool bulletPool=null)
        {
            enemyView.transform.position = positionToSet;
            SetEnemyOrientation(enemyOrientation);

            this.bulletPool = bulletPool;

            currentEnemyState = EnemyState.Moving;
            currentHealth = enemyData.maxHealth;
            speed = Random.Range(enemyData.minimumSpeed, enemyData.maximumSpeed);
            movementTimer = enemyData.movementDuration;
            enemyView.gameObject.SetActive(true);

            currentShootingState = ShootingState.NotFiring;
            currentRateOfFire = enemyData.defaultFireRate;
        }

        protected void SetEnemyOrientation(EnemyOrientation orientation)
        {
            // Rotate the enemy based on its orientation
            switch (orientation)
            {
                case EnemyOrientation.Left:
                    enemyView.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                    break;
                case EnemyOrientation.Right:
                    enemyView.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
                    break;
                case EnemyOrientation.Up:
                    enemyView.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    break;
                case EnemyOrientation.Down:
                    enemyView.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                    break;
            }
        }

        public void SetIsShooting(bool isShooting)
        {
            this.isShooting = isShooting;
            if (isShooting)
                StartShooting();
            else
                StopShooting();
        }

        protected virtual void StartShooting()
        {
            currentShootingState = ShootingState.Firing;
        }

        protected virtual void StopShooting()
        {
            currentShootingState = ShootingState.NotFiring;
        }

        protected void Shoot()
        {
            if (currentShootingState == ShootingState.Firing)
            {
              FireBulletAtPosition(enemyView.canonTransform);
            }
        }

        private void FireBulletAtPosition(Transform fireLocation)
        {
            BulletController bulletToFire = bulletPool.GetBullet();
            bulletToFire.ConfigureBullet(fireLocation);
            bulletToFire.ChangeBulletColor(Color.red);
            GameService.Instance.GetSoundService().PlaySoundEffects(SoundType.PlayerBullet);
        }

        public void TakeDamage(int damageToTake)
        {
            currentHealth -= damageToTake;
            if (currentHealth <= 0)
                EnemyDestroyed();
        }

        public virtual void UpdateMotion()
        {
            if (currentEnemyState == EnemyState.Moving)
            {
                enemyView.transform.position += enemyView.transform.up * Time.deltaTime * speed;
                movementTimer -= Time.deltaTime;

                if (movementTimer <= 0)
                {
                    SetTargetRotation();
                    movementTimer = enemyData.movementDuration;
                }
            }
            else if (currentEnemyState == EnemyState.Rotating)
            {
                enemyView.transform.rotation = Quaternion.RotateTowards(enemyView.transform.rotation, targetRotation, enemyData.rotationSpeed * Time.deltaTime);

                if (Quaternion.Angle(enemyView.transform.rotation, targetRotation) < enemyData.rotationTolerance)
                    currentEnemyState = EnemyState.Moving;
            }

            if (isShooting)
                Shoot();
        }

        protected void SetTargetRotation()
        {
            Vector3 direction = GameService.Instance.GetPlayerService().GetPlayerPosition() - enemyView.transform.position;
            targetRotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f);
            currentEnemyState = EnemyState.Rotating;
        }

        public void OnEnemyCollided(GameObject collidedGameObject)
        {
            if (collidedGameObject.GetComponent<PlayerView>() != null)
            {
                GameService.Instance.GetPlayerService().GetPlayerController().TakeDamage(enemyData.damageToInflict);
                EnemyDestroyed();
            }
        }

        protected virtual void EnemyDestroyed()
        {
            GameService.Instance.GetUIService().IncrementScore(enemyData.scoreToGrant);
            GameService.Instance.GetSoundService().PlaySoundEffects(SoundType.EnemyDeath);
            GameService.Instance.GetVFXService().PlayVFXAtPosition(VFXType.EnemyExplosion, enemyView.transform.position);
            enemyView.gameObject.SetActive(false);
            GameService.Instance.GetEnemyService().ReturnEnemyToPool(this);
        }

        // Enums
        private enum ShootingState
        {
            Firing,
            NotFiring
        }

        protected enum EnemyState
        {
            Moving,
            Rotating
        }
    }
}
