using System.Collections;
using UnityEngine;
using TMPro;

namespace MrLule.Features
{
    public class Currency : MonoBehaviour
    {
        [Header("General:")]
        [Space(5)]
        [SerializeField] private int money;
        [SerializeField] private int changeStep;

        [Header("UI Elements:")]
        [Space(5)]
        [SerializeField] private bool useText;
        [SerializeField] private TextMeshProUGUI text;

        public Currency(int money = 0)
        {
            this.money = money;
        }

        public int GetMoney()
        {
            return money;
        }

        public bool CanBuy(int price)
        {
            if (money >= price)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Add(int addAmount)
        {
            money += addAmount;
            UpdateText();
        }

        public void AddOverTime(int addAmount, int increaseAmount = 0)
        {
            if (increaseAmount == 0)
            {
                StartCoroutine(IncreaseOverTime(changeStep, addAmount));
            }
            else
            {
                StartCoroutine(IncreaseOverTime(increaseAmount, addAmount));
            }
        }

        public void Remove(int removeAmount)
        {
            money -= removeAmount;
            UpdateText();
        }

        public void RemoveOverTime(int removeAmount, int decreaseAmount = 0)
        {
            if (decreaseAmount == 0)
            {
                StartCoroutine(DecreaseOverTime(changeStep, removeAmount));
            }
            else
            {
                StartCoroutine(DecreaseOverTime(decreaseAmount, removeAmount));
            }
        }

        private void UpdateText()
        {
            if (useText)
            {
                text.SetText(GetMoney().ToString());
            }
        }

        IEnumerator IncreaseOverTime(int increaseAmount, int target)
        {
            while (money < target)
            {
                money += increaseAmount;
                yield return new WaitForSeconds(0.1f);
                UpdateText();
            }
            money = target;
            UpdateText();
        }

        IEnumerator DecreaseOverTime(int decreaseAmount, int target)
        {
            while (money > target)
            {
                money -= decreaseAmount;
                yield return new WaitForSeconds(0.1f);
                UpdateText();
            }
            money = target;
            UpdateText();
        }
    }
}
