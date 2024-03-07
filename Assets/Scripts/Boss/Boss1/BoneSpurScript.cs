using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneSpurScript : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
