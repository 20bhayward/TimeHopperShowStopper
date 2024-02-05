using UnityEngine;

public class PlayerInfoManager : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;

    public IInventory GetInventory()
    {
        return _inventory;
    }
}
