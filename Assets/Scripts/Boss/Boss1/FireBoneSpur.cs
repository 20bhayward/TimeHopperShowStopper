using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBoneSpur : MonoBehaviour
{
    [SerializeField] private bool homing;
    public GameObject BoneSpur;
    public Transform spawnPoint;
    public float speed = 20;
    public int projectilesToFire = 4; // Number of projectiles to fire at once
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

        Vector3 direction = (playerPos - spawnPoint.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);

        GameObject projectile = Instantiate(BoneSpur, spawnPoint.position, rotation);
    }
}