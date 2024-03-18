using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePoisonBomb : MonoBehaviour
{
    public GameObject PoisonBomb;
    public Transform spawnPoint;
    public int projectilesToFire = 1; // Number of projectiles to fire at once
    private GameObject playerGameObject;

    void Start()
    {
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // Check for "G" key press(This is just for testing, when actually implemented, comment/remove this function and just call SpawnAndShootProjectile from the node
        if (Input.GetKeyDown(KeyCode.B))
        {
            PlayerInfoManager playerInfoManager = playerGameObject.GetComponent<PlayerInfoManager>();
            SpawnAndShootProjectiles(playerInfoManager.GetPos());
        }
    }

    public void SpawnAndShootProjectiles(Vector3 playerPos)
    {
        Debug.Log("Spawning and shooting projectiles");
       // playerPos.y -= 4; //I want the bomb to aim at the players feet, so it hits the ground near them and explodes;

        for (int i = 0; i < projectilesToFire; i++)
        {

            Vector3 direction = (playerPos - spawnPoint.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(direction);
            GameObject projectile = Instantiate(PoisonBomb, spawnPoint.position, rotation);
        }
    }
}