using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject explosionTarget;

    /// <summary>
    /// Создает префаб взрыва с координатами выбранного объекта
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
