using CosmicCuration.Utilities;

namespace CosmicCuration.Bullets
{
    public class BulletPool : GenericObjectPool<BulletController>
    {
        private BulletView bulletPrefab;
        private BulletScriptableObject bulletSO;

        public BulletPool(BulletView bulletPrefab, BulletScriptableObject bulletSO)
        {
            this.bulletPrefab = bulletPrefab;
            this.bulletSO = bulletSO;
        }

        public BulletController GetBullet() => GetItem<BulletController>();

        public void ReturnBullet(BulletController bulletController) => ReturnItem(bulletController);

        protected override BulletController CreateItem<T>() => new BulletController(bulletPrefab, bulletSO);
    }
}