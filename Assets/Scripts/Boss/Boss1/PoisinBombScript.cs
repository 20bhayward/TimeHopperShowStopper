using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonBombScript : MonoBehaviour
{
    [SerializeField] private LayerMask deletionLayers;
    public float speed = 20f; // Speed of the projectile
    public GameObject PoisonBombCloud;
    public int damageAmount = 5;

    void Update()
    {
        // Move the projectile forward based on its local forward direction and speed
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {

        Debug.Log("I TOUCHED SOMETHING");
        // Check if the collided object belongs to any of the specified layers
        if ((deletionLayers & (1 << other.gameObject.layer)) != 0)
        {
            Debug.Log("poison bomb got deleted");
            DamageUtil.DamageObject(other.gameObject, damageAmount);
            GameObject projectile = Instantiate(PoisonBombCloud, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
