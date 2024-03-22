using System;
using CosmicCuration.Utilities;
namespace CosmicCuration.PowerUps
{
    public class PowerupPool : GenericObjectPool<PowerUpController>
    {
        private PowerUpData powerUpData;

        public PowerUpController GetPowerUp<T>(PowerUpData powerUpData) where T: PowerUpController
        {
            this.powerUpData = powerUpData;
            return GetItem<T>();
        }

        protected override PowerUpController CreateItem<U>()
        {
            if (typeof(U) == typeof(Shield))
            {
                return new Shield(powerUpData);
            }
            if (typeof(U) == typeof(DoubleTurret))
            {
                return new DoubleTurret(powerUpData);
            }
            if (typeof(U) == typeof(RapidFire))
            {
                return new RapidFire(powerUpData);
            }

            throw new NotSupportedException("No supported Powerup");
        }
    }
}