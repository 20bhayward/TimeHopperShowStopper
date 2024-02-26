using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] weaponObjects; // Assign weapon GameObjects in the inspector
    private IWeapon[] weapons; // Interface references to the weapons
    private int currentWeaponIndex = 0;

    void Start()
    {
        // Initialize the weapons array and deactivate all weapons except the first
        weapons = new IWeapon[weaponObjects.Length];
        for (int i = 0; i < weaponObjects.Length; i++)
        {
            weapons[i] = weaponObjects[i].GetComponent<IWeapon>();
            weaponObjects[i].SetActive(i == 0); // Activate the first weapon
            if (i == 0)
            {
                weapons[i].InitWeapon(); // Initialize the first (active) weapon
            }
        }
    }

    void Update()
    {
        HandleWeaponSwitching();
        HandleWeaponFiring();
    }

    private void HandleWeaponSwitching()
    {
        int previousWeaponIndex = currentWeaponIndex;

        // Input for changing weapons using scroll wheel
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            currentWeaponIndex = currentWeaponIndex >= weapons.Length - 1 ? 0 : currentWeaponIndex + 1;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            currentWeaponIndex = currentWeaponIndex <= 0 ? weapons.Length - 1 : currentWeaponIndex - 1;
        }

        // Number key input for direct weapon selection
        for (int i = 0; i < weapons.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                currentWeaponIndex = i;
            }
        }

        // Switch weapon if the index has changed
        if (previousWeaponIndex != currentWeaponIndex)
        {
            SwitchWeapon(previousWeaponIndex, currentWeaponIndex);
        }
    }

    private void HandleWeaponFiring()
    {
        if (weapons[currentWeaponIndex] != null)
        {
            weapons[currentWeaponIndex].UpdateWeapon();
            if (Input.GetButtonDown("Fire1"))
            {
                weapons[currentWeaponIndex].StartAttack();
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                weapons[currentWeaponIndex].StopAttack();
            }
        }
    }

    private void SwitchWeapon(int from, int to)
    {
        weaponObjects[from].SetActive(false);
        weaponObjects[to].SetActive(true);
        weapons[to].InitWeapon(); // Ensure the newly activated weapon is initialized
    }
}
