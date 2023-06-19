using UnityEngine;

namespace SpaceShooter
{
    public class Asteroid : Destructible
    {
        public enum Size
        {
            Small,
            Medium,
            Big,
            Huge
        }

        [SerializeField] private Size size;
        [SerializeField] private Asteroid[] nextAsteroid;
        [SerializeField] private float m_RandomSpeed;

        private void Awake()
        {
            EventOnDeath.AddListener(OnAsteroidDestroy);
            
            SetSize(size);
        }

        private void OnAsteroidDestroy()
        {
            if (size != Size.Small)
            {
                SpawnAsteroids();
            }
            Destroy(gameObject);
        }


        private void SpawnAsteroids()
        {
            for (int i = 0; i < 2; i++)
            {
                Asteroid asteroid= Instantiate(nextAsteroid[Random.Range(0,nextAsteroid.Length)], transform.position, Quaternion.identity);

                asteroid.SetSize(size - 1);
                Rigidbody2D rb = asteroid.GetComponent<Rigidbody2D>();

                if (rb != null && m_RandomSpeed > 0)
                {
                    rb.velocity = (Vector2)UnityEngine.Random.insideUnitSphere * m_RandomSpeed;
                }
            }
        }

        private void OnDestroy()
        {
            EventOnDeath.RemoveListener(OnAsteroidDestroy);
        }

        public void SetSize(Size size)
        {
            if (size < 0) return;

            
            this.size = size;
        }
    }
}
