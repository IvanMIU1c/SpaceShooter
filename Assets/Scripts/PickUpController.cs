using UnityEngine;

namespace SpaceShooter
{
    public class PickUpController : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<PickUp>() != null)
            {
                collision.gameObject.GetComponent<PickUp>().Pickup();
            }
        }
    }
}
