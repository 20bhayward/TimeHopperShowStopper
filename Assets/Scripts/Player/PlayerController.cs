using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 12f;
    public float jumpForce = 8f;
    public LayerMask groundMask; // Define what layers count as the ground
    public Transform groundCheck;
    public float groundDistance = 0.4f; // Radius of the ground check sphere
    public float airMultiplier = 0.4f; // Control how movement in air differs from ground movement

    private Rigidbody rb;
    private bool isGrounded;
    private Vector3 moveDirection;

    // Sliding
    public float slideSpeedMultiplier = 1.5f; // Increase to boost speed while sliding
    public float slideDuration = 0.8f; // Duration of the slide
    public KeyCode slideKey = KeyCode.LeftControl; // Key to initiate slide
    private bool isSliding = false;
    private float originalColliderHeight; // To store the original height of the player's collider
    public CapsuleCollider playerCollider; // Reference to the player's CapsuleCollider

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        if (playerCollider != null)
        {
            originalColliderHeight = playerCollider.height;
        }
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        moveDirection = (transform.right * x + transform.forward * z).normalized;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        if (isGrounded && !isSliding)
        {
            Move(moveDirection, moveSpeed);
            if (Input.GetKeyDown(slideKey))
            {
                StartCoroutine(Slide());
            }
        }
        else if (!isGrounded)
        {
            Move(moveDirection, moveSpeed * airMultiplier);
        }
    }


    [SerializeField] private Inventory _inventory;

    public IInventory GetInventory()
    {
        return _inventory;
    }


    void Move(Vector3 direction, float speed)
    {
        Vector3 movement = direction * speed * Time.deltaTime;
        // Move the player by changing the Rigidbody's position
        rb.MovePosition(rb.position + movement);
    }

    void Jump()
    {
        // Add a vertical force for jumping
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        // Optional: Scale the jump force by the current speed to simulate momentum-based jumping
        // This can make jumps feel more dynamic and dependent on the player's movement speed
        float currentSpeed = rb.velocity.magnitude;
        Vector3 scaledJumpDirection = Vector3.up + moveDirection.normalized * (currentSpeed / moveSpeed);
        rb.AddForce(scaledJumpDirection * jumpForce, ForceMode.Impulse);
    }

    IEnumerator Slide()
    {
        isSliding = true;
        playerCollider.height = originalColliderHeight / 2; // Reduce the collider's height
        Vector3 slideDirection = new Vector3(moveDirection.x, -0.1f, moveDirection.z); // Move down slightly

        // Apply an initial slide force
        rb.AddForce(slideDirection * moveSpeed * slideSpeedMultiplier, ForceMode.VelocityChange);

        yield return new WaitForSeconds(slideDuration);

        // Reset collider height and sliding state
        playerCollider.height = originalColliderHeight;
        isSliding = false;
    }

}
