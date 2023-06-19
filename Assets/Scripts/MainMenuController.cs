using UnityEngine;

namespace SpaceShooter
{
    public class MainMenuController : SingletonBase<MainMenuController>
    {
        [SerializeField] private SpaceShip m_DefaultSpaceShip;

        [SerializeField] private GameObject m_EpisodeSelection;

        [SerializeField] private GameObject m_ShipSelection;

        [SerializeField] private GameObject m_ShowStats;

        private void Start()
        {
            LevelSequenceController.PlayerShip = m_DefaultSpaceShip; 
        }

        public void OnButtonStartNew()
        {
            m_EpisodeSelection.gameObject.SetActive(true);

            gameObject.SetActive(false);
        }

        public void OnSelectShip()
        {
            m_ShipSelection.SetActive(true);
            gameObject.SetActive(false);
        }

        public void OnButtonExit()
        {
            Application.Quit();
        }

        public void OnButtonShowStats()
        {
            m_ShowStats.SetActive(true);

            gameObject.SetActive(false);
        }
    }
}
