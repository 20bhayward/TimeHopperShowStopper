using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompoundDamageable : MonoBehaviour
{
    [SerializeField] private EnemyHealth _health;
    [SerializeField] private float _damageWindow = 0.01f;
    private float _lastDamageTime;


    private void Start()
    {
        _lastDamageTime = -_damageWindow;
       
    }

    public void TakeDamage(float damage)
    {
        if (Time.fixedTime - _lastDamageTime >= _damageWindow)
        {
            _lastDamageTime = Time.fixedTime;
            Debug.Log("Damage Taken: " + damage);
            _health.AdjustHealth(damage);
            
        }
    }


}
