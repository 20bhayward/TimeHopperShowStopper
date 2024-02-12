using UnityEngine;

public class PlayerInfoManager : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;

    public Inventory GetInventory()
    {
        return _inventory;
    }
}
