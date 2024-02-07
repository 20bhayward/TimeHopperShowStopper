using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 2f; // More intuitive sensitivity scale
    public Transform playerBody; // Assign the player's transform here to rotate the player body left/right
    public float upDownLookLimit = 90f; // Limit looking up or down

    private float xRotation = 0f; // Rotation around the X axis (for looking up and down)

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
        Cursor.visible = false; // Make the cursor invisible
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Up and down mouse movement -> rotate the camera around the X axis (look up/down)
        xRotation -= mouseY; // Subtract mouseY to invert the vertical axis
        xRotation = Mathf.Clamp(xRotation, -upDownLookLimit, upDownLookLimit);

        // Apply rotation to the camera (for looking up and down)
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Left and right mouse movement -> rotate the player body around the Y axis (turn left/right)
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
