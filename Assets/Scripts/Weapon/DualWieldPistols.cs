using System.Collections;
using UnityEngine;

public class DualWieldPistols : AWeapon
{
    [Header("Shooting")]
    public LayerMask hitLayers;
    public float range = 100f;
    public float damageAmount = 1f;
    public float fireDelay = 0.1f;
    public AudioClip gunshotSound;
    public GameObject hitEffectPrefab;
    public GameObject muzzleFlashPrefab;
    [SerializeField] private Transform[] muzzleFlashPoints;

    [Header("Grappling")]
    public GameObject grapplingHookPrefab;
    public LayerMask whatIsGrappleable;
    public float maxGrappleDistance = 100f;
    private Vector3 grapplePoint;
    private bool isGrappling;
    private GameObject currentGrappleHook;
    private LineRenderer lr;
    private Transform grappledTarget; // To keep track of the target's transform
    private Vector3 grappleOffset; // The offset from the target's origin to the grapple point
    public float grappleSpeed = 25f;  // Speed of the grappling hook projectile


    private AudioSource audioSource;

    void Awake()
    {
        InitWeapon();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !isGrappling)
        {
            StartAttack();
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            StopAttack();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            StartGrapple();
        }

        if (isGrappling)
        {
            UpdateGrappleLine();
        }
    }

    public override void FireOnce()
    {
        StartCoroutine(FireWithDelay());
    }

    private IEnumerator FireWithDelay()
    {
        if (barrelTransforms.Length >= 2 && muzzleFlashPoints.Length >= 2)
        {
            PerformHitscan(barrelTransforms[0]);
            ShowMuzzleFlash(muzzleFlashPoints[0]);
            yield return new WaitForSeconds(fireDelay);
            PerformHitscan(barrelTransforms[1]);
            ShowMuzzleFlash(muzzleFlashPoints[1]);
        }
    }

    private void PerformHitscan(Transform shootingPoint)
    {
        RaycastHit hit;
        if (Physics.Raycast(shootingPoint.position, shootingPoint.forward, out hit, range, hitLayers))
        {
            if (hitEffectPrefab)
            {
                Instantiate(hitEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));
            }

            DamageUtil.DamageObject(hit.collider.gameObject, damageAmount);
        }
    }

    private void ShowMuzzleFlash(Transform muzzleFlashPoint)
    {
        if (gunshotSound && audioSource)
        {
            audioSource.PlayOneShot(gunshotSound);
        }
        if (muzzleFlashPrefab)
        {
            var muzzleFlashInstance = Instantiate(muzzleFlashPrefab, muzzleFlashPoint.position, muzzleFlashPoint.rotation, muzzleFlashPoint);
            Destroy(muzzleFlashInstance, 0.3f); // Adjust as needed
        }
    }

    private void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(barrelTransforms[1].position, barrelTransforms[1].forward, out hit, maxGrappleDistance, whatIsGrappleable))
        {
            isGrappling = true;
            grappledTarget = hit.transform;
            grappleOffset = hit.point - grappledTarget.position;
            currentGrappleHook = Instantiate(grapplingHookPrefab, barrelTransforms[1].position, Quaternion.identity);

            Rigidbody grappleRb = currentGrappleHook.GetComponent<Rigidbody>();
            if (grappleRb != null)
            {
                Vector3 direction = (hit.point - barrelTransforms[1].position).normalized;
                grappleRb.velocity = direction * grappleSpeed;
            }
        }
    }

    public void SetGrapplePoint(Vector3 point)
    {
        grapplePoint = point;
        if (lr != null)
        {
            lr.enabled = true;
            lr.SetPosition(0, barrelTransforms[1].position);
            lr.SetPosition(1, grapplePoint);
        }

        // Handle the physics of tethering here, e.g., applying a force towards the grapple point
    }



    private IEnumerator WaitForGrappleToReachTarget(Vector3 targetPoint)
    {
        // Wait until the grappling hook reaches the target
        while (currentGrappleHook != null && Vector3.Distance(currentGrappleHook.transform.position, targetPoint) > 0.5f)
        {
            yield return null;
        }

        if (currentGrappleHook != null)
        {
            grapplePoint = targetPoint;
            // Here you can also adjust the grapple hook's position to exactly match the target point

            // Initialize the LineRenderer or grappling visual
            if (lr != null)
            {
                lr.enabled = true;
                lr.SetPosition(0, barrelTransforms[1].position);
                lr.SetPosition(1, grapplePoint);
            }
        }
    }

    private void UpdateGrappleLine()
    {
        if (isGrappling && lr != null && grappledTarget != null)
        {
            // Update the grapple point based on the target's current position and the initial offset
            grapplePoint = grappledTarget.position + grappleOffset;
            lr.SetPosition(0, barrelTransforms[1].position);
            lr.SetPosition(1, grapplePoint);
        }
    }

    private void StopGrapple()
    {
        if (isGrappling)
        {
            isGrappling = false;
            lr.enabled = false;
            Destroy(currentGrappleHook);
            // Reset player movement and any grapple-related states
        }
    }
}
