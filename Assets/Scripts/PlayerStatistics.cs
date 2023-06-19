using UnityEngine;

namespace SpaceShooter
{
    public class PlayerStatistics : MonoBehaviour
    {
        public int numKills;
        public int score;
        public int time;
        public int globalNumKills;
        public int globalScore;
        public int globaltime;
        public void Reset()
        {
            globalNumKills += numKills;
            globalScore += score;
            globaltime += time;

            numKills = 0;
            score = 0;
            time = 0;
        }

        public int getGlobalNumKills()
        {
            return globalNumKills;
        }

        public int getGlobalScore()
        {
            return globalScore;
        }

        public int getGlobalTime()
        {
            return globaltime;
        }

    }
}
