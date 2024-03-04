using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    // Movement parameters
    public float moveSpeed = 12f;
    public float jumpForce = 10f;
    public int maxJumps = 1; // Total number of jumps allowed
    public LayerMask groundMask; // LayerMask to determine what constitutes ground
    public float airMultiplier = 0.4f; // Movement multiplier for air control

    // Internal state
    private Rigidbody rb;
    private bool isGrounded;
    private int jumpsLeft;
    private Vector3 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevent Rigidbody from rotating
        jumpsLeft = maxJumps;
    }

    void Update()
    {
        // Jumping logic
        if (Input.GetButtonDown("Jump") && jumpsLeft > 0)
        {
            jumpsLeft--;
            Jump();
        }

        // Get input for movement
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        moveDirection = (transform.right * x + transform.forward * z).normalized;

        // Handle movement
        Move(moveDirection * (isGrounded ? moveSpeed : moveSpeed * airMultiplier));
    }

    void Move(Vector3 velocity)
    {
        Vector3 move = velocity * Time.deltaTime;
        rb.MovePosition(rb.position + move);
    }

    void Jump()
    {
        // Reset vertical velocity for a consistent jump
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
            jumpsLeft = maxJumps; // Reset jumps when you touch the ground
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
        }
    }
}
