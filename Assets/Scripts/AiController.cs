using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(SpaceShip))]
    public class AiController : MonoBehaviour
    {
        public enum AIBehaviour
        {
            Null,
            Patrol,
            PointPatrol,
            Hunt
        }

        [SerializeField] private AIBehaviour m_AIBehaviour;

        [SerializeField] private AIPointPatrol m_PointPatrol;

        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_NavigationLinear;

        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_NavigationAngular;

        [SerializeField] private float m_PredictCoeff;

        [SerializeField] private float m_RandomSelectMovePointTime;
        
        [SerializeField] private float m_FindNewTargetTime;
        
        [SerializeField] private float m_ShootDelay;
        
        [SerializeField] private float m_EvadeRayLength;

        [SerializeField] private GameObject[] m_PatrolPoints;

        private SpaceShip m_SpaceShip;

        private Vector3 m_MovePosition;

        private Destructible m_SelectTarget;

        private Timer m_RandomizeDirectionTime;
        private Timer m_FireTimer;
        private Timer m_FindNewTargetTimer;

        private int m_CurrentPoint = 1;

        private void Start()
        {
            m_SpaceShip = GetComponent<SpaceShip>();

            InitTimers();
        }

        private void Update()
        {
            UpdateTimers();

            UpdateAI();
        }

        private void UpdateAI()
        {
            if(m_AIBehaviour == AIBehaviour.Patrol)
            {
                UpdateBehaviourPatrol();
            }

            if(m_AIBehaviour == AIBehaviour.PointPatrol)
            {
                UpdateBehaviourPatrol();
            }
        }

        public void UpdateBehaviourPatrol()
        {
            ActionFindNewMovePosition();
            ActionControlShip();
            ActionFindNewAttackTarget();
            ActionFire();
            ActionEvadeCollision();
        }

        private void ActionFindNewMovePosition() 
        {
            if(m_AIBehaviour == AIBehaviour.Patrol)
            {

                if(m_SelectTarget != null)
                {
                    m_MovePosition = m_SelectTarget.transform.position;
                }

                else
                {
                    if(m_PointPatrol != null)
                    {

                        bool isInsidePatrolZone = (m_PointPatrol.transform.position - transform.position).sqrMagnitude < m_PointPatrol.Radius * m_PointPatrol.Radius;

                        if (isInsidePatrolZone == true)
                        {

                            if (m_RandomizeDirectionTime.IsFinished == true)
                            {
                                Vector2 newPoint = UnityEngine.Random.onUnitSphere * m_PointPatrol.Radius + m_PointPatrol.transform.position;

                                m_MovePosition = newPoint;

                                m_RandomizeDirectionTime.Start(m_RandomSelectMovePointTime);

                                
                            }
                        }

                        else
                        {
                            m_MovePosition = m_PointPatrol.transform.position;
                        }
                    }
                }
            }

            if (m_AIBehaviour == AIBehaviour.PointPatrol)
            {

                if (m_SelectTarget != null)
                {
                    m_MovePosition = m_SelectTarget.transform.position;
                    m_MovePosition = m_MovePosition + m_SelectTarget.transform.up * m_SelectTarget.GetComponent<Rigidbody2D>().velocity.magnitude * m_PredictCoeff;
                    //m_MovePosition = CalculateLeadTarget(m_SelectTarget.transform.position, m_SelectTarget.GetComponent<SpaceShip>().GetSpeed(), 2000, transform.position);
                }

                else
                {

                    if(m_MovePosition != null)
                    {
                        m_MovePosition = m_PatrolPoints[m_CurrentPoint].transform.position;
                    }

                    else
                    {
                        m_MovePosition = this.transform.position;
                    }
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Collider2D chek = collision;

            if (m_PatrolPoints[m_CurrentPoint].GetComponent<Collider2D>() == chek)
            {
                if (m_CurrentPoint < m_PatrolPoints.Length - 1)
                {
                    m_CurrentPoint++;
                }
                else
                {
                    m_CurrentPoint = 0;
                }
            }

        }




        private void ActionEvadeCollision()
        {
            if(Physics2D.Raycast(transform.position, transform.up, m_EvadeRayLength) == true)
            {
                m_MovePosition = transform.position + transform.right * 100.0f;
            }
        }

        private const float MAX_ANGLE = 45.0f;
        private static float ComputeAliginTorqueNormalized(Vector3 targetPosition, Transform ship)
        {
            Vector2 localTargetPosition = ship.InverseTransformPoint(targetPosition);

            float angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward);

            angle = Mathf.Clamp(angle, -MAX_ANGLE, MAX_ANGLE) / MAX_ANGLE;
            
            return -angle;
        }

        private void ActionControlShip()
        {
            m_SpaceShip.ThrustControl = m_NavigationLinear;

            m_SpaceShip.TorqueControl = ComputeAliginTorqueNormalized(m_MovePosition, m_SpaceShip.transform) * m_NavigationAngular;
        }

        private void ActionFindNewAttackTarget()
        {
            if (m_FindNewTargetTimer.IsFinished == true)
            {
                m_SelectTarget = FindNearestDestructableTarget();

                m_FindNewTargetTimer.Start(m_FindNewTargetTime);
            }
        }
        private void ActionFire()
        {
            if(m_SelectTarget != null)
            {
                if(m_FireTimer.IsFinished == true)
                {
                    m_SpaceShip.Fire(TurretMode.Primary);

                    m_FireTimer.Start(m_ShootDelay);
                }
            }
        }

        private Destructible FindNearestDestructableTarget()
        {
            float maxDist = float.MaxValue;

            Destructible potentialTarget = null;

            foreach(var v in Destructible.AllDestructibles)
            {
                if (v.GetComponent<SpaceShip>() == m_SpaceShip) continue;

                if (v.TeamId == Destructible.TeamIdNeutral) continue;

                if (v.TeamId == m_SpaceShip.TeamId) continue;

                float dist = Vector2.Distance(m_SpaceShip.transform.position, v.transform.position);

                if (dist < maxDist)
                {
                    maxDist = dist;

                    potentialTarget = v;
                }
            }
            return potentialTarget;
        }

        Vector3 CalculateLeadTarget(Vector3 targetPosition, Vector3 targetVelocity, float bulletSpeed, Vector3 shooterPosition)
        {
            Vector3 relativePosition = targetPosition - shooterPosition;
            float distance = relativePosition.magnitude;
            float timeToIntercept = distance / bulletSpeed;


            Vector3 leadTarget = targetPosition + (targetVelocity * timeToIntercept);
            return leadTarget;
        }



        #region Timers
        private void InitTimers()
        {
            m_RandomizeDirectionTime = new Timer(m_RandomSelectMovePointTime);

            m_FireTimer = new Timer(m_ShootDelay);

            m_FindNewTargetTimer = new Timer(m_FindNewTargetTime);
        }

        private void UpdateTimers()
        {
            m_RandomizeDirectionTime.RemoveTime(Time.deltaTime);
            m_FireTimer.RemoveTime(Time.deltaTime);
            m_FindNewTargetTimer.RemoveTime(Time.deltaTime);
        }

        public void SetPatrolBehaviour(AIPointPatrol point)
        {
            m_AIBehaviour = AIBehaviour.Patrol;
            m_PointPatrol = point;
        }

        #endregion
    }
}
