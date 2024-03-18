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
    }
}