using UnityEngine;

public class PlasmaShotgun : AWeapon
{
    [Header("Shotgun Settings")]
    public int pelletsCount = 10;
    public float spreadAngle = 30f;
    public float projectileSpeed = 10f;
    public float range = 10f;
    public float bulletLifetime = 5f; // Bullets are destroyed after this time
    public GameObject projectilePrefab;
    public float damageAmount = 10f;

    [Header("Sound Effects")]
    public AudioClip shootingSound;
    public AudioClip impactSound;
    public AudioClip chargedShotReadySound;
    private AudioSource audioSource;

    [Header("Charging Mechanic")]
    public float chargeTime = 2f;
    private bool isCharging = false;
    private float chargeTimer = 0f;
    private float nextFireTime = 0f;

    void Awake()
    {
        base.InitWeapon(); // Ensure the base class's Awake method is called if it has important logic
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            FireOnce();
            nextFireTime = Time.time + 1f / fireRate;
        }
        if (Input.GetButtonDown("Fire2"))
        {
            isCharging = true;
            // Reset the charge timer if we start charging
            chargeTimer = 0f;
        }
        if (isCharging)
        {
            chargeTimer += Time.deltaTime;
            if (chargeTimer >= chargeTime)
            {
                // Maybe add visual or audio feedback for fully charged
            }
        }
    }

    public override void FireOnce()
    {
        if (isCharging && chargeTimer >= chargeTime)
        {
            FireChargedShot();
            isCharging = false;
            chargeTimer = 0f;
            PlaySound(chargedShotReadySound);
        }
        else if (!isCharging)
        {
            FireShotgunBlast();
        }
    }

    private void FireShotgunBlast()
    {
        PlaySound(shootingSound);
        for (int i = 0; i < pelletsCount; i++)
        {
            Vector3 direction = GetSpreadDirection(barrelTransforms[0].forward, spreadAngle);
            FireProjectile(barrelTransforms[0].position, direction);
        }
    }

    private void FireChargedShot()
    {
        Vector3 direction = barrelTransforms[0].forward; // Straight direction for charged shot
        FireProjectile(barrelTransforms[0].position, direction);
    }

    private void FireProjectile(Vector3 position, Vector3 direction)
    {
        GameObject projectile = Instantiate(projectilePrefab, position, Quaternion.LookRotation(direction));
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = direction * projectileSpeed;
        }
        Destroy(projectile, bulletLifetime);
    }

    private Vector3 GetSpreadDirection(Vector3 forwardDirection, float spreadAngle)
    {
        return Quaternion.Euler(Random.Range(-spreadAngle, spreadAngle), Random.Range(-spreadAngle, spreadAngle), 0) * forwardDirection;
    }


    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
