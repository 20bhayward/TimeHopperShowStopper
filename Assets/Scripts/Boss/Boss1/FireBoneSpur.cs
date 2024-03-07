using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBoneSpur : MonoBehaviour
{
    [SerializeField] private int projectileCount;
    [SerializeField] private bool homing;
    public GameObject BoneSpur;
    public Transform spawnPoint;
    public float speed = 4;
    public int projectilesToFire = 5; // Number of projectiles to fire at once
    private List<GameObject> projectiles = new List<GameObject>();
    void Start()
    {
        
    }


    void SpawnProjectile(Vector3 playerPos)
    {
        Vector3 direction = (playerPos - spawnPoint.position).normalized;

        GameObject projectile = Instantiate(BoneSpur, spawnPoint.transform.position, Quaternion.LookRotation(direction));

        projectiles.Add(projectile);

        if (projectiles.Count >= projectilesToFire)
        {
            ShootAtPlayer(playerPos);
        }
    }

    void ShootAtPlayer(Vector3 playerPos)
    {
        Vector3 direction = (playerPos - spawnPoint.position).normalized;
        Destroy(BoneSpur, 5f);

        foreach (var projectile in projectiles)
        {
            projectile.transform.position += direction * speed * Time.deltaTime;
        }


        projectiles.Clear();
    }
}
