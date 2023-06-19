using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    /// <summary>
    /// Destructible object on scene. Or smth that can has hp.
    /// </summary>
    public class Destructible : Entity
    {
        #region Properties
        /// <summary>
        /// Object avoids damage
        /// </summary>
        [SerializeField] private bool m_Indestructible;
        public bool IsIndestructible => m_Indestructible;

        /// <summary>
        /// Start hit Points
        /// </summary>
        [SerializeField] private int m_HitPoints;


        /// <summary>
        /// Current hit Points
        /// </summary>
        private int m_CurrentHitPoint;
        public int HitPoints => m_CurrentHitPoint;

        public int StartHitPoints => m_HitPoints;
        #endregion


        #region UnityEvents

        protected virtual void Start()
        {
            m_CurrentHitPoint = m_HitPoints;

        }
        #endregion

        #region PublicApi
        /// <summary>
        /// Applies damage to object
        /// </summary>
        /// <param name="damage">Damage to the object</param>
        public void ApplyDamage(int damage)
        {
            if (m_Indestructible) return;

            m_CurrentHitPoint -= damage;

            if (m_CurrentHitPoint <= 0)
                OnDeath();
        }
        #endregion

        /// <summary>
        /// Destruction event. When hp <= 0
        /// </summary>
        protected virtual void OnDeath()
        {
            Destroy(gameObject);
            m_EventOnDeath?.Invoke();
        }

        private static HashSet<Destructible> m_AllDestructibles;
        public static IReadOnlyCollection<Destructible> AllDestructibles => m_AllDestructibles;

        protected virtual void OnEnable()
        {
            if (m_AllDestructibles == null)
                m_AllDestructibles = new HashSet<Destructible>();

            m_AllDestructibles.Add(this);
        }

        protected virtual void OnDestroy()
        {
            m_AllDestructibles.Remove(this);
        }

        public const int TeamIdNeutral = 0;
        [SerializeField] private int m_TeamId;
        public int TeamId => m_TeamId;


        [SerializeField] private UnityEvent m_EventOnDeath;
        public UnityEvent EventOnDeath => m_EventOnDeath;

        protected void SetIndestruct()
        {
            m_Indestructible = true;
        }

        protected void SetDestruct()
        {
            m_Indestructible = false;
        }

        #region Score
        [SerializeField] private int m_ScoreValue;
        public int ScoreValue => m_ScoreValue;
        #endregion

    }
}
