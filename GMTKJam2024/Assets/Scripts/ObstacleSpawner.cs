using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _obstaclePrefabs;

    private float _spawnTimer;


    private void Update()
    {
        _spawnTimer -= Time.deltaTime;

        if (_spawnTimer <= 0)
        {
            SpawnObstacle();
            _spawnTimer = Random.Range(0.6f, 1.5f);
        }
    }

    private void SpawnObstacle()
    {
        float yPos = Random.Range(-1.6f, 1.6f);

        Instantiate(_obstaclePrefabs[Random.Range(0, _obstaclePrefabs.Count)], new Vector3(transform.position.x, transform.position.y + yPos, transform.position.z), Quaternion.identity);
    }

}
