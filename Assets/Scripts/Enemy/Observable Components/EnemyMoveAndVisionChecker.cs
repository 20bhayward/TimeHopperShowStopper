using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveAndVisionChecker : MonoBehaviour
{
    [SerializeField] private Transform _eyeTransform;
    [SerializeField] private float _movementCheckOffset;
    [SerializeField] private LayerMask _blockVisionLayers;
    [SerializeField] private LayerMask _blockMoveLayers;

    public bool CanSeePos(Vector3 pos)
    {
        return !RaycastHits(_eyeTransform.position, pos, _blockVisionLayers);
    }

    public bool CanMoveToPosDirect(Vector3 pos)
    {
        bool canMove = !RaycastHits(transform.position + _movementCheckOffset * Vector3.up, pos + _movementCheckOffset * Vector3.up, _blockMoveLayers);
        //if (!canMove)
        //{
        //    Debug.Log("##Can't reach target");
        //}
        return true;
        //return canMove;
    }

    private bool RaycastHits(Vector3 origin, Vector3 target, LayerMask layerMask)
    {
        Debug.DrawRay(origin, target - origin, Color.green);
        return Physics.Raycast(origin, target - origin, Mathf.Infinity, layerMask);
    }
}
