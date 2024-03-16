using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Controller : EnemyController
{
    [SerializeField] private EnemyMeleeWeapon _meleeWeapon;
    [SerializeField] private FireBoneSpur _boneSpurs;
    [SerializeField] private NegativeWaveComponent _negativeWave;

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

    #region SPECIAL ABILITIES
    public void FireBoneSpurs(Vector3 playerPos)
    {
        _boneSpurs.SpawnAndShootProjectiles(playerPos);
    }

    public void FireNegativeWave(Vector3 playerPos)
    {
        _negativeWave.SpawnProjectile(playerPos);
    }
    #endregion
}
