using System;
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
    private CapsuleCollider collider;
    private Vector3 originalCenter; // Original center of the collider
    private float originalHeight; // Original height of the collider
    public Transform cameraTransform; // Reference to the camera transform
    private float originalCameraHeight; // Original local position y of the camera


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<CapsuleCollider>();
        originalHeight = collider.height;
        originalCenter = collider.center;
        if (cameraTransform != null)
        {
            originalCameraHeight = cameraTransform.localPosition.y;
        }
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
        collider.height = originalHeight / 2; // Halve the collider's height
        collider.center = new Vector3(originalCenter.x, originalCenter.y / 2, originalCenter.z); // Adjust the center
        if (cameraTransform != null)
        {
            // Lower the camera to simulate sliding perspective
            cameraTransform.localPosition = new Vector3(cameraTransform.localPosition.x, originalCameraHeight / 2, cameraTransform.localPosition.z);
        }
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
        collider.height = originalHeight; // Restore the original height
        collider.center = originalCenter; // Restore the original center
        if (cameraTransform != null)
        {
            // Restore the camera's original position
            cameraTransform.localPosition = new Vector3(cameraTransform.localPosition.x, originalCameraHeight, cameraTransform.localPosition.z);
        }
    }

    public bool getIsSliding()
    {
        return isSliding;
    }
}
