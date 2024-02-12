using UnityEngine;

public class EnemyInfoManager : MonoBehaviour
{
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
}
