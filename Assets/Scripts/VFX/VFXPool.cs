using CosmicCuration.Utilities;

namespace CosmicCuration.VFX
{
    public class VFXPool : GenericObjectPool<VFXController>
    {
        private VFXView vfxView;
        
        public VFXController GetVFX(VFXView vfxView)
        {
            this.vfxView = vfxView;
            return GetItem<VFXController>();
        }

        protected override VFXController CreateItem<T>() => new VFXController(vfxView);
    }
}