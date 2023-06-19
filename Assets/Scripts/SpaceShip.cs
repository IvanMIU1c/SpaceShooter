using UnityEngine;

namespace SpaceShooter
{

    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceShip : Destructible
    {
        /// <summary>
        /// Mass for rigidbody
        /// </summary>
        [Header("Space ship")]
        [SerializeField] private float m_Mass;


        /// <summary>
        /// pushing forward force
        /// </summary>
        [SerializeField] private float m_Thrust;

        /// <summary>
        /// rotating force
        /// </summary>
        [SerializeField] private float m_Mobility;

        /// <summary>
        /// max linear speed/velocity
        /// </summary>
        [SerializeField] private float m_MaxLinearVelocity;
        public float MaxLinearVelocity => m_MaxLinearVelocity;

        /// <summary>
        /// max angular speed/velocity
        /// </summary>
        [SerializeField] private float m_MaxAngularVelocity;
        public float MaxAngularVelocity => m_MaxAngularVelocity;


        [SerializeField] private Sprite m_PreviewImage;
        public Sprite PreviewImage => m_PreviewImage;


        /// <summary>
        /// Saved link on rigid; 
        /// </summary>
        private Rigidbody2D m_Rigid;

        #region Public Api
        /// <summary>
        /// Linear thrust control in range from -1.0 to +1.0
        /// </summary>
        public float ThrustControl { get; set; }
        /// <summary>
        /// Torque thrust control in range from -1.0 to +1.0
        /// </summary>
        public float TorqueControl { get; set; }
        #endregion


        #region Unity Event
        protected override void Start()
        {
            base.Start();

            m_Rigid = GetComponent<Rigidbody2D>();
            m_Rigid.mass = m_Mass;

            m_Rigid.inertia = 1;

            InitOffensive();
        }



        private void FixedUpdate()
        {
            UpdateRigidBody();

            UpdateEnergyRegen();

            if (m_BoostSpeedTime <= 0)
                m_SpeedBoost = 1;
            else
            {
                m_BoostSpeedTime -= Time.fixedDeltaTime;
                m_SpeedBoost = 3;
            }

            if (m_IndestructBoostTimer <= 0)
                SetDestruct();
            else
            {
                m_IndestructBoostTimer -= Time.fixedDeltaTime;
                SetIndestruct();
            }
        }
        #endregion

        /// <summary>
        /// Method that adds forces to ship for movement
        /// </summary>
        private void UpdateRigidBody()
        {
            m_Rigid.AddForce(m_Thrust * ThrustControl * m_SpeedBoost * transform.up*Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigid.AddForce(-m_Rigid.velocity * (m_Thrust / m_MaxLinearVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigid.AddTorque(TorqueControl * m_Mobility * Time.fixedDeltaTime,ForceMode2D.Force);

            m_Rigid.AddTorque(-m_Rigid.angularVelocity * (m_Mobility/m_MaxAngularVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);

        }

        [SerializeField] private Turret[] m_Turrets;

        public void Fire(TurretMode mode)
        {
            for (int i = 0; i< m_Turrets.Length; i++)
            {
                if (m_Turrets[i].Mode == mode)
                {
                    m_Turrets[i].Fire();
                }
            }
        }


        [SerializeField] private int m_MaxEnergy;
        [SerializeField] private int m_MaxAmmo;
        [SerializeField] private int m_EnergyRegenPerSecond;

        private float m_PrimaryEnergy;
        private int m_SecondaryAmmo;

        private float m_SpeedBoost;
        private float m_BoostSpeedTime;

        public void SpeedBoostTime(float BoostTime)
        {
            m_BoostSpeedTime += BoostTime;
        }


        private float m_IndestructBoostTimer;
        public void IndestructBoostTime(float BoostTime)
        {
            m_IndestructBoostTimer += BoostTime;
        }


        public void AddEnergy(int e)
        {
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy+e, 0, m_MaxEnergy) ;
        }

        public void AddAmmo(int ammo)
        {
            m_SecondaryAmmo = Mathf.Clamp(m_SecondaryAmmo + ammo, 0, m_MaxAmmo);
        }

        private void InitOffensive()
        {
            m_PrimaryEnergy = m_MaxEnergy;
            m_SecondaryAmmo = m_MaxAmmo;
        }

        private void  UpdateEnergyRegen()
        {
            m_PrimaryEnergy += (float)m_EnergyRegenPerSecond * Time.fixedDeltaTime;

            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy, 0, m_MaxEnergy);
        }



        public bool DrawEnergy(int count)
        {
            if (count == 0)
                return true;

            if(m_PrimaryEnergy >= count)
            {
                m_PrimaryEnergy -= count;
                return true;
            }


            return false;
        }

        public bool DrawAmmo(int count)
        {
            if (count == 0)
                return true;

            if(m_SecondaryAmmo >= count)
            {
                m_SecondaryAmmo -= count;
                return true;
            }


            return false;
        }

        public void AssignWeapon(TurretProperties props)
        {
            for(int i = 0; i < m_Turrets.Length; i++)
            {
                m_Turrets[i].AssignLoadout(props);
            }
        }

        public Vector2 GetSpeed()
        {
            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                return rb.velocity;
            }
            else
            {
                return Vector2.zero;
            }
        }
    }
}

