using UnityEngine;
using UnityEngine.Events;

public class SteveController : MonoBehaviour
{

    // Injections
    [Range(0, .9f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    public Rigidbody2D rigidBody2D;
    public Animator animator;

    // Controllers
    MovementController movementController;
    AnimationController animationController;
    StatusController statusController = new StatusController(PlayerStatus.idle);

    // Start is called before the first frame update
    void Start()
    {
        movementController = new MovementController(this, m_MovementSmoothing, rigidBody2D, statusController);
        animationController = new AnimationController(this, animator, statusController);
    }

    // Update is called once per frame
    void Update()
    {
        positionController.Update();
        animationController.Update();
    }

    private void FixedUpdate()
    {
        movementController.FixedUpdate();
    }
}


public class AnimationController {
    public Animator animator;
    MonoBehaviour gameObject;
    StatusController statusController;

    public AnimationController(MonoBehaviour _gameObject, Animator _animator, StatusController _statusController)
    {
        gameObject = _gameObject;
        animator = _animator;
        statusController = _statusController;
    }

    public void Update()
    {

    }
}

public class MovementController : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
public class StatusController
{
    public PlayerStatus playerStatus { get; set; }

    public StatusController(PlayerStatus initialStatus)
    {
        playerStatus = initialStatus;
    }
}

public enum PlayerStatus {
   running,
   jumping,
   dying,
   dead,
   takingDamage,
   idle
}