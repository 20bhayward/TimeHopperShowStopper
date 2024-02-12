using UnityEngine;

public class Moveable : MonoBehaviour
{
    public void ApplyForce(Vector2 force)
    {
        GetComponent<Rigidbody2D>().AddForce(force);
    }
}