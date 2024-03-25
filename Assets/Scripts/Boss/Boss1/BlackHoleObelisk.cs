using UnityEngine;

public class BlackHoleObelisk : MonoBehaviour
{
    public GameObject BlackHole; // The GameObject to spawn
    public float spawnInterval = 35f; // Time interval between each spawn
    public Transform spawnPoint; // The point from which the spawnObject will be instantiated

    private float timer = 0f;

    private void Update()
    {
        // Increment the timer
        timer += Time.deltaTime;

        // Check if it's time to spawn the object
        if (timer >= spawnInterval)
        {
            // Reset the timer
            timer = 0f;

            // Spawn the object
            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        // Ensure spawnPoint is assigned
        if (spawnPoint == null)
        {
            Debug.LogError("Spawn point is not assigned!");
            return;
        }

        // Ensure spawnObject is assigned
        if (BlackHole == null)
        {
            Debug.LogError("Spawn object is not assigned!");
            return;
        }

        // Instantiate the spawnObject at the spawnPoint's position and rotation
        Instantiate(BlackHole, spawnPoint.position, spawnPoint.rotation);
    }
}