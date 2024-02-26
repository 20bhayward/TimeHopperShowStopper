using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    public void InitWeapon();

    public void UpdateWeapon();

    public void StartAttack();

    public void StopAttack();

    public void SetBarrelTransform(Transform[] barrelTransform);

    public Transform[] GetBarrels();
    void SetActive(bool v);
}