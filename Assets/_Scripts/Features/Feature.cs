using System.Collections;
using UnityEngine;

namespace MrLule.Features
{
    public class Feature : MonoBehaviour
    {
        public float Current { get => current; set => current = value; }
        public float Maximum { get => maximum; set => maximum = value; }
        public float ChangeStep { get => changeStep; set => changeStep = value; }
        public float Target { get => target; set => target = value; }

        [SerializeField] protected float current;
        [SerializeField] protected float maximum;
        [SerializeField] protected float changeStep;
        [SerializeField] protected bool canRecover;
        [SerializeField] protected float recover;
        [SerializeField] protected bool useHitPoint;
        [SerializeField] protected float hitPoint;

        protected float target;

        public virtual void Update()
        {
            if (canRecover)
            {
                Increase(recover * Time.deltaTime);
            }
        }

        public virtual float GetCurrentPercent()
        {
            return current / maximum;
        }

        public virtual void Decrease(float decreaseAmount = -1f)
        {
            if (decreaseAmount == -1f || useHitPoint)
            {
                decreaseAmount = hitPoint;
            }
            current = current - decreaseAmount <= 0 ? 0 : current - decreaseAmount;
        }

        public virtual void DecreaseOvertime(float decreaseAmount, float decreaseStep = 0)
        {
            if (decreaseStep == 0)
            {
                StartCoroutine(DecreaseOverTime(changeStep, decreaseAmount));
            }
            else
            {
                StartCoroutine(DecreaseOverTime(decreaseStep, decreaseAmount));
            }
        }

        public virtual void Increase(float increaseAmount = -1)
        {
            if (increaseAmount == -1f || useHitPoint)
            {
                increaseAmount = hitPoint;
            }
            current = current + increaseAmount >= maximum ? maximum : current + increaseAmount;
        }

        public virtual void IncreaseOvertime(float increaseAmount, float increaseStep = 0)
        {
            if (increaseStep == 0)
            {
                StartCoroutine(IncreaseOverTime(changeStep, increaseAmount));
            }
            else
            {
                StartCoroutine(IncreaseOverTime(increaseStep, increaseAmount));
            }
        }

        IEnumerator IncreaseOverTime(float increasePerStep, float target)
        {
            while (current < target)
            {
                current += increasePerStep;
                yield return new WaitForSeconds(0.1f);
            }
            current = target;
        }

        IEnumerator DecreaseOverTime(float decreasePerStep, float target)
        {
            while (current > target)
            {
                current -= decreasePerStep;
                yield return new WaitForSeconds(0.1f);
            }
            current = target;
        }
    }
}
