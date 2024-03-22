using System.Collections.Generic;
using UnityEngine;

namespace CosmicCuration.VFX
{
    public class VFXService
    {
        private VFXPool vfxPool;
        private List<VFXData> vfxData = new List<VFXData>();

        public VFXService(VFXScriptableObject vfxScriptableObject)
        {
            vfxData = vfxScriptableObject.vfxData;
            vfxPool = new VFXPool();
        }

        public void PlayVFXAtPosition(VFXType type, Vector2 spawnPosition)
        {
            VFXView prefabToSpawn = vfxData.Find(item => item.type == type).prefab;
            VFXController vfxToPlay = vfxPool.GetVFX(prefabToSpawn);
            vfxToPlay.Configure(spawnPosition);
        }

        public void ReturnVfxToPool(VFXController vfxToReturn) => vfxPool.ReturnItem(vfxToReturn);
    } 
}