using UnityEngine;

public class PlayerInfoManager : MonoBehaviour
{
    [SerializeField] private WeaponManager _inventory;

    public WeaponManager GetInventory()
    {
        return _inventory;
    }

    public Vector3 GetPos()
    {
        return transform.position;
    }
}
