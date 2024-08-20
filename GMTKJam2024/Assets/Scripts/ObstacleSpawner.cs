using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public static ObstacleSpawner Instance { get; private set; }

    [SerializeField] private List<GameObject> _obstaclePrefabs;
    [SerializeField] private GameObject _fruitPrefab;

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
        if (!GameManager.Instance.IsPlaying)
            return;

        transform.position = new Vector3(_cameraZoom.Screenbounds.x + 10, transform.position.y, transform.position.z);

        _spawnTimer -= Time.deltaTime;
        _fruitTimer -= Time.deltaTime;

        if (_spawnTimer <= 0)
        {
            SpawnObstacle();
            _spawnTimer = Random.Range(0.8f, 2f);
        }

        if (_fruitTimer <= 0)
        {
            SpawnFruit();
            _fruitTimer = Random.Range(2f, 4f);
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

    public void UpdateObstaclesToSpawn()
    {
        _obstaclePrefabs = GameManager.Instance.CurrentStateStats.obstaclesToSpawn;
    }

}
