using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonBombCloud : MonoBehaviour

{
    public int damageAmount = 1;
    [SerializeField] private LayerMask deletionLayers;
    private float totalTime = 0;

    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        totalTime += Time.deltaTime;
        if (totalTime > 10f)
        {
            Destroy(gameObject);
            Debug.Log("poison bomb got deleted");
        }
    }

    void OnTriggerStay(Collider other)
    {

        // Check if the collided object belongs to any of the specified layers
        if ((deletionLayers & (1 << other.gameObject.layer)) != 0)
        {
            Debug.Log("poison damage tick");
            DamageUtil.DamageObject(other.gameObject, damageAmount);
            
        }
    }
}
