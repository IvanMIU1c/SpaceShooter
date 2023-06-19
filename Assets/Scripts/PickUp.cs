using UnityEngine;

namespace SpaceShooter
{
    public class PickUp : MonoBehaviour
    {
        // Переменная для хранения объекта, который будет подбираться
        [SerializeField] private GameObject item;
        [SerializeField] private DirectionToTarget direction;
        [SerializeField] private float spawnRange;
        [SerializeField] private bag bag;
        // Переменная для хранения звука подбора предмета
        //  public AudioClip pickupSound;
        // Метод для подбора предмета

        private void Start()
        {
            getTarget();
        }
        public void Pickup()
        {
            // Воспроизведение звука подбора
            // AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            // Создание нового объекта на случайном расстоянии от текущей позиции
            bag.Add(1);
            Vector3 newPosition = transform.position + new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0);
            Instantiate(item, newPosition, Quaternion.identity);
            getTarget();
            // Уничтожение объекта, который был подобран
            Destroy(gameObject);

        }
        public void getTarget()
        {
            direction.setTarget(item.transform);
        }
    }
}
