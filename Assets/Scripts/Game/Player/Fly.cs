using UnityEngine;

public class Fly : MonoBehaviour
{
    [SerializeField] private float _impulse;

    private Rigidbody2D _rb;
 
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.Sleep();
    }

    public void FlyUp()
    {
        _rb.velocity = Vector2.up * _impulse;
    }
}
