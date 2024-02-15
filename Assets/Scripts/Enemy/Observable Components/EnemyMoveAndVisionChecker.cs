using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveAndVisionChecker : MonoBehaviour
{
    [SerializeField] private Transform _eyeTransform;
    [SerializeField] private LayerMask _blockVisionLayers;
    [SerializeField] private LayerMask _blockMoveLayers;

    public bool CanSeePos(Vector3 pos)
    {
        return !RaycastHits(_eyeTransform.position, pos, _blockVisionLayers);
    }

    public bool CanMoveToPosDirect(Vector3 pos)
    {
        return !RaycastHits(transform.position, pos, _blockMoveLayers);
    }

    private bool RaycastHits(Vector3 origin, Vector3 target, LayerMask layerMask)
    {
        return Physics.Raycast(origin, target - origin, Mathf.Infinity, layerMask);
    }
}
