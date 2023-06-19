using UnityEngine;

namespace SpaceShooter
{
    public class SpeedPowerup : Powerup
    {
        [SerializeField] private float BoostTime;
        protected override void OnPickedUp(SpaceShip ship)
        {
            ship.SpeedBoostTime(BoostTime);
        }
    }
}
