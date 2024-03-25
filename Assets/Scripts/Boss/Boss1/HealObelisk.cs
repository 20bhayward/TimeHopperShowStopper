using UnityEngine;

public class BossHealing : MonoBehaviour
{
    public int healAmount = 50;
    public float healInterval = 20f;
    public LayerMask enemyLayer;
    public float healRadius = 100f;
    private float timer = 0f;

    private void Update()
    {
        // Increment the timer
        timer += Time.deltaTime;

        // Check if it's time to heal the boss
        if (timer >= healInterval)
        {
            // Reset the timer
            timer = 0f;

            // Call the function to heal the boss
            HealBoss();
        }
    }

    private void HealBoss()
    {
        // Find enemies within the specified radius
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, healRadius, enemyLayer);

        // Loop through all colliders found
        foreach (Collider collider in hitColliders)
        {
            // Call DamageUtil to heal the enemy
            DamageUtil.DamageObject(collider.gameObject, -healAmount);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a wire sphere to visualize the heal radius
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, healRadius);
    }
}