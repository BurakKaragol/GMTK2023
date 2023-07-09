using DG.Tweening;
using MrLule.Managers.AudioMan;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWorkArea : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private string allowedNPCName = "Kedi";
    [SerializeField] private string objectToAdd = "Elma";
    [SerializeField] private int maximumObjectCount = 1;
    [SerializeField] private CanvasGroup requestedNPCImg;
    [SerializeField] private CanvasGroup onAreaInteractableImage;
    [SerializeField] private float interactableButtonDuration = 0.5f;
    [SerializeField] private Ease interactableButtonEase = Ease.OutCubic;
    [SerializeField] private Image fillImage;
    [SerializeField] private Image emptyImage;
    [SerializeField] private float emptyImageShowTime = 2.5f;
    [SerializeField] private float holdWaitTime = 2f;

    public bool playerOnArea;

    private ControlManager player;
    private bool holdingKey = false;
    private float holdStartTime = float.PositiveInfinity;

    private void Update()
    {
        if (!playerOnArea)
        {
            return;
        }

        bool keyState = Input.GetKey(KeyCode.E);
        if (keyState)
        {
            if (maximumObjectCount <= 0)
            {
                //emptyImage.DOFade(1f, emptyImageShowTime / 2f).SetEase(Ease.OutCubic).OnComplete(() =>
                //emptyImage.DOFade(0f, emptyImageShowTime / 2f).SetEase(Ease.OutCubic));
                return;
            }

            if (holdingKey)
            {
                if (Time.time >= holdStartTime + holdWaitTime)
                {
                    holdingKey = false;
                    fillImage.fillAmount = 0f;
                    player.AddObjectToInventory(objectToAdd);
                    AudioManager.Instance.Play("CompleteJob");
                    maximumObjectCount--;
                    player.isWorking = false;
                }
                else
                {
                    fillImage.fillAmount = (Time.time - holdStartTime) / holdWaitTime;
                }
            }
            else
            {
                holdingKey = true;
                player.isWorking = true;
                player.xAxis = 0;
                player.yAxis = 0;
                holdStartTime = Time.time;
            }
        }
        else
        {
            holdingKey = false;
            fillImage.fillAmount = 0f;
            holdStartTime = float.PositiveInfinity;
            player.isWorking = false;
        }
    }

    private void OnDestroy()
    {
        DOTween.CompleteAll();
        DOTween.Clear();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            player = collision.GetComponent<ControlManager>();
            if (player.isRoleChangeMode)
            {
                return;
            }
            if (player.activeController.NPCName != allowedNPCName)
            {
                requestedNPCImg.DOFade(1f, interactableButtonDuration).SetEase(interactableButtonEase);
                requestedNPCImg.transform.DOLocalMoveY(1f, interactableButtonDuration).SetEase(interactableButtonEase);
                return;
            }
            playerOnArea = true;
            player.playerInWorkArea = true;
            onAreaInteractableImage.DOFade(1f, interactableButtonDuration).SetEase(interactableButtonEase);
            onAreaInteractableImage.transform.DOLocalMoveY(1f, interactableButtonDuration).SetEase(interactableButtonEase);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        requestedNPCImg.DOFade(0f, interactableButtonDuration).SetEase(interactableButtonEase);
        requestedNPCImg.transform.DOLocalMoveY(0f, interactableButtonDuration).SetEase(interactableButtonEase);
        if (!playerOnArea)
        {
            return;
        }
        if (collision.CompareTag(playerTag))
        {
            playerOnArea = false;
            player.playerInWorkArea = false;
            onAreaInteractableImage.DOFade(0f, interactableButtonDuration).SetEase(interactableButtonEase);
            onAreaInteractableImage.transform.DOLocalMoveY(0f, interactableButtonDuration).SetEase(interactableButtonEase);
            player = null;
        }
    }
}
