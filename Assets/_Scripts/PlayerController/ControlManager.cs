using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using DG.Tweening;
using MrLule.Attributes;
using MrLule.ExtensionMethods;
using UnityEngine;
using UnityEngine.UI;

public class ControlManager : MonoBehaviour
{
    [SerializeField] public Controller activeController;
    [SerializeField] private float roleChangeMoveSpeed = 0.2f;
    [SerializeField] private float roleChangeDetectionDistance = 2f;
    [SerializeField] private LayerMask whatIsChangeable;
    [SerializeField] private Image holdButtonImage;
    [SerializeField] private float holdWaitTime = 2f;
    [SerializeField] private Transform lastBoundTransform;
    [SerializeField] private float maximumDistanceFromLastTransform = 10f;
    [SerializeField] private Image notOnInventoryImage;
    [SerializeField] private float notOnInventoryShowTime = 3f;
    /*[ShowOnly] */public List<string> inventory = new List<string>();

    public bool isRoleChangeMode = false;
    public bool isWorking = false;

    [SerializeField][ShowOnly] private bool holdingGhost = true;
    [SerializeField][ShowOnly] private bool holdingControl = true;
    [SerializeField][ShowOnly] private float holdStartTime;
    [ShowOnly] public bool playerInWorkArea = false;

