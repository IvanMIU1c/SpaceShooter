using System.Collections;
using UnityEngine;

namespace SpaceShooter
{
    public class HomingRocket : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float m_Velocity;
        [SerializeField] private float rotateSpeed;
        [SerializeField] private Vector2 positionToMoveTo;
        
        private void Start()
        {
            StartCoroutine(LerpPosition(target.position, 10));
        }

        IEnumerator LerpPosition(Vector2 targetPosition, float duration)
        {
            float time = 0;
            Vector2 startPosition = transform.position;
            while (time < duration)
            {
                transform.position = Vector2.Lerp(startPosition, targetPosition, time / duration);
                time += Time.deltaTime;
                yield return null;
            }
            transform.position = targetPosition;
        }

        private void FixedUpdate()
        {
            
            if (!target)
                return;

            Vector2 direction = target.position - transform.position;
            
            float distance = direction.magnitude;
            
            float angle = Vector2.Angle(transform.forward, direction);

        }
    }
}
