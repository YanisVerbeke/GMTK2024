using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _obstaclePrefabs;
    [SerializeField] private GameObject _fruitPrefab;

    private float _spawnTimer;

    private float _fruitTimer;


    private void Update()
    {
        _spawnTimer -= Time.deltaTime;
        _fruitTimer -= Time.deltaTime;

        if (_spawnTimer <= 0)
        {
            SpawnObstacle();
            _spawnTimer = Random.Range(0.6f, 1.5f);
        }

        if (_fruitTimer <= 0)
        {
            SpawnFruit();
            _fruitTimer = Random.Range(2f, 6f);
        }
    }

    private void SpawnObstacle()
    {
        float yPos = Random.Range(-1.2f, 1.6f);

        Instantiate(_obstaclePrefabs[Random.Range(0, _obstaclePrefabs.Count)], new Vector3(transform.position.x, transform.position.y + yPos, transform.position.z), Quaternion.identity);
    }

    private void SpawnFruit()
    {
        float yPos = Random.Range(-4f, 4f);

        Instantiate(_fruitPrefab, new Vector3(transform.position.x, transform.position.y + yPos, transform.position.z), Quaternion.identity);
    }

}
