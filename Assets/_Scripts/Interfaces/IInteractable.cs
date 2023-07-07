namespace MrLule.Interfaces
{
    public interface IInteractable
    {
        public void AreaState(bool onArea);
        public void InAreaPercent(float percent);
        public void Interact();
    }
}
