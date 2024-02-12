using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class AWeapon : MonoBehaviour, IWeapon
{
    [SerializeField] protected float fireRate;

    protected bool firing;

    protected List<Transform> barrelTransforms;

    private float _lastFireTime;

    public void InitWeapon()
    {
        _lastFireTime = Time.fixedTime - fireRate;
    }

    public void UpdateWeapon()
    {
        if (firing)
        {
            WhileFiring();
        }
    }

    private void WhileFiring()
    {
        if (Time.fixedTime >= _lastFireTime + fireRate)
        {
            _lastFireTime = Time.fixedTime;
            FireOnce();
        }
    }

    public void SetBarrelTransform(List<Transform> barrelTransforms)
    {
        this.barrelTransforms = barrelTransforms;
    }


    public void StartAttack()
    {
        OnStartFiring();
        firing = true;
    }

    public void StopAttack()
    {
        if (firing)
        {
            OnStopFiring();
            firing = false;
        }
    }

    public virtual void OnStartFiring() { }

    public virtual void OnStopFiring() { }

    public abstract void FireOnce();
}