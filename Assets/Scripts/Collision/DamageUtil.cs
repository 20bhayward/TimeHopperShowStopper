using UnityEngine;

public static class DamageUtil
{
    public static void DamageObject(GameObject obj, float damage, float knockback, Vector3 knockbackDir)
    {
        DamageObject(obj, damage);
        KnockbackObject(obj, knockback, knockbackDir);
    }

    public static void DamageObject(GameObject obj, float damage)
    {
        IDamageable damageable = obj.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }
    }

    public static void KnockbackObject(GameObject obj, float knockback, Vector3 knockbackDir)
    {
        Moveable moveable = obj.GetComponent<Moveable>();

        if (moveable != null)
        {
            moveable.ApplyForce(knockbackDir.normalized * knockback);
        }
    }
}
