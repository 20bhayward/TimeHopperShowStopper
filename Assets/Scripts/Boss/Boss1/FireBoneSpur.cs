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
    public float fireDelay = .1f;

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

            Vector3 shotgunPosition = playerPos;
            float randomX = Random.Range(1f, 3f);
            float randomY = Random.Range(-2f, 2f);
            float randomZ = Random.Range(1f, 3f);

            shotgunPosition.x += randomX;
            shotgunPosition.y += randomY;
            shotgunPosition.z += randomZ;

            Vector3 direction = (shotgunPosition - spawnPoint.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(direction);

            GameObject projectile = Instantiate(BoneSpur, spawnPoint.position, rotation);

            yield return new WaitForSeconds(fireDelay); // Wait for 1 second before spawning the next projectile
        }
    }
}