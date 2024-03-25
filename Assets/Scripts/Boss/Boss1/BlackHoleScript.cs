using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public float attractionForce = 10f;
    public float radius = 100f;
    public float destroyAfter = 10f;
    private float startTime;
    public LayerMask playerLayer;

    private void Start()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        // Destroy the black hole after a certain amount of time
        if (Time.time - startTime > destroyAfter)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Check if the object's layer is within the affected layers
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 direction = transform.position - other.transform.position;
                rb.AddForce(direction.normalized * attractionForce * Time.deltaTime);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a wire sphere to visualize the influence radius
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}