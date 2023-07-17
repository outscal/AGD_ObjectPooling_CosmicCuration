using CosmicCuration.Player;

namespace CosmicCuration.PowerUps
{
    public class Health : PowerUpController
    {
        public Health(PowerUpData powerUpData) : base(powerUpData) { }

        public override void Activate()
        {
            base.Activate();
            //GameService.Instance.GetPlayerService().GetPlayerController().SetShieldState(ShieldState.Activated);
        }

        public override void Deactivate()
        {
            base.Deactivate();
            //GameService.Instance.GetPlayerService().GetPlayerController().SetShieldState(ShieldState.Deactivated);
        }
    }
}
