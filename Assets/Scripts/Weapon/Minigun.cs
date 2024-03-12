using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Minigun : MonoBehaviour, IWeapon, IWeaponAbility
{

    [Header("Gun Settings")]
    public LayerMask hitLayers;
    public Transform barrelEnd;
    public Material laserMaterial1;
    public Material laserMaterial2;
    public float damageAmount = 1.0f;
    public float maxSpreadAngle = 10f;
    public float timeToFocus = 3f;
    public float laserRange = 100f;

    [Header("Overheat Settings")]
    public float maxHeat = 100f;
    public float heatPerSecond = 20f;
    public float cooldownRate = 10f;
    public Renderer minigunRenderer;
    public int overheatMaterialIndex = 0;
    public Color coolColor = Color.blue;
    public Color overheatedColor = Color.red;

    [Header("Sound Settings")]
    public AudioClip firingSound;
    public AudioClip whirringSound;
    public AudioClip overheatSound; // Overheat sound clip

    private AudioSource audioSource;
    private AudioSource whirringAudioSource;
    private bool hasPlayedOverheatSound = false; // Flag to control overheat sound playback
    private float fireTimer = 0f;
    private LineRenderer laserLineRenderer;
    private bool isOverheated = false;
    private float currentHeat = 0f;
    private bool isUsingMaterial1 = true;

    [Header("Animation Settings")]
    public Animator barrelAnimator;
    public Animator gunAnimator;

    [Header("Ability Settings")]
    public float baseDamage = 10f;
    public float maxDamage = 30f;
    public float baseRange = 2f;
    public float maxRange = 5f;
    public Collider regularAttackCollider;
    public Collider chargedAttackCollider;
    public AudioClip chargedWeaponSound;
    public AudioClip attackSound;
    public AudioClip chargedAttackSound;
    public GameObject chargedAttackVFX;

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

        UpdateEmission(0f); // Initialize emission to zero
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

        if (!Input.GetButton("Fire1") && currentHeat > 0)
        {
            DecreaseHeat(cooldownRate * Time.deltaTime);
        }

        UpdateEmission(currentHeat / maxHeat);
        UpdateBarrelSpinSpeed(); // Update barrel spin speed based on current heat
    }

    void IncreaseHeat(float amount)
    {
        currentHeat += amount;
        currentHeat = Mathf.Min(currentHeat, maxHeat);

        if (currentHeat >= maxHeat && !hasPlayedOverheatSound)
        {
            audioSource.PlayOneShot(overheatSound);
            hasPlayedOverheatSound = true;
        }
        else if (currentHeat < maxHeat)
        {
            hasPlayedOverheatSound = false;
        }

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
            //Debug.Log("Hit: " + hit.collider.name);
            DamageUtil.DamageObject(hit.collider.gameObject, damageAmount); 
        }
        else
        {
            laserLineRenderer.SetPositions(new Vector3[] { barrelEnd.position, barrelEnd.position + direction * laserRange });
        }
    }


    void UpdateEmission(float heatFraction)
    {
        // Dynamically adjust the emission based on the current heat
        Material material = minigunRenderer.materials[overheatMaterialIndex];
        Color baseColor = Color.Lerp(coolColor, overheatedColor, heatFraction); // Linearly interpolate between cool and overheated colors based on heatFraction
        float emissionIntensity = 1 - heatFraction; // Adjust emission intensity to decrease as the weapon heats up

        // Set the emission color, adjusting for gamma space as Unity's renderer expects gamma space values for emission
        material.SetColor("_EmissionColor", baseColor * Mathf.LinearToGammaSpace(emissionIntensity));
        minigunRenderer.materials[overheatMaterialIndex] = material; // Update the material
    }


    void UpdateBarrelSpinSpeed()
    {
        float normalizedHeat = currentHeat / maxHeat;
        float minSpeed = 0.1f; // Minimum spin speed when the gun is cool.
        float maxSpeed = 2.0f; // Maximum spin speed when the gun is overheated.
        float spinSpeed = Mathf.Lerp(minSpeed, maxSpeed, normalizedHeat);
        barrelAnimator.speed = spinSpeed;
    }

    public void ActivateAbility()
    {
        bool isChargedAttack = currentHeat >= maxHeat;
        float damage = Mathf.Lerp(baseDamage, maxDamage, currentHeat / maxHeat);
        Collider activeCollider = isChargedAttack ? chargedAttackCollider : regularAttackCollider;

        // Play appropriate sound
        AudioClip soundToPlay = isChargedAttack ? chargedAttackSound : attackSound;
        audioSource.PlayOneShot(soundToPlay);

        // Show VFX for charged attack
        if (isChargedAttack && chargedAttackVFX != null)
        {
            chargedAttackVFX.SetActive(true);
            // You might want to deactivate the VFX after some time
            Invoke(nameof(HideChargedAttackVFX), 2f); // Adjust time as needed
        }

        StartCoroutine(ActivateCollider(activeCollider, damage, isChargedAttack));
    }

    IEnumerator ActivateCollider(Collider collider, float damage, bool isChargedAttack)
    {
        collider.enabled = true;

        // Wait for the next frame to ensure the collider only hits once
        yield return null;

        // You can add logic here to apply damage using DamageUtils
        // Ensure DamageUtils and the method you're using support specifying the damage amount and collider

        collider.enabled = false;
    }

    void HideChargedAttackVFX()
    {
        if (chargedAttackVFX != null)
            chargedAttackVFX.SetActive(false);
    }



    // Implementation of IWeapon interface methods
    public void InitWeapon() { }
    public void UpdateWeapon() { }
    public void SetBarrelTransform(Transform[] barrelTransform) { }
    public Transform[] GetBarrels() { return new Transform[] { barrelEnd }; }
    public void SetActive(bool isActive) { gameObject.SetActive(isActive); }
}
