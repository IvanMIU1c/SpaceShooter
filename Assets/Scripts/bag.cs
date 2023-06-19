using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    public class bag : MonoBehaviour
    {
        private int amountGas;
        public UnityEvent ChangeAmountGas;
        public LevelConditionScore level;
        public void Add(int amount)
        {


            amountGas += amount;

            ChangeAmountGas.Invoke();
            if (amountGas == 15)
                level.setScore();

        }
        public bool DrawGas(int amount)
        {
            if (amountGas - amount < 0) return false;
            amountGas -= amount;
            ChangeAmountGas.Invoke();

            return true;
        }

        public int GetGas()
        {
            return amountGas;
        }
    }
}
