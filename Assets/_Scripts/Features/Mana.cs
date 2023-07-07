namespace MrLule.Features
{
    public class Mana : Feature
    {
        public void Use(float decreaseAmount = -1) => Decrease(decreaseAmount);
        public override void Decrease(float decreaseAmount = -1)
        {
            base.Decrease(decreaseAmount);
        }

        public void UseOverTime(float decreaseAmount, float decreaseStep) => DecreaseOvertime(decreaseAmount, decreaseStep);
        public override void DecreaseOvertime(float decreaseAmount, float decreaseStep = 0)
        {
            base.DecreaseOvertime(decreaseAmount, decreaseStep);
        }

        public override float GetCurrentPercent()
        {
            return base.GetCurrentPercent();
        }

        public void Gain(float increaseAmount = -1) => Decrease(increaseAmount);
        public override void Increase(float increaseAmount = -1)
        {
            base.Increase(increaseAmount);
        }
        public void GainOverTime(float increaseAmount, float increaseStep) => DecreaseOvertime(increaseAmount, increaseStep);
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
