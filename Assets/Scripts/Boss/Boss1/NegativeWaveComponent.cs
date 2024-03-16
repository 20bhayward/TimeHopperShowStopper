using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegativeWaveComponent : MonoBehaviour
{
    public float speed = 5f; // Speed of the wave
    public float growthRate = 0.1f; // Rate of growth of the wave
    private float currentSize = 0f; // Current size of the wave
    public GameObject NegativeWave;
    public Transform spawnPoint;
    private GameObject playerGameObject;

    void Start()
    {
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
    }


    private void Update()
    {

    }

    void SpawnProjectile(Vector3 playerPos)
    {
        Vector3 direction = (playerPos - spawnPoint.position).normalized;

        GameObject projectile = Instantiate(NegativeWave, spawnPoint.transform.position, Quaternion.LookRotation(direction));
        StartCoroutine(FireProjectile(projectile, direction));
    }
    IEnumerator FireProjectile(GameObject projectile, Vector3 direction)
    {
        while (true)
        {
            // Move the wave forward
            projectile.transform.Translate(Vector3.forward * speed * Time.deltaTime);

            // Increase the size of the wave
            currentSize += growthRate * Time.deltaTime;
            transform.localScale = new Vector3(currentSize, currentSize, currentSize);
        }
    }
}
