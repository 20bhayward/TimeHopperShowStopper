using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemyInfoManager : MonoBehaviour
{
    [SerializeField] protected EnemyMoveAndVisionChecker moveAndVisionChecker;

    private string _currentStateName;

    private Dictionary<string, float> _stateCooldowns;
    private Dictionary<string, float> _stateTimeLog;

    private void Start()
    {
        _stateCooldowns = new Dictionary<string, float>();
        _stateTimeLog = new Dictionary<string, float>();
    }

    public void SetStateCooldown(StateNode state, float cooldown)
    {
        string stateName = state.GetType().Name;
        if (_stateCooldowns.ContainsKey(stateName))
        {
            return;
        }
        _stateCooldowns.Add(stateName, cooldown);
    }
    
    public void LogState(StateNode state)
    {
        string stateName = state.GetType().Name;
        if (_stateCooldowns.ContainsKey(stateName))
        {
            if (_stateTimeLog.ContainsKey(stateName))
            {
                _stateTimeLog[stateName] = Time.time;
                return;
            }
            _stateTimeLog.Add(stateName, Time.time);
        }
    }

    public bool CanPerformState(StateNode state)
    {
        string stateName = state.GetType().Name;
        if (_stateCooldowns.ContainsKey(stateName))
        {
            if (_stateTimeLog.ContainsKey(stateName))
            {
                float lastLoggedTime = _stateTimeLog[stateName];
                float cooldown = _stateCooldowns[stateName];
                Debug.Log("%% TIME SINCE LAST: " + (Time.time - lastLoggedTime));
                return Time.time > lastLoggedTime + cooldown;
            }
        }
        return true;
    }

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
