using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class NPCShowDialogue : MonoBehaviour
{
    [SerializeField] private float areaCheckRadius = 3f;
    [SerializeField] private CanvasGroup dialogueBox;
    [SerializeField] private LayerMask dialogueTriggerLayers;
    [SerializeField] private float showDuration = 1f;
    [SerializeField] private Ease showEase = Ease.OutCubic;
    [SerializeField] private float hideDuration = 0.5f;
    [SerializeField] private Ease hideEase = Ease.OutCubic;
    [SerializeField] private float dialogueBoxMoveUpAmount = 2f;

    private Controller controller;
    private bool isShowing  = false;

    private void Start()
    {
        controller = GetComponent<Controller>();
    }

    private void Update()
    {
        Collider2D hitCollider = Physics2D.OverlapCircle(transform.position, areaCheckRadius, dialogueTriggerLayers);
        if (hitCollider != null && hitCollider != GetComponent<Collider2D>())
        {
            ShowDialogueBox();
        }
        else
        {
            HideDialogueBox();
        }
    }

    public void ShowDialogueBox()
    {
        if (isShowing)
        {
            return;
        }
        isShowing = true;
        dialogueBox.transform.DOScale(new Vector3(controller.isFacingRight ? 1 : -1, 1, 1), showDuration).SetEase(showEase);
        dialogueBox.transform.DOLocalMoveY(dialogueBoxMoveUpAmount, showDuration).SetEase(showEase);
        dialogueBox.DOFade(1, showDuration).SetEase(showEase);
    }

    public void HideDialogueBox()
    {
        if (!isShowing)
        {
            return;
        }
        isShowing = false;
        dialogueBox.transform.DOScale(Vector3.zero, showDuration).SetEase(showEase);
        dialogueBox.transform.DOLocalMoveY(0f, showDuration).SetEase(showEase);
        dialogueBox.DOFade(0, hideDuration).SetEase(hideEase);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + Vector3.up * dialogueBoxMoveUpAmount, Vector3.one);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, areaCheckRadius);
        Gizmos.color = Color.white;
    }
}
