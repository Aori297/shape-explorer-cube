using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float maxSpeed;
    [SerializeField] private Animator animator;
    private SpriteRenderer spriteRenderer;

    private Vector2 targetPosition;

    private bool isMoving = false;
    private bool levelCompleted = false;
    private bool canMove = false;

    private void Start()
    {
        mainCamera = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        Invoke("EnableMovement", 3f);
    }

    private void EnableMovement()
    {
        canMove = true;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && !levelCompleted && canMove)
        {
            targetPosition = GetMousePosition();
            isMoving = true;

            Vector2 direction = (targetPosition - rb.position).normalized;
            UpdateAnimation(direction);
        }
        else
        {
            isMoving = false;
            if (canMove && !levelCompleted)
            {
                animator.Play("Idle");
            }
        }
    }

    private void FixedUpdate()
    {
        if (isMoving && canMove)
        {
            Vector2 newPosition = Vector2.MoveTowards(rb.position, targetPosition, maxSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPosition);
        }
    }

    private Vector2 GetMousePosition()
    {
        return mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void UpdateAnimation(Vector2 direction)
    {
        if (Mathf.Abs(direction.y) > Mathf.Abs(direction.x))
        {
            if (direction.y > 0)
            {
                animator.Play("Back");
            }
            else
            {
                animator.Play("Front");
            }
        }
        else
        {
            if (Mathf.Abs(direction.x) > 0.1f)
            {
                animator.Play("Side");
                spriteRenderer.flipX = direction.x < 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "EndPoint")
        {
            Debug.Log("Jityo");
            levelCompleted = true;
            animator.Play("Idle");
        }
        else if (collision.gameObject.CompareTag("Incorrect"))
        {
            Debug.Log("Haryo");
        }
    }
}