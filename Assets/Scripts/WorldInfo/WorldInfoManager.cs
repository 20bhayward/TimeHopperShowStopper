using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldInfoManager : MonoBehaviour
{
    [SerializeField] protected WorldController worldController;

    public TimeState GetTimeState()
    {
        return worldController.GetTimeState();
    }
}
