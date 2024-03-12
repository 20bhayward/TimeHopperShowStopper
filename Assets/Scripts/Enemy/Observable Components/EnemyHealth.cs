using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float _maxHealth;
    public float _currHealth;
    public Image mainHealthBarFill;


    private void Start()
    {
        _currHealth = _maxHealth;
        UpdateHealthBar();
    }

    public void AdjustHealth(float value)
    {
      
        if (_currHealth - value > _maxHealth)
        {
            _currHealth = _maxHealth;
            UpdateHealthBar();
            return;
        }
        if (_currHealth - value < 0)
        {
            _currHealth = 0;
            OnEnemyDie();
            return;
        }
        _currHealth -= value;
        UpdateHealthBar();
    }

    private void OnEnemyDie()
    {

    }

    void UpdateHealthBar()
    {
        float healthPercent = _currHealth / _maxHealth;
        if (mainHealthBarFill != null)
        {
            mainHealthBarFill.fillAmount = healthPercent;
        }

    }


}
