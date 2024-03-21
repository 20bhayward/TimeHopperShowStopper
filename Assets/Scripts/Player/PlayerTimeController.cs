using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTimeController : MonoBehaviour
{
    [SerializeField] private WorldController _worldController;
    [SerializeField] private int _maxTimeTravelPeriod;
    [SerializeField] private float _recoveryRate;

    [SerializeField] private int _remainingTime;
    private float _lastTick;

    private void Start()
    {
        _remainingTime = _maxTimeTravelPeriod;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _worldController.SwitchTimeState();
            _lastTick = Time.fixedTime;
        }
    }

    private void FixedUpdate()
    {
        if (_worldController.GetTimeState() == TimeState.Past)
        {
            WhileInPast();
        }
        else
        {
            WhileInPresent();
        }
    }

    private void WhileInPast()
    {
        if (Time.fixedTime >= _lastTick + 1)
        {
            _lastTick = Time.fixedTime;
            _remainingTime -= 1;
        }
        if (_remainingTime < 1)
        {
            _worldController.SwitchTimeState();
        }
    }

    private void WhileInPresent()
    {
        if (Time.fixedTime >= _lastTick + _recoveryRate && _remainingTime < _maxTimeTravelPeriod)
        {
            _lastTick = Time.fixedTime;
            _remainingTime += 1;
        }
    }
}
