using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Controller : EnemyController
{
    [SerializeField] private EnemyMeleeWeapon _meleeWeapon;

    #region MELEE ATTACKS
    public void StartMeleeAttack(float damage)
    {
        _meleeWeapon.StartMeleeAttack(damage);
    }

    public void EndMeleeAttack()
    {
        _meleeWeapon.EndMeleeAttack();
    }
    #endregion
}
