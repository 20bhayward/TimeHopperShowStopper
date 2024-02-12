using System.Collections;
using UnityEngine;

public class DualWieldPistols : AWeapon
{
    public Transform leftBarrel;
    public Transform rightBarrel;
    public LayerMask hitLayers; // Define which layers the bullets can hit
    public float range = 100f; // Max range of the hitscan
    public GameObject hitEffectPrefab; // Prefab for the visual effect at the hit location
    public float fireDelay = 0.1f; // Delay in seconds between firing each pistol

    // Extra Decals
    public AudioClip gunshotSound; // Assign in the inspector
    public Material laserMaterial; // Assign in the inspector
    public float laserDuration = 0.1f; // How long the laser is visible

    private AudioSource audioSource;

    void Awake()
    {
        InitWeapon();
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        // Check for fire button press (e.g., left mouse button)
        if (Input.GetButtonDown("Fire1")) // "Fire1" is typically the left mouse button or Ctrl in Unity's Input Manager
        {
            StartAttack();
        }
        else if (Input.GetButtonUp("Fire1")) // Ensures firing stops when the button is released
        {
            StopAttack();
        }
    }

    public override void FireOnce()
    {
        StartCoroutine(FireWithDelay());
    }

    private IEnumerator FireWithDelay()
    {
        PerformHitscan(leftBarrel);
        yield return new WaitForSeconds(fireDelay); // Wait for the specified delay
        PerformHitscan(rightBarrel);
    }

    private void PerformHitscan(Transform barrel)
    {
        // Play gunshot sound
        if (gunshotSound && audioSource)
        {
            audioSource.PlayOneShot(gunshotSound);
        }

        RaycastHit hit;
        if (Physics.Raycast(barrel.position, barrel.forward, out hit, range, hitLayers))
        {
            // Draw laser
            StartCoroutine(DrawLaser(barrel.position, hit.point));
            Debug.Log("Hit: " + hit.collider.name);
            Debug.DrawLine(barrel.position, hit.point, Color.red, 1.0f);
            if (hitEffectPrefab)
            {
                Instantiate(hitEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
        else
        {
            Debug.Log("No hit detected.");
        }
    }


    private IEnumerator DrawLaser(Vector3 start, Vector3 end)
    {
        GameObject laser = new GameObject("Laser");
        LineRenderer lineRenderer = laser.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;
        lineRenderer.material = laserMaterial;
        lineRenderer.SetPositions(new Vector3[] { start, end });

        yield return new WaitForSeconds(laserDuration);

        Destroy(laser);
    }
}
