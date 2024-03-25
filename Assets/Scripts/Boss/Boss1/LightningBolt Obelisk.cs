using UnityEngine;

public class LightningBoltObelisk : MonoBehaviour
{
    public GameObject lightningBoltPrefab; // Prefab for the lightning bolt
    public float attackInterval = 45f; // Time interval between each attack
    public float attackRange = 100f; // Maximum range for the attack
    public Transform head; // The point from which the lightning bolt will be fired
    public LayerMask playerLayer; // Layer mask for the player
    private float timer = 0f;

    private void Update()
    {
        // Increment the timer
        timer += Time.deltaTime;

        // Check if it's time to attack
        if (timer >= attackInterval)
        {
            // Reset the timer
            timer = 0f;

            // Attack the player if within range
            AttackPlayer();
        }
    }

    private void AttackPlayer()
    {
        // Find the player within the attack range
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange, playerLayer);

        // If player found, shoot lightning bolt
        if (hitColliders.Length > 0)
        {
            // Instantiate the lightning bolt at the head position
            Instantiate(lightningBoltPrefab, head.position, Quaternion.identity);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a wire sphere to visualize the attack range
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}