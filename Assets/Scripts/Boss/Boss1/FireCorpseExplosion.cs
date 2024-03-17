using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCorpseExplosion : MonoBehaviour
{
    public GameObject corpse;
    public Transform spawnPoint;
    public int projectilesToFire = 15; // Number of projectiles to fire at once
    public float radius = 20;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Check for "G" key press(This is just for testing, when actually implemented, comment/remove this function and just call SpawnAndShootProjectile from the node
        if (Input.GetKeyDown(KeyCode.L))
        {
            SpawnAndShootProjectiles();
        }
    }

    public void SpawnAndShootProjectiles()
    {
        for (int i = 0; i < projectilesToFire; i++)
        {
            // Generate a random point within the specified radius
            Vector2 randomPoint = Random.insideUnitCircle * radius;

            // Calculate the position for the corpse relative to the spawnPoint
            Vector3 spawnPosition = spawnPoint.position + new Vector3(randomPoint.x, 0f, randomPoint.y);

            // Spawn the corpse at the calculated position
            Instantiate(corpse, spawnPosition, Quaternion.identity);
        }
    }
}
