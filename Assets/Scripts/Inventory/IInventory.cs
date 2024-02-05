public interface IInventory
{
    public bool HasWeaponSlot(uint slot);

    public void AssignWeaponSlot(uint slot, AWeapon weapon);

    public IWeapon GetWeaponAtSlot(uint slot);

    public IWeapon GetCurrWeapon();

    public IWeapon SwitchToWeaponAtSlot(uint slot);

    public IWeapon SwitchToNextWeapon();

    public IWeapon SwitchToPrevWeapon();
}