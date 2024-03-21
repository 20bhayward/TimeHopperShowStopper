using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldInfoManager : MonoBehaviour
{
    [SerializeField] protected WorldController worldController;
    [SerializeField] private List<Obelisk> _obelisks;

    public TimeState GetTimeState()
    {
        return worldController.GetTimeState();
    }

    public bool IsObeliskIntact(int index)
    {
        if (index >= _obelisks.Count || index < 0 || _obelisks[index] == null)
        {
            return false;
        }
        return _obelisks[index].IsIntact();
    }
}
