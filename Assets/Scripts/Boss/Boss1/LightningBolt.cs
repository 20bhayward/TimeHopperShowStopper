using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBolt : MonoBehaviour
{
    [SerializeField] private LayerMask deletionLayers;
    public float speed = 200f; // Speed of the projectile
    float countdown = 1;
    public int damageAmount = 20;
    public GameObject lightningEffect;


    void Update()
    {
        // Move the projectile forward based on its local forward direction and speed
        transform.position += transform.forward * speed * Time.deltaTime;
        countdown -= Time.deltaTime;
        if (countdown <= 0f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the collided object belongs to any of the specified layers
        if ((deletionLayers & (1 << other.gameObject.layer)) != 0)
        {
            Debug.Log("bone spur got deleted");
            DamageUtil.DamageObject(other.gameObject, damageAmount);
            Instantiate(lightningEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}