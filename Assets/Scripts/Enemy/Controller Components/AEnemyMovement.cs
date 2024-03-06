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
        RotateTowardPos(pos);
        Vector3 dir = (pos - transform.position).normalized;
        MoveInDir(dir);
    }

    public void RotateTowardPos(Vector3 pos)
    {
        Vector3 dir = (pos - transform.position).normalized;
        Vector3 flattenedDir = new Vector3(dir.x, 0, dir.z);
        rb.rotation = Quaternion.LookRotation(flattenedDir);
    }
}
