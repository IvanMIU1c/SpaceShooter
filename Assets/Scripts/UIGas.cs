using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class UIGas : MonoBehaviour
    {
        [SerializeField] private bag bag;
        [SerializeField] private Text text;

        private void Start()
        {
            bag.ChangeAmountGas.AddListener(OnChangeHitPoints);
        }

        private void OnDestroy()
        {
            bag.ChangeAmountGas.RemoveListener(OnChangeHitPoints);
        }

        private void OnChangeHitPoints()
        {
            text.text = bag.GetGas().ToString();
        }
    }
}