using MrLule.Interfaces;
using UnityEngine;

namespace MrLule.Managers.AudioMan
{
    public class AudioStartTrigger : MonoBehaviour, IInteractable
    {
        [SerializeField] private string audioName;

        private bool onArea = false;
        private float areaPercent = 0f;

        private AudioManager audioManager;

        private void Start()
        {
            audioManager = AudioManager.Instance;
        }

        public void AreaState(bool onArea)
        {
            this.onArea = onArea;
        }

        public void InAreaPercent(float percent)
        {
            areaPercent = percent;
        }

        public void Interact()
        {
            audioManager.Play(audioName);
        }
    }
}
