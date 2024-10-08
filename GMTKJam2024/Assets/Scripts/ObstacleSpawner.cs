using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public static ObstacleSpawner Instance { get; private set; }

    [SerializeField] private List<GameObject> _obstaclePrefabs;
    [SerializeField] private List<GameObject> _fruitPrefabs;

    private float _spawnTimer;

    private float _fruitTimer;

    private CameraZoom _cameraZoom;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _cameraZoom = Camera.main.gameObject.GetComponent<CameraZoom>();
    }

    private void Update()
    {
        if (!GameManager.Instance.IsPlaying || GameManager.Instance.CurrentStateIndex >= 4)
            return;

        transform.position = new Vector3(_cameraZoom.Screenbounds.x + 10, transform.position.y, transform.position.z);

        _spawnTimer -= Time.deltaTime;
        _fruitTimer -= Time.deltaTime;

        if (_spawnTimer <= 0)
        {
            SpawnObstacle();
            _spawnTimer = Random.Range(0.6f, 1.6f);
        }

        if (_fruitTimer <= 0)
        {
            SpawnFruit();
            _fruitTimer = Random.Range(2f, 4f);
        }
    }

    private void SpawnObstacle()
    {
        float yPos = Random.Range(-1.2f - Mathf.Pow(GameManager.Instance.CurrentStateIndex - 1, 4), 1.6f + Mathf.Pow(GameManager.Instance.CurrentStateIndex - 1, 4));

        if (_obstaclePrefabs.Count > 0)
        {
            Instantiate(_obstaclePrefabs[Random.Range(0, _obstaclePrefabs.Count)], new Vector3(transform.position.x, transform.position.y + yPos, transform.position.z), Quaternion.identity);
        }
    }

    private void SpawnFruit()
    {
        float yPos = Random.Range(-4f, 4f);

        Instantiate(_fruitPrefabs[Random.Range(0, _fruitPrefabs.Count)], new Vector3(transform.position.x, transform.position.y + yPos, transform.position.z), Quaternion.identity);
    }

    public void UpdateObstaclesToSpawn()
    {
        _obstaclePrefabs = GameManager.Instance.CurrentStateStats.obstaclesToSpawn;
    }

}
