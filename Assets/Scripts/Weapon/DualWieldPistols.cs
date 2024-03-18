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
    private bool grappleShotInProgress = false;  // Flag to indicate a grapple shot is in progress
    [Header("Grappling Physics")]
    public Rigidbody playerRb;
    public float swingForceMultiplier = 10f;
    public float pullForceMultiplier = 0.1f;
    public float grapplePullStrength = 10f;
    public float grappleDuration = 5f;  // Time after which the grapple automatically disengages
    public float minGrappleReachDistance = 1f;


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
        if (lr != null)
        {
            lr.enabled = false;  // Ensure the line renderer is initially disabled
        }
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
            lr.enabled = false;
            StartGrapple();
        }


        if (isGrappling)
        {
            UpdateGrappleLine();
            ApplyGrapplePhysics();  
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
    private IEnumerator MoveGrappleHookTowardsTarget(Vector3 targetPoint)
    {
        if (!currentGrappleHook) yield break;

        float startTime = Time.time;
        Vector3 startPosition = currentGrappleHook.transform.position;
        float journeyLength = Vector3.Distance(startPosition, targetPoint);
        float fracComplete = 0;

        while (fracComplete < 1 && currentGrappleHook)
        {
            float distCovered = (Time.time - startTime) * grappleSpeed;
            fracComplete = distCovered / journeyLength;
            currentGrappleHook.transform.position = Vector3.Lerp(startPosition, targetPoint, fracComplete);
            yield return null;
        }

        if (currentGrappleHook)
        {
            SetGrapplePoint(targetPoint);
            Destroy(currentGrappleHook); // Destroy the hook after it reaches the target
        }
    }

    private void StopGrapple()
    {
        if (isGrappling)
        {
            isGrappling = false;
            grappleShotInProgress = false;
            if (lr != null)
            {
                lr.enabled = false;
            }
            if (currentGrappleHook)
            {
                Destroy(currentGrappleHook);
            }

            // Reset the player's velocity to stop movement immediately when grappling ends
            Rigidbody playerRb = GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                playerRb.velocity = Vector3.zero;
                playerRb.angularVelocity = Vector3.zero; // Reset angular velocity if there's any rotation applied
            }
        }
    }


    private void StartGrapple()
    {
        if (grappleShotInProgress) return;

        RaycastHit hit;
        if (Physics.Raycast(barrelTransforms[1].position, barrelTransforms[1].forward, out hit, maxGrappleDistance, whatIsGrappleable))
        {
            grappleShotInProgress = true;
            isGrappling = true;
            grappledTarget = hit.transform;
            grapplePoint = hit.point;
            currentGrappleHook = Instantiate(grapplingHookPrefab, barrelTransforms[1].position, Quaternion.LookRotation(hit.point - barrelTransforms[1].position));

            StartCoroutine(MoveGrappleHookTowardsTarget(hit.point));
            StartCoroutine(GrappleDurationTimer(grappleDuration));
        }
    }

    private IEnumerator GrappleDurationTimer(float duration)
    {
        yield return new WaitForSeconds(duration);
        StopGrapple();
    }

    public void SetGrapplePoint(Vector3 point)
    {
        grapplePoint = point;
        isGrappling = true;

        if (lr != null)
        {
            lr.enabled = true;
            lr.SetPosition(0, barrelTransforms[1].position);
            lr.SetPosition(1, grapplePoint);
        }

        // Start applying physics forces
        ApplyGrapplePhysics();
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
    private void ApplyGrapplePhysics()
    {
        if (!isGrappling || grappledTarget == null || playerRb == null) return;

        Vector3 directionToGrapplePoint = grapplePoint - playerRb.transform.position;
        float distanceToGrapplePoint = directionToGrapplePoint.magnitude;

        if (distanceToGrapplePoint < minGrappleReachDistance)
        {
            StopGrapple();
            return;
        }

        // Get player input for swinging
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate swing direction based on horizontal input only, to keep swinging in the horizontal plane
        Vector3 swingDirection = transform.right * horizontalInput;

        // Apply swing force based on player input, ensuring it's perpendicular to the direction to the grapple point
        if (swingDirection.magnitude > 0.1f)
        {
            Vector3 perpendicularSwingDirection = Vector3.Cross(swingDirection, directionToGrapplePoint).normalized;
            Vector3 swingForce = perpendicularSwingDirection * swingForceMultiplier;
            playerRb.AddForce(swingForce);
        }

        // Apply a moderated pull force towards the grapple point, preventing excessive vertical movement
        Vector3 pullForce = directionToGrapplePoint.normalized * grapplePullStrength * pullForceMultiplier;
        if (verticalInput >= 0)  // Optionally, use vertical input to control the intensity of the pull
        {
            playerRb.AddForce(pullForce * verticalInput);
        }
    }







}
