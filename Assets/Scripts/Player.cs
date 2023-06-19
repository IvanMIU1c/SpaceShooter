using UnityEngine;

namespace SpaceShooter
{
    public class Player : SingletonBase<Player>
    {
        [SerializeField] private int m_NumLives;
        [SerializeField] private SpaceShip m_Ship;
        [SerializeField] private GameObject m_PlayerShipPrefab;
        public SpaceShip ActiveShip => m_Ship;

        [SerializeField] private CameraController m_CameraController;
        [SerializeField] private MovementCOntroller m_MovementController;

        protected override void Awake()
        {
            base.Awake();

            if (m_Ship != null)
            {
                Destroy(m_Ship.gameObject);
            }
        }


        [SerializeField] private Explosion explosionMainShip;
        [SerializeField] private LevelBoundaryLimiter levelBoundaryLimiter;

        [SerializeField] private PickUp pickUp;
        private void Start()
        {
            Respawn();
            m_Ship.EventOnDeath.AddListener(OnShipDeath);
        }

        private void OnShipDeath()
        {
            explosionMainShip.ExplosionInit();

            m_NumLives--;

            if (m_NumLives > 0)
            {
                Invoke("Respawn", 1.5f);
            }

            else
            {
                LevelSequenceController.Instance.FinishCurrentLevel(false);
            }
        }

        private void Respawn()
        {
            if (LevelSequenceController.PlayerShip != null)
            {
                var newPlayerShip = Instantiate(LevelSequenceController.PlayerShip);

                m_Ship = newPlayerShip.GetComponent<SpaceShip>();

                m_CameraController.SetTarget(m_Ship.transform);
                m_MovementController.SetTargetShip(m_Ship);

                levelBoundaryLimiter = m_Ship.gameObject.GetComponent<LevelBoundaryLimiter>();
                explosionMainShip = m_Ship.gameObject.GetComponent<Explosion>();
                pickUp = m_Ship.gameObject.GetComponent<PickUp>();

                m_Ship.EventOnDeath.AddListener(OnShipDeath);


            }
        }


        #region Score

        public int Score { get; private set; }
        public int NumKills { get; private set; }

        public void AddKill()
        {
            NumKills++;
        }

        public void AddScore(int num)
        {
            Score += num;
        }

        #endregion
    }
}
