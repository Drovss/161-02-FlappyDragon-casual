using UnityEngine;
using UnityEngine.Events;

public class Collision : MonoBehaviour
{
    [SerializeField] private float _downDestroyPos;

    private Transform _tr;

    public UnityEvent LoseEvent;

    private void Start()
    {
        _tr = GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        if (_tr.position.y < _downDestroyPos)
        {
            Destroy(gameObject);
            LoseEvent.Invoke();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Obstacle>())
        {
            LoseEvent.Invoke();
        }
    }
}
