using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroundMovement :AEnemyMovement
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _runBoolName;
    [SerializeField] private float _groundSpeed;

    public override void MoveInDir(Vector3 dir)
    {
        _animator.SetBool(_runBoolName, true);
        // TODO: Improve this code later
        Vector3 flattenedDir = new Vector3(dir.x, 0, dir.z);
        rb.velocity = flattenedDir.normalized * _groundSpeed;
    }

    public override void StopMovement()
    {
        Debug.Log("## Stopping");
        rb.velocity = Vector3.zero;
        _animator.SetBool(_runBoolName, false);
    }
}
