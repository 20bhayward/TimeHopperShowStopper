using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Player/Inventory")]
public class Inventory : MonoBehaviour, IInventory
{
    [SerializeField] private AWeapon[] _inventory;
    [SerializeField] private uint _currSlot;

    public void AssignWeaponSlot(uint slot, AWeapon weapon)
    {
        if (slot >= _inventory.Length)
        {
            throw new ArgumentOutOfRangeException("Slot index " + slot + " out of range!");
        }

        _inventory[slot] = weapon;
    }

    public bool HasWeaponSlot(uint slot)
    {
        if (slot >= _inventory.Length)
        {
            throw new ArgumentOutOfRangeException("Slot index " + slot + " out of range!");
        }

        return _inventory[slot] != null;
    }

    public IWeapon GetWeaponAtSlot(uint slot)
    {
        if (slot >= _inventory.Length)
        {
            throw new ArgumentOutOfRangeException("Slot index " + slot + " out of range!");
        }

        return _inventory[slot];
    }

    public IWeapon GetCurrWeapon()
    {
        if (!HasWeaponSlot(_currSlot))
        {
            return SwitchToNextWeapon();
        }

        return _inventory[_currSlot];
    }

    public IWeapon SwitchToWeaponAtSlot(uint slot)
    {
        IWeapon weapon = GetWeaponAtSlot(slot);
        if (weapon != null)
        {
            _currSlot = slot;
            return weapon;
        }
        return _inventory[_currSlot];
    }

    public IWeapon SwitchToNextWeapon()
    {
        for (uint i = _currSlot + 1; i < _inventory.Length; i++)
        {
            if (HasWeaponSlot(i))
            {
                _currSlot = i;
                return _inventory[i];
            }
        }

        for (uint i = 0; i < _currSlot; i++)
        {
            if (HasWeaponSlot(i))
            {
                _currSlot = i;
                return _inventory[i];
            }
        }

        return _inventory[_currSlot];
    }

    public IWeapon SwitchToPrevWeapon()
    {
        for (int i = (int)_currSlot - 1; i >= 0; i--)
        {
            if (HasWeaponSlot((uint)i))
            {
                _currSlot = (uint)i;
                return _inventory[i];
            }
        }

        for (int i = _inventory.Length - 1; i > _currSlot; i--)
        {
            if (HasWeaponSlot((uint)i))
            {
                _currSlot = (uint)i;
                return _inventory[i];
            }
        }

        return _inventory[_currSlot];
    }
}