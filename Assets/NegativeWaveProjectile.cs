using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandWave : MonoBehaviour
{
    public float speed = 5f; // Speed of the wave
    public float growthRate = 0.1f; // Rate of growth of the wave
    private float currentSize = 0f; // Current size of the wave

    void Update()
    {
        MoveWave();
        Expand();
    }

    void MoveWave()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void Expand()
    {
        currentSize += growthRate * Time.deltaTime;
        transform.localScale = new Vector3(currentSize, currentSize, currentSize);
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
