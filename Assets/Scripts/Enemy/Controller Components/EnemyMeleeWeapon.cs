using UnityEngine;

public class EnemyMeleeWeapon : MonoBehaviour
{
    [SerializeField] private LayerMask _collisionLayers;

    private bool _enabled = false;
    private float _damage;

    private void OnTriggerEnter(Collider other)
    {
        if (_enabled)
        {
            DamageUtil.DamageObject(other.gameObject, _damage);
        }
    }

    public void StartMeleeAttack(float damage)
    {
        _damage = damage;
        _enabled = true;
    }

    public void EndMeleeAttack()
    {
        _damage = 0;
        _enabled = false;
    }
}
