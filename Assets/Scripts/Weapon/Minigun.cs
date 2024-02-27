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
    public float maxHeat = 100f; // The maximum heat before overheating
    public float heatPerSecond = 20f; // How much heat is generated per second of firing
    public float cooldownRate = 10f; // How fast the gun cools down per second when not firing
    public Renderer minigunRenderer;
    public int overheatMaterialIndex = 0; // Index of the material to change on overheat
    public Color coolColor = Color.blue;
    public Color overheatedColor = Color.red;

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

    [Header("Animation Settings")]
    public Animator barrelAnimator;

    void Start()
    {
        laserLineRenderer = gameObject.AddComponent<LineRenderer>();
        laserLineRenderer.startWidth = 0.5f;
        laserLineRenderer.endWidth = 0.5f;
        laserLineRenderer.material = laserMaterial1;
        laserLineRenderer.enabled = false;

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = firingSound;

        whirringAudioSource = gameObject.AddComponent<AudioSource>();
        whirringAudioSource.playOnAwake = false;
        whirringAudioSource.loop = true;
        whirringAudioSource.clip = whirringSound;

        if (barrelAnimator == null)
        {
            barrelAnimator = GetComponent<Animator>();
        }

        UpdateMaterial(0); // Set initial material state to cool
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && !isOverheated)
        {
            StartAttack();
        }
        else
        {
            StopAttack();
        }

        // Cooling down logic
        if (!Input.GetButton("Fire1") && currentHeat > 0)
        {
            DecreaseHeat(cooldownRate * Time.deltaTime);
        }

        // Update the color based on current heat
        UpdateMaterial(currentHeat / maxHeat);
    }

    void IncreaseHeat(float amount)
    {
        currentHeat += amount;
        currentHeat = Mathf.Min(currentHeat, maxHeat);
        isOverheated = currentHeat >= maxHeat;
    }

    void DecreaseHeat(float amount)
    {
        currentHeat -= amount;
        currentHeat = Mathf.Max(currentHeat, 0);
        isOverheated = currentHeat >= maxHeat;
    }

    void UpdateMaterial(float heatFraction)
    {
        Color newColor = Color.Lerp(coolColor, overheatedColor, heatFraction);
        Debug.Log($"Updating color to: {newColor}");
        Material[] materials = minigunRenderer.materials;
        materials[overheatMaterialIndex].color = newColor;
        minigunRenderer.materials = materials;
    }


    public void StartAttack()
    {
        if (isOverheated) return;

        barrelAnimator.SetBool("isFiring", true);

        fireTimer += Time.deltaTime;
        IncreaseHeat(heatPerSecond * Time.deltaTime);
        FireWeapon();

        if (!whirringAudioSource.isPlaying)
            whirringAudioSource.Play();
    }

    void FireWeapon()
    {
        laserLineRenderer.enabled = true;

        if (!audioSource.isPlaying)
            audioSource.Play();

        laserLineRenderer.material = isUsingMaterial1 ? laserMaterial1 : laserMaterial2;
        isUsingMaterial1 = !isUsingMaterial1;

        Vector3 direction;
        if (fireTimer < timeToFocus)
        {
            float currentSpread = Mathf.Lerp(maxSpreadAngle, 0f, fireTimer / timeToFocus);
            direction = Quaternion.Euler(Random.Range(-currentSpread, currentSpread), Random.Range(-currentSpread, currentSpread), 0) * barrelEnd.forward;
        }
        else
        {
            direction = barrelEnd.forward;
        }

        FireBullet(direction);
    }

    public void StopAttack()
    {
        if (laserLineRenderer.enabled)
            audioSource.Stop();

        laserLineRenderer.enabled = false;
        fireTimer = 0f;
        barrelAnimator.SetBool("isFiring", false);

        if (whirringAudioSource.isPlaying)
            whirringAudioSource.Stop();
    }

    void FireBullet(Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(barrelEnd.position, direction, out hit, laserRange, hitLayers))
        {
            laserLineRenderer.SetPositions(new Vector3[] { barrelEnd.position, hit.point });
        }
        else
        {
            laserLineRenderer.SetPositions(new Vector3[] { barrelEnd.position, barrelEnd.position + direction * laserRange });
        }
    }

    // Implementation of IWeapon interface methods
    public void InitWeapon() { }
    public void UpdateWeapon() { }
    public void SetBarrelTransform(Transform[] barrelTransform) { }
    public Transform[] GetBarrels() { return new Transform[] { barrelEnd }; }
    public void SetActive(bool isActive) { gameObject.SetActive(isActive); }
}
