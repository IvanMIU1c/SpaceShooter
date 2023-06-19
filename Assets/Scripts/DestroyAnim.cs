using UnityEngine;

public class DestroyAnim : MonoBehaviour
{
    [SerializeField] private float timeToDestroy;
    void Start()
    {
        Destroy(gameObject,timeToDestroy);
    }
}
