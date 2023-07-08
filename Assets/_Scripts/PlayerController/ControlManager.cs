using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using MrLule.Attributes;
using MrLule.ExtensionMethods;
using UnityEngine;
using UnityEngine.UI;

public class ControlManager : MonoBehaviour
{
    [SerializeField] private Controller activeController;
    [SerializeField] private float roleChangeMoveSpeed = 0.2f;
    [SerializeField] private float roleChangeDetectionDistance = 2f;
    [SerializeField] private LayerMask whatIsChangeable;
    [SerializeField] private Image holdButtonImage;
    [SerializeField] private float holdWaitTime = 2f;
    [SerializeField] private Transform lastBoundTransform;
    [SerializeField] private float maximumDistanceFromLastTransform = 10f;

    public bool isRoleChangeMode = false;

    [SerializeField][ShowOnly] private bool holdingGhost = true;
    [SerializeField][ShowOnly] private bool holdingControl = true;
    [SerializeField][ShowOnly] private float holdStartTime;

    private LineRenderer lineRenderer;
    private Collider2D collider;
    private bool hasControlableInArea = false;
    private Controller controllableInArea = null;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        float xAxis = Input.GetAxisRaw("Horizontal");
        float yAxis = Input.GetAxisRaw("Vertical");
        if (activeController != null)
        {
            lineRenderer.enabled = false;
            hasControlableInArea = false;
            controllableInArea = null;
            collider.isTrigger = true;
            transform.position = activeController.transform.position;
            activeController.movementX = xAxis;
            activeController.isJumpInput = Input.GetAxis("Jump") > 0;
            activeController.SetDialogueBoxState(false);

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
                    lastBoundTransform = activeController.transform;
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
            }
            isRoleChangeMode = true;
            lineRenderer.enabled = true;
            lineRenderer.SetPositions(new Vector3[] { transform.position, lastBoundTransform.position });
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

    private void CheckControllable()
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

    private void OnDrawGizmos()
    {
        Gizmos.color = hasControlableInArea ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position, roleChangeDetectionDistance);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(lastBoundTransform.position, maximumDistanceFromLastTransform);
    }
}
