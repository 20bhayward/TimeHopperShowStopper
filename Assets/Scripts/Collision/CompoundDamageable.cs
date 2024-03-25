using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompoundDamageable : MonoBehaviour
{
    [SerializeField] private EnemyHealth _health;
    [SerializeField] private float _damageWindow = 0.01f;
    private float _lastDamageTime;
    private bool _invulnerable = false;


    private void Start()
    {
        _lastDamageTime = -_damageWindow;
       
    }

    public void TakeDamage(float damage)
    {
        if (_invulnerable)
        {
            return;
        }
        if (Time.fixedTime - _lastDamageTime >= _damageWindow)
        {
            _lastDamageTime = Time.fixedTime;
            _health.AdjustHealth(damage);
            
        }
    }

    public void SetInvulnerability(bool invulnerable)
    {
        _invulnerable = invulnerable;
    }
}
