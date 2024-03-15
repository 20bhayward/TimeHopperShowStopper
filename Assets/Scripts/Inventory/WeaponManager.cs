using UnityEngine;
using UnityEngine.UI; // Required for UI elements manipulation

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] weaponObjects; // Assign weapon GameObjects in the inspector
    private IWeapon[] weapons; // Interface references to the weapons
    private int currentWeaponIndex = 0;

    [SerializeField]
    private GameObject[] crosshairObjects; // Array of crosshair Image objects

    void Start()
    {
        weapons = new IWeapon[weaponObjects.Length];
        for (int i = 0; i < weaponObjects.Length; i++)
        {
            weapons[i] = weaponObjects[i].GetComponent<IWeapon>();
            weaponObjects[i].SetActive(i == 0);
            if (i == 0)
            {
                weapons[i].InitWeapon();
                UpdateCrosshair(i); // Set the initial crosshair
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

        // Handle input for changing weapons
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            currentWeaponIndex = currentWeaponIndex >= weapons.Length - 1 ? 0 : currentWeaponIndex + 1;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            currentWeaponIndex = currentWeaponIndex <= 0 ? weapons.Length - 1 : currentWeaponIndex - 1;
        }

        // Handle number key input for direct weapon selection
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
            UpdateCrosshair(currentWeaponIndex);
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
        weapons[to].InitWeapon();
    }

    private void UpdateCrosshair(int weaponIndex)
    {
        for (int i = 0; i < crosshairObjects.Length; i++)
        {
            if (crosshairObjects[i])
            {
                crosshairObjects[i].SetActive(i == weaponIndex);
            }
        }
    }
}
