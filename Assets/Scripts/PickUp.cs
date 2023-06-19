using UnityEngine;

namespace SpaceShooter
{
    public class PickUp : MonoBehaviour
    {
        // ���������� ��� �������� �������, ������� ����� �����������
        [SerializeField] private GameObject item;
        [SerializeField] private DirectionToTarget direction;
        [SerializeField] private float spawnRange;
        [SerializeField] private bag bag;
        // ���������� ��� �������� ����� ������� ��������
        //  public AudioClip pickupSound;
        // ����� ��� ������� ��������

        private void Start()
        {
            getTarget();
        }
        public void Pickup()
        {
            // ��������������� ����� �������
            // AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            // �������� ������ ������� �� ��������� ���������� �� ������� �������
            bag.Add(1);
            Vector3 newPosition = transform.position + new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0);
            Instantiate(item, newPosition, Quaternion.identity);
            getTarget();
            // ����������� �������, ������� ��� ��������
            Destroy(gameObject);

        }
        public void getTarget()
        {
            direction.setTarget(item.transform);
        }
    }
}
