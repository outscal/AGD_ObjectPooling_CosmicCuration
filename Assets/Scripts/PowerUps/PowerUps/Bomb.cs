using CosmicCuration.Player;

namespace CosmicCuration.PowerUps
{
    public class Bomb : PowerUpController
    {
        public Bomb(PowerUpData powerUpData) : base(powerUpData) { }

        public override void Activate()
        {
            base.Activate();
            GameService.Instance.GetEnemyService().DestroyAllEnemies();
            GameService.Instance.ShakeCamera();
        }

        public override void Deactivate()
        {
            base.Deactivate();
        }
    }
}
