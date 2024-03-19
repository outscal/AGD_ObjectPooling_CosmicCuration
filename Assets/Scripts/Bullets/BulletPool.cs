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

        public void ReturnBullet(BulletController bulletController)
        {
            PooledBullet pooledBullet = pooledBullets.Find(item => item.Bullet == bulletController);
            pooledBullet.IsUsed = false;
        }

        private BulletController CreateNewPooledBullet()
        {
            PooledBullet pooledBullet = new PooledBullet();
            pooledBullet.Bullet = new BulletController(bulletView, bulletScriptableObject);
            pooledBullet.IsUsed = true;
            pooledBullets.Add(pooledBullet);
            return pooledBullet.Bullet;
        }
    }
}