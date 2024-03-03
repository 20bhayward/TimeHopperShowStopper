using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    private float _currHealth;

    private void Start()
    {
        _currHealth = _maxHealth;
    }

    public void AdjustHealth(float value)
    {
        if (_currHealth + value > _maxHealth)
        {
            _currHealth = _maxHealth;
            return;
        }
        if (_currHealth + value < 0)
        {
            _currHealth = 0;
            OnEnemyDie();
            return;
        }
        _currHealth += value;
    }

    private void OnEnemyDie()
    {

    }
}
