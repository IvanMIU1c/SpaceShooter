using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private Vector3 speed;
    [SerializeField] private Transform m_transform;

    private void Update()
    {
        m_transform.Rotate(speed * Time.deltaTime);
    }
}
