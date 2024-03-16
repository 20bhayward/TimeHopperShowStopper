using System.Collections;
using UnityEngine;

public class DualWieldPistols : AWeapon
{
    public LayerMask hitLayers;
    public float range = 100f;
    public float damageAmount = 1f;
    public float fireDelay = 0.1f;
    public AudioClip gunshotSound;
    public GameObject hitEffectPrefab;
    public GameObject muzzleFlashPrefab;
    [SerializeField] private Transform[] muzzleFlashPoints; // Points where muzzle flash will appear

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
            // Instantiate the muzzle flash as a child of the muzzleFlashPoint
            var muzzleFlashInstance = Instantiate(muzzleFlashPrefab, muzzleFlashPoint.position, muzzleFlashPoint.rotation, muzzleFlashPoint);
            Destroy(muzzleFlashInstance, 0.3f); // Adjust the duration of the muzzle flash effect as needed
        }
    }
}
