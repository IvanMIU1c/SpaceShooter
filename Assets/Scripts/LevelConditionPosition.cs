using UnityEngine;
namespace SpaceShooter
{
    public class LevelConditionPosition : MonoBehaviour, ILevelCondition
    {
        [SerializeField] private Transform targetPosition; // �������, ������� ����� �������
        [SerializeField] private float radius; // ������ �������, � ������� ����� ����������
        private bool m_win;
        private bool m_Reached;

        public LevelConditionScore conditionScore;


        bool ILevelCondition.IsCompleted
        {
            get
            {
                if (m_win == true)
                {
                    m_Reached = true;
                    Debug.Log(m_Reached);
                    return m_Reached;
                }
                return m_Reached;
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            m_win = true;
            conditionScore.setScore();
        }

        private void Update()
        {
            // ���� ���������� ����� ������� �������� � ������� ������ �������, �� ������� �������
            if (Vector3.Distance(transform.position, targetPosition.position) <= radius)
            {
                Debug.Log("here1");

                m_win = true;
            }
        }


        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, radius);
        }
    }
}