using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneSpurScript : MonoBehaviour
{
    [SerializeField] private LayerMask deletionLayers;
    public int damageAmount = 10;

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