using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ALevelController : MonoBehaviour
{
    public abstract void EnableLevel();

    public abstract void DisableLevel();

    public Vector3 GetPos()
    {
        return transform.position;
    }
}
