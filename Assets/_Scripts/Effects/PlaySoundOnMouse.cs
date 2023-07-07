using MrLule.Managers.AudioMan;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaySoundOnMouse : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private string audioName = "Button";

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Instance.Play(audioName);
    }
}
