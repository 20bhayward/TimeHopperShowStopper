using UnityEngine;

public class PlayerInfoManager : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;

    public Inventory GetInventory()
    {
        return _inventory;
    }

    public Vector3 GetPos()
    {
        return transform.position;
    }
}
