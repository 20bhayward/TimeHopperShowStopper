using UnityEngine;

public interface IWeapon
{
    public void InitWeapon();

    public void UpdateWeapon();

    public void StartAttack();

    public void StopAttack();

    public bool IsAmmoFull();

    public void AddAmmo(uint ammo);

    public uint GetAmmo();

    public void SetBarrelTransform(Transform barrelTransform);
}