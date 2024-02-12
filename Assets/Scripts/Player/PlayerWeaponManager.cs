using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    [SerializeField] private PlayerInfoManager _playerInfoManager;
    [SerializeField] private List<Transform> _barrelTransform;

    private Inventory _inventory;

    private IWeapon _currWeapon;

    private void Start()
    {
        _inventory = _playerInfoManager.GetInventory();
        SwitchToWeapon(_inventory.GetCurrWeapon());
    }

    private void Update()
    {
        _currWeapon.UpdateWeapon();
        // Check for fire button press (e.g., left mouse button)
        if (Input.GetButtonDown("Fire1")) // "Fire1" is typically the left mouse button or Ctrl in Unity's Input Manager
        {
            StartAttack();
        }
        else if (Input.GetButtonUp("Fire1")) // Ensures firing stops when the button is released
        {
            StopAttack();
        }
    }

    public void SwitchToSlot(uint slot)
    {
        SwitchToWeapon(_inventory.SwitchToWeaponAtSlot(slot));
    }

    public void NextWeapon()
    {
        SwitchToWeapon(_inventory.SwitchToNextWeapon());
    }

    public void PrevWeapon()
    {
        SwitchToWeapon(_inventory.SwitchToPrevWeapon());
    }    

    private void SwitchToWeapon(IWeapon weapon)
    {
        if (weapon == _currWeapon)
        {
            return;
        }

        weapon.SetBarrelTransform(_barrelTransform);
        weapon.InitWeapon();
        _currWeapon = weapon;
    }

    public void StartAttack()
    {
        _currWeapon.StartAttack();
    }

    public void StopAttack()
    {
        _currWeapon.StopAttack();
    }
}
