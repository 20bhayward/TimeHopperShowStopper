using UnityEngine;

public class DamageablePiece : MonoBehaviour, IDamageable
{
    [SerializeField] private CompoundDamageable _compoundDamageable;
    public void TakeDamage(float damage)
    {
        _compoundDamageable.TakeDamage(damage);
    }
}
