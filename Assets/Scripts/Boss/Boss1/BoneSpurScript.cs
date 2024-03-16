using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneSpurScript : MonoBehaviour
{
    [SerializeField] private LayerMask deletionLayers;
    public float speed = 50f; // Speed of the projectile
    public int damageAmount = 10;

    void Update()
    {
        // Move the projectile forward based on its local forward direction and speed
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the collided object belongs to any of the specified layers
        if ((deletionLayers & (1 << other.gameObject.layer)) != 0)
        {
            Debug.Log("bone spur got deleted");
            DamageUtil.DamageObject(other.gameObject, damageAmount);
            Destroy(gameObject);
        }
    }
}