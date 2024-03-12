using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public float health = 100f;
    public float maxHealth = 100f;
    public Image mainHealthBarFill; // Assign in inspector

    private void Die()
    {
        gameObject.SetActive(false);
        Debug.Log("Player died.");
    }
    void Start()
    {
        health = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthBar();

        if (health <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        float healthPercent = health / maxHealth;
        if (mainHealthBarFill != null)
        {
            mainHealthBarFill.fillAmount = healthPercent;
        }

    }
}
