using UnityEngine;

public class PlayerSlide : MonoBehaviour
{
    public Transform orientation;
    public float slideForce = 10f;
    public float maxSlideTime = 2f;
    public KeyCode slideKey = KeyCode.LeftControl;

    private Rigidbody rb;
    private bool isSliding = false;
    private float slideTimer;
    private Vector3 originalScale; // Store the original scale
    private float slideHeightMultiplier = 0.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalScale = transform.localScale; // Save the original scale
    }

    void Update()
    {
        if (Input.GetKeyDown(slideKey))
        {
            StartSlide();
        }
        else if (Input.GetKeyUp(slideKey) || slideTimer <= 0)
        {
            StopSlide();
        }

        if (isSliding)
        {
            slideTimer -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (isSliding)
        {
            SlidingMovement();
        }
    }

    private void StartSlide()
    {
        isSliding = true;
        // Adjust the scale and position to simulate crouching without sinking into the ground
        transform.localScale = new Vector3(originalScale.x, originalScale.y * slideHeightMultiplier, originalScale.z);
        transform.position -= new Vector3(0, (originalScale.y - transform.localScale.y) / 2, 0);
        slideTimer = maxSlideTime;
    }

    private void SlidingMovement()
    {
        Vector3 slideDirection = orientation.forward; // Slide forward relative to player orientation
        rb.AddForce(slideDirection.normalized * slideForce, ForceMode.Force);
    }

    private void StopSlide()
    {
        isSliding = false;
        // Reset the scale and position
        transform.localScale = originalScale;
        transform.position += new Vector3(0, (originalScale.y - transform.localScale.y) / 2, 0);
    }
}
