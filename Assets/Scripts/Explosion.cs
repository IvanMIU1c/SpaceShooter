using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject explosionTarget;

    /// <summary>
    /// ������� ������ ������ � ������������ ���������� �������
    /// </summary>
    public void ExplosionInit()
    {
        Instantiate(explosion, explosionTarget.transform.position, Quaternion.identity);
    }

    public void SetExplosionTarget(GameObject target)
    {
        explosionTarget = target;
    }
}
