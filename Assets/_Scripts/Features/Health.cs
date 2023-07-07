namespace MrLule.Features
{
    public class Health : Feature
    {
        public void Damage(float decreaseAmount = -1) => Decrease(decreaseAmount);
        public override void Decrease(float decreaseAmount = -1)
        {
            base.Decrease(decreaseAmount);
        }

        public void DamageOverTime(float decreaseAmount, float decreaseStep = 0) => DecreaseOvertime(decreaseAmount, decreaseStep);
        public override void DecreaseOvertime(float decreaseAmount, float decreaseStep = 0)
        {
            base.DecreaseOvertime(decreaseAmount, decreaseStep);
        }

        public override float GetCurrentPercent()
        {
            return base.GetCurrentPercent();
        }

        public void Heal(float increaseAmount = -1) => Increase(increaseAmount);
        public override void Increase(float increaseAmount = -1)
        {
            base.Increase(increaseAmount);
        }

        public void HealOverTime(float increaseAmount, float increaseStep = 0) => IncreaseOvertime(increaseAmount, increaseStep);
        public override void IncreaseOvertime(float increaseAmount, float increaseStep = 0)
        {
            base.IncreaseOvertime(increaseAmount, increaseStep);
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
