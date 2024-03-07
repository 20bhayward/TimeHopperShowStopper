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


    void SpawnProjectile(Vector3 playerPos)
    {
        Vector3 direction = (playerPos - spawnPoint.position).normalized;

        GameObject projectile = Instantiate(NegativeWave, spawnPoint.transform.position, Quaternion.LookRotation(direction));

    }
}
