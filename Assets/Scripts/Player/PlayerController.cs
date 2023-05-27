using System.Threading.Tasks;
using UnityEngine;
using CosmicCuration.Bullets;
using CosmicCuration.Audio;
using CosmicCuration.VFX;

namespace CosmicCuration.Player
{
    public class PlayerController : IDamageable
    {
        #region Dependencies
        private PlayerView playerView;
        private PlayerScriptableObject playerSO;
        private BulletPool bulletPool;
        #endregion

        #region Variables
        private WeaponMode currentWeaponMode;
        private int currentHealth;
        private float currentRateOfFire;
        private bool isShooting;
        private bool shieldActive; 
        #endregion

        #region Initialization

        public PlayerController(PlayerView playerViewPrefab, PlayerScriptableObject playerSO, BulletPool bulletPool)
        {
            playerView = Object.Instantiate(playerViewPrefab);
            playerView.SetController(this);
            this.playerSO = playerSO;
            this.bulletPool = bulletPool;

            InitializeVariables();
        }

        private void InitializeVariables()
        {
            currentWeaponMode = WeaponMode.SingleCanon;
            currentHealth = playerSO.maxHealth;
            currentRateOfFire = playerSO.defaultFireRate;
            isShooting = shieldActive = false;
            GameService.Instance.GetUIService().UpdateHealthUI(currentHealth);
        }

        #endregion

        #region Input Handling
        public void HandlePlayerInput()
        {
            HandlePlayerMovement();
            HandlePlayerRotation();
            HandleShooting();
        }

        private void HandlePlayerMovement()
        {
            if (Input.GetKey(KeyCode.W))
                playerView.transform.Translate(Vector2.up * Time.deltaTime * playerSO.movementSpeed);
            if (Input.GetKey(KeyCode.S))
                playerView.transform.Translate(Vector2.down * Time.deltaTime * playerSO.movementSpeed);
            if (Input.GetKey(KeyCode.A))
                playerView.transform.Translate(Vector2.left * Time.deltaTime * playerSO.movementSpeed);
            if (Input.GetKey(KeyCode.D))
                playerView.transform.Translate(Vector2.right * Time.deltaTime * playerSO.movementSpeed);
        }

        private void HandlePlayerRotation()
        {
            // Rotate the player to look in the direction of mouse position.
            var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(playerView.transform.position);
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            playerView.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }

        private void HandleShooting()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                FireWeapon();
            if (Input.GetKeyUp(KeyCode.Space))
                isShooting = false;
        }
        #endregion

        #region Firing Weapons
        private async void FireWeapon()
        {
            isShooting = true;
            while (isShooting)
            {
                switch (currentWeaponMode)
                {
                    case WeaponMode.SingleCanon:
                        FireBulletAtPosition(playerView.canonTransform);
                        break;
                    case WeaponMode.DoubleTurret:
                        FireBulletAtPosition(playerView.turretTransform1);
                        FireBulletAtPosition(playerView.turretTransform2);
                        break;
                }
                await Task.Delay(Mathf.RoundToInt(currentRateOfFire * 1000));
            }
        }

        private void FireBulletAtPosition(Transform fireLocation)
        {
            BulletController bulletToFire = bulletPool.GetBullet();
            bulletToFire.ConfigureBullet(fireLocation);
            GameService.Instance.GetSoundService().PlaySoundEffects(SoundType.PlayerBullet);
        } 
        #endregion

        #region PowerUp Logic

        public void ToggleShield(bool setActive)
        {
            if (setActive)
                shieldActive = true;
            else
                shieldActive = false;
        }

        public void ToggleDoubleTurret(bool setActive)
        {
            if (setActive)
                currentWeaponMode = WeaponMode.DoubleTurret;
            else
                currentWeaponMode = WeaponMode.SingleCanon;
        }

        public void ToggleRapidFire(bool setActive)
        {
            if (setActive)
                currentRateOfFire = playerSO.rapidFireRate;
            else
                currentRateOfFire = playerSO.defaultFireRate;
        }

        #endregion

        public void TakeDamage(int damageToTake)
        {
            if (!shieldActive)
            {
                currentHealth -= damageToTake;
                GameService.Instance.GetUIService().UpdateHealthUI(currentHealth);
            }

            if (currentHealth <= 0)
                PlayerDeath();
        }

        private async void PlayerDeath()
        {
            Object.Destroy(playerView.gameObject);
            GameService.Instance.GetVFXService().PlayVFXAtPosition(VFXType.PlayerExplosion, playerView.transform.position);
            GameService.Instance.GetSoundService().PlaySoundEffects(SoundType.PlayerDeath);
            GameService.Instance.GetEnemyService().ToggleEnemySpawning(false);
            GameService.Instance.GetPowerUpService().DisablePowerUpSpawning();
            await Task.Delay(2000);
            GameService.Instance.GetUIService().EnableGameOverUI();
        }
    }

    public enum WeaponMode
    {
        SingleCanon,
        DoubleTurret
    } 
}