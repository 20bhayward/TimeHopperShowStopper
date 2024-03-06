using UnityEngine;

public class EnemyInfoManager : MonoBehaviour
{
    [SerializeField] protected EnemyMoveAndVisionChecker moveAndVisionChecker;

    private string _currentStateName;

    public int GetHealth()
    {
        return 0;
    }

    public Vector3 GetPos()
    {
        return transform.position;
    }

    public float GetDistanceTo(Vector3 pos)
    {
        return Vector3.Distance(GetPos(), pos);
    }

    public bool PosReached(Vector3 otherPos)
    {
        return GetDistanceTo(otherPos) < 0.01f;
    }

    public bool CanSeePos(Vector3 pos)
    {
        return moveAndVisionChecker.CanSeePos(pos);
    }

    public bool CanMoveToPosDirect(Vector3 pos)
    {
        return moveAndVisionChecker.CanMoveToPosDirect(pos);
    }

    public void SetCurrentStateName(string name)
    {
        _currentStateName = name;
    }

    public string GetCurrentStateName()
    {
        return _currentStateName;
    }
}
