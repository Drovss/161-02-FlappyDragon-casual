using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private int _poolCount;
    [SerializeField] private float _minSpawnTime = 1;
    [SerializeField] private float _maxSpawnTime = 2;
    [Space]
    [SerializeField] private Vector2 _upSpawnPos;
    [SerializeField] private Vector2 _downSpawnPos;
    [SerializeField] private float _radiusSpawn;

    private bool _isSpawn = true;

    private Queue<GameObject> _currentEnemy = new Queue<GameObject>();

    private void OnEnable()
    {
        Obstacle.ReturnGOAction += ReturnGO;
    }

    private void OnDisable()
    {
        Obstacle.ReturnGOAction -= ReturnGO;
    }

    private void Start()
    {

        for (int i = 0; i < _poolCount; i++)
        {
            CreateNewGO();
        }

        StartCoroutine(Spawn());
    }

    public void StopSpawn()
    {
        _isSpawn = false;
    }

    private IEnumerator Spawn()
    {
        if (_minSpawnTime == 0)
        {
            _minSpawnTime = 1;
        }

        while (true)
        {
            yield return new WaitForSeconds(Random.Range(_minSpawnTime, _maxSpawnTime));
            if (_currentEnemy.Count > 1)
            {
                ActiveGO();
            }
            else
            {
                _poolCount+=2;
                CreateNewGO();
                CreateNewGO();
                ActiveGO();
            }

            if (_isSpawn == false)
            {
                break;
            }
        }
    }

    private void ActiveGO()
    {
        ActiveUpGO();
        ActiveDownGO();       
    }

    private void ActiveUpGO()
    {
        Vector2 position = new Vector2(_upSpawnPos.x, Random.Range(_upSpawnPos.y - _radiusSpawn, _upSpawnPos.y + _radiusSpawn));
        var enemy = _currentEnemy.Dequeue();
        enemy.transform.position = position;
        enemy.GetComponent<SpriteRenderer>().flipY = true;
        enemy.SetActive(true);
    }

    private void ActiveDownGO()
    {
        Vector2 position = new Vector2(_downSpawnPos.x, Random.Range(_downSpawnPos.y - _radiusSpawn, _downSpawnPos.y + _radiusSpawn));
        var enemy = _currentEnemy.Dequeue();
        enemy.transform.position = position;
        enemy.GetComponent<SpriteRenderer>().flipY = false;
        enemy.SetActive(true);
    }

    private void CreateNewGO()
    {
        var arrow = Instantiate(_enemyPrefab);
        arrow.SetActive(false);
        _currentEnemy.Enqueue(arrow);
    }

    private void ReturnGO(GameObject arrow)
    {
        arrow.SetActive(false);
        _currentEnemy.Enqueue(arrow);
    }
}
