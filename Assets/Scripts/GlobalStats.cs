using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class GlobalStats : SingletonBase<GlobalStats>
    {
        [SerializeField] private Text m_Kills;
        [SerializeField] private Text m_Score;
        [SerializeField] private Text m_Time;

        [SerializeField] private Text m_ButtonNextText;

        private int a;
        private int b;
        private int c;

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void UpdateResults(PlayerStatistics levelResults)
        {
            a += levelResults.globalNumKills;
            b += levelResults.globalScore; 
            c += levelResults.globaltime;
        }

        public void ShowResults()
        {
            gameObject.SetActive(true);
            
            m_ButtonNextText.text = "Back";

            m_Kills.text = "Kills : " + a;

            m_Score.text = "Score :" + b;

            m_Time.text = "Time : " + c;

            Debug.Log("here");
        }


    }
}
