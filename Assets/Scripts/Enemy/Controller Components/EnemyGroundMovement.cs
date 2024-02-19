using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroundMovement :AEnemyMovement
{
    [SerializeField] private float _groundSpeed;

    public override void MoveInDir(Vector3 dir)
    {
        // TODO: Improve this code later
        Vector3 flattenedDir = new Vector3(dir.x, 0, dir.z);
        rb.AddForce(flattenedDir.normalized * _groundSpeed);
    }

    public override void StopMovement()
    {
        throw new System.NotImplementedException();
    }
}