    private LineRenderer lineRenderer;
    private Collider2D collider;
    private SpriteRenderer playerImg;
    private bool hasControlableInArea = false;
    private Controller controllableInArea = null;
    private InventoryVisualizer inventoryVisualizer;
    public float xAxis;
    public float yAxis;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        collider = GetComponent<Collider2D>();
        playerImg = GetComponent<SpriteRenderer>();
        inventoryVisualizer = FindObjectOfType<InventoryVisualizer>();
    }

    private void Update()
    {
        if (isWorking)
        {
            return;
        }

        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");
        if (activeController != null)
        {
            lineRenderer.enabled = false;
            hasControlableInArea = false;
            isRoleChangeMode = false;
            controllableInArea = null;
            collider.isTrigger = true;
            playerImg.enabled = false;
            transform.position = activeController.transform.position;
            activeController.movementX = xAxis;
            activeController.isJumpInput = Input.GetAxis("Jump") > 0;
            activeController.SetDialogueBoxState(false);

            if (playerInWorkArea)
            {
                return;
            }

            CheckControllable();
            if (!hasControlableInArea)
            {
                holdingGhost = false;
                return;
            }
            if (playerInWorkArea)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.F) && !controllableInArea.isRequirementSatisfied)
            {
                if (HasObjectInInventory(controllableInArea.requestedItemName))
                {
                    RemoveOPbjectFromInventory(controllableInArea.requestedItemName);
                    controllableInArea.CompleteRequirement();
                }
                else
                {
                    notOnInventoryImage.DOFade(1f, notOnInventoryShowTime / 2f).SetEase(Ease.OutCubic).OnComplete(() =>
                    notOnInventoryImage.DOFade(0f, notOnInventoryShowTime / 2f).SetEase(Ease.OutCubic));
                }
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                holdStartTime = Time.time;
            }
            holdingControl = Input.GetKey(KeyCode.E);
            if (holdingControl)
            {
                holdButtonImage.fillAmount = (Time.time - holdStartTime) / holdWaitTime;
                if (Time.time >= holdStartTime + holdWaitTime)
                {
                    holdingControl = false;
                    holdStartTime = float.PositiveInfinity;
                    activeController?.SetDialogueBoxState(true);
                    lastBoundTransform = activeController.boundPosition;
                    activeController.movementX = 0;
                    activeController.isJumpInput = false;
                    activeController = null;
                    holdButtonImage.fillAmount = 0f;
                    transform.position += Vector3.up * 2f;
                }
            }
            else
            {
                holdButtonImage.fillAmount = 0f;
            }
        }
        else
        {
            if (collider != null)
            {
                collider.isTrigger = false;
                playerImg.enabled = true;
            }
            isRoleChangeMode = true;
            lineRenderer.enabled = true;
            lineRenderer.SetPositions(new Vector3[] { transform.position + Vector3.down * 0.5f, lastBoundTransform.position });
            float distance = Vector3.Distance(transform.position, lastBoundTransform.position);
            if (distance >= maximumDistanceFromLastTransform)
            {
                lineRenderer.startColor = Color.red;
                lineRenderer.endColor = Color.red;
                Vector3 diff = lastBoundTransform.position - transform.position;
                float multiplier = distance - maximumDistanceFromLastTransform;
                diff = diff.normalized * multiplier;
                transform.position += new Vector3(xAxis, yAxis, 0) * roleChangeMoveSpeed + diff;
            }
            else
            {
                lineRenderer.startColor = Color.green;
                lineRenderer.endColor = Color.green;
                transform.position += new Vector3(xAxis, yAxis, 0) * roleChangeMoveSpeed;
            }

            CheckControllable();
            if (!hasControlableInArea)
            {
                holdingGhost = false;
                return;
            }
            if (playerInWorkArea)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.F) && !controllableInArea.isRequirementSatisfied)
            {
                if (HasObjectInInventory(controllableInArea.requestedItemName))
                {
                    RemoveOPbjectFromInventory(controllableInArea.requestedItemName);
                    controllableInArea.CompleteRequirement();
                }
                else
                {
                    notOnInventoryImage.DOFade(1f, notOnInventoryShowTime / 2f).SetEase(Ease.OutCubic).OnComplete(() =>
                    notOnInventoryImage.DOFade(0f, notOnInventoryShowTime / 2f).SetEase(Ease.OutCubic));
                }
            }

            if (!controllableInArea.isRequirementSatisfied)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                holdStartTime = Time.time;
            }
            holdingGhost = Input.GetKey(KeyCode.E);
            if (holdingGhost)
            {
                holdButtonImage.fillAmount = (Time.time - holdStartTime) / holdWaitTime;
                if (Time.time >= holdStartTime + holdWaitTime)
                {
                    holdingGhost = false;
                    holdStartTime = float.PositiveInfinity;
                    activeController = controllableInArea;
                    holdButtonImage.fillAmount = 0f;
                }
            }
            else
            {
                holdButtonImage.fillAmount = 0f;
            }
        }
    }

    public bool AddObjectToInventory(string objectToAdd)
    {
        inventory.Add(objectToAdd);
        inventoryVisualizer.UpdateVisual();
        return true;
    }

    public bool HasObjectInInventory(string objectToCheck)
    {
        return inventory.Contains(objectToCheck);
    }

    public void RemoveOPbjectFromInventory(string objectToRemove)
    {
        if (inventory.Contains(objectToRemove))
        {
            inventory.Remove(objectToRemove);
            inventoryVisualizer.UpdateVisual();
        }
    }

    private void CheckControllable()
    {
        if (isRoleChangeMode)
        {
            Collider2D hitCollider = Physics2D.OverlapCircle(transform.position, roleChangeDetectionDistance, whatIsChangeable);
            if (hitCollider != null)
            {
                hasControlableInArea = true;
                controllableInArea = hitCollider.GetComponent<Controller>();
            }
            else
            {
                hasControlableInArea = false;
                controllableInArea = null;
            }
        }
        else
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, roleChangeDetectionDistance, whatIsChangeable);
            if (hitColliders.Length != 0)
            {
                hasControlableInArea = true;
                foreach (Collider2D hit in hitColliders)
                {
                    if (hit == activeController.GetComponent<Collider2D>())
                    {
                        continue;
                    }
                    else
                    {
                        controllableInArea = hit.GetComponent<Controller>();
                    }
                }
            }
            else
            {
                hasControlableInArea = false;
                controllableInArea = null;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = hasControlableInArea ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position, roleChangeDetectionDistance);
        Gizmos.color = Color.white;
        if (lastBoundTransform != null)
        {
            Gizmos.DrawWireSphere(lastBoundTransform.position, maximumDistanceFromLastTransform);
        }
    }

    private void OnDestroy()
    {
        DOTween.CompleteAll();
        DOTween.Clear();
    }
}
