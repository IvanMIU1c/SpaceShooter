using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    public class FinishTrigger : MonoBehaviour
    {
        [SerializeField] private UnityEvent Enter;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Enter.Invoke();
        }
    }
}
