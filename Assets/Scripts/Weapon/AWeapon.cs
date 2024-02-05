using System;
using UnityEngine;

[Serializable]
public abstract class AWeapon : ScriptableObject, IWeapon
{
    [SerializeField] protected uint maxAmmo;
    [SerializeField] protected uint currAmmo;

    [SerializeField] protected float fireRate;

    protected bool firing;

    protected Transform barrelTransform;

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
            if (maxAmmo != 0)
            {
                currAmmo--;
            }
            FireOnce();
        }

        if (currAmmo == 0 && maxAmmo > 0)
        {
            StopAttack();
        }
    }

    public void SetBarrelTransform(Transform barrelTransform)
    {
        this.barrelTransform = barrelTransform;
    }

    public bool IsAmmoFull()
    {
        return currAmmo == maxAmmo;
    }

    public void AddAmmo(uint ammo)
    {
        if (currAmmo + ammo > maxAmmo)
        {
            currAmmo = maxAmmo;
            return;
        }
        currAmmo += ammo;
    }

    public uint GetAmmo()
    {
        return currAmmo;
    }

    public void StartAttack()
    {
        if (maxAmmo == 0 || currAmmo > 0)
        {
            OnStartFiring();
            firing = true;
        }
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