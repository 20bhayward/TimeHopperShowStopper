using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AEnemyMovement : MonoBehaviour
{
    [SerializeField] protected Rigidbody rb;

    public abstract void MoveInDir(Vector3 dir);

    public abstract void StopMovement();

    public void MoveTowardPos(Vector3 pos)
    {
        Vector3 dir = (pos - transform.position).normalized;
        MoveInDir(dir);
    }
}
