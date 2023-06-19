using UnityEngine;

namespace SpaceShooter
{
    public class IndestructibilityPowerup : Powerup
    {
        [SerializeField] private float BoostTime;
        protected override void OnPickedUp(SpaceShip ship)
        {
            ship.IndestructBoostTime(BoostTime);
        }
    }
}
