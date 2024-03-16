using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    [SerializeField] protected ALevelController pastLevel;
    [SerializeField] protected ALevelController presentLevel;

    [SerializeField] protected List<Transform> moveBetweenLevels;

    protected TimeState timeState;

    private Vector3 _levelDistance;

    private void Start()
    {
        _levelDistance = pastLevel.GetPos() - presentLevel.GetPos();
        timeState = TimeState.Present;
        pastLevel.DisableLevel();
        presentLevel.EnableLevel();
    }

    public void SwitchTimeState()
    {
        switch (timeState)
        {
            case TimeState.Present:
                SwitchToPast();
                return;
            case TimeState.Past:
                SwitchToPresent();
                return;
        }
    }

    public TimeState GetTimeState()
    {
        return timeState;
    }

    private void SwitchToPast()
    {
        foreach (Transform t in moveBetweenLevels)
        {
            t.position += _levelDistance;
        }
        timeState = TimeState.Past;
        presentLevel.DisableLevel();
        pastLevel.EnableLevel();
    }

    private void SwitchToPresent()
    {
        foreach (Transform t in moveBetweenLevels)
        {
            t.position -= _levelDistance;
        }
        timeState = TimeState.Present;
        pastLevel.DisableLevel();
        presentLevel.EnableLevel();
    }
}
