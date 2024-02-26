using UnityEngine;

public class Moveable : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;

    public void ApplyForce(Vector2 force)
    {
        _rb.AddForce(force);
    }
}