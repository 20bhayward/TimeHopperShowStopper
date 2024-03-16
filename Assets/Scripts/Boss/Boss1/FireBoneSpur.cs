using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBoneSpur : MonoBehaviour
{
    [SerializeField] private bool homing;
    public GameObject BoneSpur;
    public Transform spawnPoint;
    public float speed = 50;
    public int projectilesToFire = 34; // Number of projectiles to fire at once
    private GameObject playerGameObject;

    void Start()
    {
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // Check for "G" key press(This is just for testing, when actually implemented, comment/remove this function and just call SpawnAndShootProjectile from the node
        if (Input.GetKeyDown(KeyCode.G))
        {
            PlayerInfoManager playerInfoManager = playerGameObject.GetComponent<PlayerInfoManager>();
            SpawnAndShootProjectiles(playerInfoManager.GetPos());
        }
    }

    public void SpawnAndShootProjectiles(Vector3 playerPos)
    {
        Debug.Log("Spawning and shooting projectiles");

        StartCoroutine(SpawnProjectilesWithDelay(playerPos));
    }

    private IEnumerator SpawnProjectilesWithDelay(Vector3 playerPos)
    {
        for (int i = 0; i < projectilesToFire; i++)
        {
            Debug.Log("spawned " + i);
            Vector3 direction = (playerPos - spawnPoint.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(direction);

            GameObject projectile = Instantiate(BoneSpur, spawnPoint.position, rotation);

            yield return new WaitForSeconds(.5f); // Wait for 1 second before spawning the next projectile
        }
    }
}