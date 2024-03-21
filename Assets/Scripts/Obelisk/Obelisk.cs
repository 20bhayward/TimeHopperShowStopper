using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obelisk : MonoBehaviour
{
    [SerializeField] private bool _intact;

    private void Start()
    {
        _intact = true;
    }

    public bool IsIntact()
    {
        return _intact;
    }

    public void DestroyObelisk()
    {
        _intact = false;
    }
}
