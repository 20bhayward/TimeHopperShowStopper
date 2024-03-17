using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseExpolsionScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float delay = 3f;
    public float blastRadius = 5f;
    public int damageAmount = 50;

    public GameObject explosionEffect;

    float countdown;
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if(countdown <= 0f)
        {
            Explode();
        }
    }


    void Explode()
    {
        //Instantiate(explosionEffect, transform.position, transform.rotation);

        // Create a layer mask for the objects you want to collide with
        LayerMask collisionMask = LayerMask.GetMask("Player");

        // Find colliders only on the specified layer within the blast radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius, collisionMask);

        foreach (Collider collider in colliders)
        {
            DamageUtil.DamageObject(collider.gameObject, damageAmount);
        }

        Destroy(gameObject);
    }
}
