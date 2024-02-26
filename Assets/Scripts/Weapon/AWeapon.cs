using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class AWeapon : MonoBehaviour, IWeapon
{
    [SerializeField] protected float fireRate;

    protected bool firing;

    [SerializeField] protected Transform[] barrelTransforms;

    

    private float _lastFireTime;

    public void InitWeapon()
    {
        _lastFireTime = Time.fixedTime - fireRate;
        SetBarrelTransform(barrelTransforms);
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

    public void SetBarrelTransform(Transform[] barrelTransforms)
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

    public Transform[] GetBarrels()
    {
        return barrelTransforms;
    }

    public virtual void OnStartFiring() { }

    public virtual void OnStopFiring() { }

    public abstract void FireOnce();

    public void SetActive(bool v)
    {
        gameObject.SetActive(v);
    }
}