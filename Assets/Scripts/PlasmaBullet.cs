using UnityEngine;

public class PlasmaBullet : MonoBehaviour
{
    public float damageAmount = 10f;
    public LayerMask impactLayers;
    public AudioClip impactSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Hit: {other.gameObject.name}");
        if ((impactLayers.value & (1 << other.gameObject.layer)) != 0)
        {
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damageAmount);
                Debug.Log($"Damaged: {other.gameObject.name}");
            }
            else
            {
                Debug.Log($"No IDamageable found on: {other.gameObject.name}");
            }
            Destroy(gameObject);  // Destroy the bullet after impact
        }
    }




    private void PlayImpactSound()
    {
        if (audioSource != null && impactSound != null)
        {
            audioSource.PlayOneShot(impactSound);
            // Optionally, you can destroy the bullet object after the sound finishes playing
            Destroy(gameObject, impactSound.length);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
