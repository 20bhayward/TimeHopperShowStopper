using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 12f;
    public float jumpForce = 8f;
    public LayerMask groundMask;
    private float groundDistance = 0.1f; // Radius for ground check sphere
    public float airMultiplier = 0.4f; // How movement in air differs from ground movement
    private Rigidbody rb;
    private bool isGrounded;
    private Vector3 moveDirection;
    private float groundCheckOffset = -1.5f; // Offset from the player's center to check for ground

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevent Rigidbody from rotating
    }

    void Update()
    {
        // Calculate the position for ground check directly below the player's feet
        Vector3 groundCheckPosition = transform.position + Vector3.up * groundCheckOffset;
        isGrounded = Physics.CheckSphere(groundCheckPosition, groundDistance, groundMask);

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        moveDirection = (transform.right * x + transform.forward * z).normalized;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        if (isGrounded)
        {
            Move(moveDirection, moveSpeed);
        }
        else
        {
            Move(moveDirection, moveSpeed * airMultiplier);
        }
    }

    void Move(Vector3 direction, float speed)
    {
        Vector3 movement = direction * speed * Time.deltaTime; // Calculate movement vector
        rb.MovePosition(rb.position + movement); // Move the player
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Apply jump force
    }
}
