using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Basic class of all interactive objects on scene
    /// </summary>
    public abstract class Entity : MonoBehaviour
    {
        /// <summary>
        /// Objects name for user
        /// </summary>
        [SerializeField]
        private string m_Nickname;
        public string Nickname => m_Nickname;
    }
}

