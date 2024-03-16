using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTimeController : MonoBehaviour
{
    [SerializeField] private WorldController _worldController;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _worldController.SwitchTimeState();
        }
    }
}
