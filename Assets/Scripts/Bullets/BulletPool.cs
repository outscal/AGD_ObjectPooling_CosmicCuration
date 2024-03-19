using System.Collections.Generic;

namespace CosmicCuration.Bullets
{
    public class PooledBullet
    {
        public BulletController bulletController;
        public bool IsUsed;
    }
    
    public class BulletPool
    {
        private BulletView bulletView;
        private BulletScriptableObject bulletScriptableObject;
        private List<PooledBullet> pooledBullets = new();

        public BulletPool(BulletView bulletView, BulletScriptableObject bulletScriptableObject)
        {
            this.bulletView = bulletView;
            this.bulletScriptableObject = bulletScriptableObject;
        }

        public BulletController GetBullet()
        {
            if (pooledBullets.Count > 0)
            {
                PooledBullet pooledBullet = pooledBullets.Find(item => !item.IsUsed);
                return pooledBullet.bulletController;
            }

            return CreateNewPooledBullet();
        }

        private BulletController CreateNewPooledBullet()
        {
            PooledBullet pooledBullet = new PooledBullet();
            pooledBullet.bulletController = new BulletController(bulletView, bulletScriptableObject);
            pooledBullet.IsUsed = true;

            return pooledBullet.bulletController;
        }
    }
}