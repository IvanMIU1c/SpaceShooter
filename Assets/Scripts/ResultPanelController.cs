using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class ResultPanelController : SingletonBase<ResultPanelController>
    {
        [SerializeField] private Text m_Kills;
        [SerializeField] private Text m_Score;
        [SerializeField] private Text m_Time;

        [SerializeField] private Text m_Result;
        [SerializeField] private Text m_ButtonNexxtText;

        private bool m_success;
        private int bonusScore;
        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void ShowResults(PlayerStatistics levelResults, bool success)
        {
            gameObject.SetActive(true);

            m_success = success;

            m_Result.text = success ? "Win" : "Lose";

            m_ButtonNexxtText.text = success ? "Next" : "Restart";

            m_Kills.text = "Kills : " + levelResults.numKills.ToString();



            if(levelResults.time < 30)
            {
                bonusScore = levelResults.score * (30 - levelResults.time) / 10;

                m_Score.text = "Score: " + levelResults.score + "\nBonus for quick passage: " + bonusScore + "\nTotal score:" + (levelResults.score + bonusScore);
            }

            else
            {
                m_Score.text = "Score : " + levelResults.score.ToString();
            }
            
            
            m_Time.text = "Time : " + levelResults.time.ToString();

            Time.timeScale = 0;
        }

        public void OnButtonNextAction()
        {
            gameObject.SetActive(false);

            Time.timeScale = 1;

            if (m_success)
            {
                LevelSequenceController.Instance.AdvanceLevel();
            }
            else
            {
                LevelSequenceController.Instance.RestartLevel();
            }
        }
    }
}
