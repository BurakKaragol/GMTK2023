using MrLule.Attributes;
using MrLule.ExtensionMethods;
using MrLule.General;
using MrLule.Managers.AudioMan;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    [SerializeField] public string requestedItemName;
    [SerializeField] public string NPCName = "Kedi";
    [SerializeField] private float movementSpeed = 600f;
    [SerializeField] private float jumpForce = 300f;
    [SerializeField] private float jumpGravity = 1f;
    [SerializeField] private float fallGravity = 2f;
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform dialogueBox;
    [SerializeField] public Transform boundPosition;
    [SerializeField] private NPCShowDialogue npcShowDialogue;
    [SerializeField] private Sprite completedSprite;
    [SerializeField] private Image completedButtonImage;

    [ShowOnly] public float movementX;
    [ShowOnly] public bool isJumpInput;
    [ShowOnly] public bool isFacingRight = true;
    [ShowOnly] public bool isRequirementSatisfied = false;

    private Rigidbody2D rb;
    private Animator animator;
    private float jumpTime;

    [SerializeField][ShowOnly] private bool isGrounded = true;
    [SerializeField][ShowOnly] private bool jumped = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    bool tillki = false;
    private void FixedUpdate()
    {
        if (!isRequirementSatisfied)
        {
            return;
        }
        if (NPCName == "Tilki" && !tillki)
        {
            FindObjectOfType<CameraFocusChanger>().DisableArea();
            tillki = true;
        }

        rb.velocity = new Vector2(movementX * movementSpeed * Time.fixedDeltaTime, rb.velocity.y);
        animator.SetBool("isRunning", rb.velocity.x != 0);
        animator.SetFloat("verticalVelocity", rb.velocity.y);
        if (isFacingRight && rb.velocity.x < 0)
        {
            isFacingRight = false;
            transform.localScale = (Vector3.one * 1.3f).SetX(-1.3f);
        }
        else if (!isFacingRight && rb.velocity.x > 0)
        {
            isFacingRight = true;
            transform.localScale = Vector3.one * 1.3f;
        }

        CheckGround();
        animator.SetBool("isGrounded", isGrounded);
        if (isJumpInput)
        {
            if (jumped)
            {
                return;
            }
            jumped = true;
            jumpTime = Time.time;
            rb.gravityScale = jumpGravity;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        else
        {
            rb.gravityScale = fallGravity;
        }

        if (jumped && isGrounded && Time.time >= jumpTime + 0.5f)
        {
            jumped = false;
        }
    }

    public void CompleteRequirement()
    {
        completedButtonImage.sprite = completedSprite;
        isRequirementSatisfied = true;
    }

    public void SetDialogueBoxState(bool state)
    {
        if (state == npcShowDialogue.enabled)
        {
            return;
        }
        if (state)
        {
            npcShowDialogue.enabled = true;
            npcShowDialogue.ShowDialogueBox();
        }
        else
        {
            npcShowDialogue.HideDialogueBox();
            npcShowDialogue.enabled = false;
        }
    }

    public void PlayFootstep(string audioName)
    {
        AudioManager.Instance.Play(audioName);
    }

    private void CheckGround()
    {
        Collider2D hitCollider = Physics2D.OverlapCircle(groundCheckTransform.position, groundCheckRadius, whatIsGround);
        isGrounded = hitCollider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheckTransform.position, groundCheckRadius);
    }
}
