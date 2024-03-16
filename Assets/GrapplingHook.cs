using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    public DualWieldPistols weaponSystem;

    private void OnCollisionEnter(Collision collision)
    {
        if (weaponSystem && weaponSystem.whatIsGrappleable == (weaponSystem.whatIsGrappleable | (1 << collision.gameObject.layer)))
        {
            weaponSystem.SetGrapplePoint(transform.position);
            Destroy(gameObject);  // Or disable it if you want to reuse the hook
        }
    }
}
