using CosmicCuration.Player;

namespace CosmicCuration.PowerUps
{
    public class Health : PowerUpController
    {
        public Health(PowerUpData powerUpData) : base(powerUpData) { }

        public override void Activate()
        {
            base.Activate();
            GameService.Instance.GetPlayerService().GetPlayerController().IncreaseHealth();
        }

        public override void Deactivate()
        {
            base.Deactivate();
        }
    }
}
