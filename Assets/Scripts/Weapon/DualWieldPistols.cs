using System.Collections;
using UnityEngine;

public class DualWieldPistols : AWeapon
{
    public LayerMask hitLayers; // Define which layers the bullets can hit, including enemies
    public float range = 100f; // Max range of the hitscan
    public GameObject hitEffectPrefab; // Prefab for the visual effect at the hit location
    public float fireDelay = 0.1f; // Delay in seconds between firing each pistol
    public AudioClip gunshotSound; // Assign in the inspector
    public Material laserMaterial; // Assign in the inspector
    public float laserDuration = 0.1f; // How long the laser is visible

    private AudioSource audioSource;

    // Add to the class fields
    public LayerMask enemyLayers; // Ensure this only includes layers considered as enemies
    public float altFireRadius = 50f; // Radius in screen space pixels
    public bool isAltFireMode = false; // Toggle for alt-fire mode
    public float detectionRadius = 5f; // Radius in world units for detecting enemies around the ray hit point


    void Awake()
    {
        InitWeapon();
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StartAttack();
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            StopAttack();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            isAltFireMode = !isAltFireMode;
        }

        if (isAltFireMode)
        {
            CheckForEnemiesInRange();
        }
    }

    private void CheckForEnemiesInRange()
    {
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range))
        {
            // Perform an overlap sphere at the hit point to detect enemies within the specified radius
            Collider[] hits = Physics.OverlapSphere(hit.point, detectionRadius, enemyLayers);
            int enemyCount = hits.Length;
            if (enemyCount > 0) // If there are enemies, aim at the first detected enemy
            {
                Vector3 targetDirection = (hits[0].transform.position - transform.position).normalized;
                AimAtTarget(targetDirection);
            }
            PerformAltFireHitscan();

            // Output the number of enemies detected
            Debug.Log($"Enemies in range: {enemyCount}");
        }
    }

    private void AimAtTarget(Vector3 targetDirection)
    {
        foreach (var barrel in barrelTransforms)
        {
            barrel.LookAt(barrel.position + targetDirection);
        }
    }

    public override void FireOnce()
    {
        StartCoroutine(FireWithDelay());
    }

    private IEnumerator FireWithDelay()
    {
        PerformHitscan(barrelTransforms[0]);
        yield return new WaitForSeconds(fireDelay);
        PerformHitscan(barrelTransforms[1]);
    }

    private void PerformHitscan(Transform barrel)
    {
        RaycastHit hit;
        if (Physics.Raycast(barrel.position, barrel.forward, out hit, range, hitLayers))
        {
            Vector3 directionToTarget = hit.point - barrel.position;
            barrel.LookAt(hit.point); // Orient the barrel towards the hit point

            if (gunshotSound && audioSource)
            {
                audioSource.PlayOneShot(gunshotSound);
            }

            StartCoroutine(DrawLaser(barrel.position, hit.point));
            Debug.Log("Hit: " + hit.collider.name);
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
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = laserMaterial;
        lineRenderer.SetPositions(new Vector3[] { start, end });

        yield return new WaitForSeconds(laserDuration);

        Destroy(laser);
    }

    private void PerformAltFireHitscan()
    {
        // This method can be updated similarly to include auto-aim functionality if required
    }

    void OnDrawGizmos()
    {
        if (isAltFireMode)
        {
            Vector3 screenCenterWorld = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(screenCenterWorld, altFireRadius / Screen.dpi); // Approximation for visualization
        }
    }

}
