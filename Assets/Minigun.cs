using UnityEngine;

public class Minigun : MonoBehaviour, IWeapon
{
    [Header("Laser Settings")]
    public LayerMask hitLayers;
    public Transform barrelEnd;
    public Material laserMaterial1;
    public Material laserMaterial2;
    public float maxSpreadAngle = 10f;
    public float timeToFocus = 3f;
    public float laserRange = 100f;

    [Header("Overheat Settings")]
    public float overheatTime = 5f;
    public float cooldownRate = 1f;

    [Header("Sound Settings")]
    public AudioClip firingSound;
    public AudioClip whirringSound;

    private AudioSource audioSource;
    private AudioSource whirringAudioSource;
    private float fireTimer = 0f;
    private LineRenderer laserLineRenderer;
    private bool isOverheated = false;
    private float currentHeat = 0f;
    private bool isUsingMaterial1 = true;

    void Start()
    {
        // Laser LineRenderer setup
        laserLineRenderer = gameObject.AddComponent<LineRenderer>();
        laserLineRenderer.startWidth = 0.5f;
        laserLineRenderer.endWidth = 0.5f;
        laserLineRenderer.material = laserMaterial1;
        laserLineRenderer.enabled = false;

        // AudioSource setup for firing sound
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = firingSound;

        // Separate AudioSource for continuous whirring sound
        whirringAudioSource = gameObject.AddComponent<AudioSource>();
        whirringAudioSource.playOnAwake = false;
        whirringAudioSource.loop = true;
        whirringAudioSource.clip = whirringSound;
    }

    public void InitWeapon()
    {

    }

    void Update()
    {
        HandleCooling();

        if (Input.GetButton("Fire1") && !isOverheated)
        {
            if (!whirringAudioSource.isPlaying)
                whirringAudioSource.Play();

            fireTimer += Time.deltaTime;
            IncreaseHeat(Time.deltaTime);
            FireWeapon();
        }
        else
        {
            StopAttack();
            if (whirringAudioSource.isPlaying)
                whirringAudioSource.Stop();
        }
    }

    void IncreaseHeat(float amount)
    {
        currentHeat += amount;
        if (currentHeat >= overheatTime)
        {
            isOverheated = true;
            laserLineRenderer.enabled = false; // Optionally disable laser on overheat
            // Add overheat feedback (e.g., visual, audio) here
        }
    }

    void HandleCooling()
    {
        if (!Input.GetButton("Fire1") && currentHeat > 0)
        {
            currentHeat -= cooldownRate * Time.deltaTime;
            if (currentHeat <= 0)
            {
                currentHeat = 0;
                isOverheated = false;
                // Add cooldown feedback (e.g., visual, audio) here
            }
        }
    }

    public void StartAttack()
    {
        if (isOverheated) return;

        fireTimer = 0f;
        laserLineRenderer.enabled = true;
    }

    void FireWeapon()
    {
        laserLineRenderer.enabled = true;

        if (!audioSource.isPlaying)
            audioSource.Play(); // Play firing sound

        // Alternate laser material with each shot
        laserLineRenderer.material = isUsingMaterial1 ? laserMaterial1 : laserMaterial2;
        isUsingMaterial1 = !isUsingMaterial1;

        if (fireTimer < timeToFocus)
        {
            // Calculate the dynamic spread based on fire timer
            float currentSpread = Mathf.Lerp(maxSpreadAngle, 0f, fireTimer / timeToFocus);

            // Generate a random direction within the spread cone
            Vector3 spreadDirection = Quaternion.Euler(Random.Range(-currentSpread, currentSpread), Random.Range(-currentSpread, currentSpread), 0) * barrelEnd.forward;

            FireBullet(spreadDirection);
        }
        else
        {
            // Once fully focused, fire a straight laser shot
            FireBullet(barrelEnd.forward);
        }
    }

    void FireBullet(Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(barrelEnd.position, direction, out hit, laserRange, hitLayers))
        {
            // Position the laser to hit the target
            laserLineRenderer.SetPositions(new Vector3[] { barrelEnd.position, hit.point });
        }
        else
        {
            // Extend the laser to its maximum range if it doesn't hit anything
            laserLineRenderer.SetPositions(new Vector3[] { barrelEnd.position, barrelEnd.position + direction * laserRange });
        }
    }

    public void StopAttack()
    {
        if (laserLineRenderer.enabled)
            audioSource.Stop(); // Stop firing sound when not firing

        laserLineRenderer.enabled = false;
        fireTimer = 0f;
    }

    // IWeapon interface stubs
    public void UpdateWeapon() { }
    public void SetBarrelTransform(Transform[] barrelTransform) { }
    public Transform[] GetBarrels() { return new Transform[] { barrelEnd }; }
    public void SetActive(bool isActive) { gameObject.SetActive(isActive); }
    
}
