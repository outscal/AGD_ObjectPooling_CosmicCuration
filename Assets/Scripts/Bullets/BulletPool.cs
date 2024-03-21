using System.Collections.Generic;
using CosmicCuration.Utilities;

namespace CosmicCuration.Bullets
{
    public class BulletPool:GenericObjectPool<BulletController>
    {
        private BulletView bulletView;
        private BulletScriptableObject bulletSO;

        public BulletPool(BulletView bulletView, BulletScriptableObject bulletSO)
        {
            this.bulletView = bulletView;
            this.bulletSO = bulletSO;
        }

        public BulletController GetBullet() => GetItem();

        protected override BulletController CreateItem() => new BulletController(bulletView, bulletSO);
    }
}