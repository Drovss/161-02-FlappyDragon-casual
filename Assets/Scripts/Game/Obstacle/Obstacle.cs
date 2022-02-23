using System;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _direction;
    [SerializeField] private Vector3 _positionOnDestroy;

    private Transform _tr;

    public static Action<GameObject> ReturnGOAction;
    public static Action UpScoreAction;

    private void Start()
    {
        _tr = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        Move();

        if (_tr.position.x < _positionOnDestroy.x)
        {
            ReturnGO();
        }
    }

    private void Move()
    {
        _tr.Translate(_direction * _speed * Time.fixedDeltaTime);
    }

    private void ReturnGO()
    {
        if (ReturnGOAction != null)
        {
            ReturnGOAction(gameObject);
        }

        if (UpScoreAction != null)
        {
            UpScoreAction();
        }
    }
}
