using UnityEngine;

namespace SpaceShooter
{
    public class DirectionToTarget : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        void Update()
        {
            var direction = _target.position - transform.position;
            var angle = -(Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg);
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        public void setTarget(Transform target)
        {
            _target = target;
        }
    }
}

