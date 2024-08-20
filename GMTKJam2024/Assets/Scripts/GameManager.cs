using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private StateStats _currentStateStats;
    private int _stateIndex = 0;

    [SerializeField] private List<GameObject> _state1Obstacles;
    [SerializeField] private List<GameObject> _state2Obstacles;
    [SerializeField] private List<GameObject> _state3Obstacles;

    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private GameObject _winScreen;

    public StateStats CurrentStateStats { get { return _currentStateStats; } }
    public int CurrentStateIndex { get { return _stateIndex; } }

    public bool IsPlaying { get; private set; }

    private float _winTimer = 6f;

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
        StartGame();

    }

    private void Update()
    {
        if (CurrentStateIndex >= 4)
        {
            _winTimer -= Time.deltaTime;

            if (_winTimer <= 0)
            {
                DisplayWinScreen();
            }
        }
    }


    public void GoToNextState()
    {
        if (_stateIndex >= 4)
            return;

        _stateIndex++;
        switch (_stateIndex)
        {
            case 1:
                _currentStateStats = new StateStats(8, _state1Obstacles, "OnMid", 10, 7);
                break;
            case 2:
                _currentStateStats = new StateStats(18, _state2Obstacles, "OnBig", 30, 10);
                break;
            case 3:
                _currentStateStats = new StateStats(32, _state3Obstacles, "OnGiant", 50, 14);
                break;
            case 4:
                _currentStateStats = new StateStats(1000000, _state3Obstacles, "", 0, 0);
                break;
            default:
                break;
        }
        ObstacleSpawner.Instance.UpdateObstaclesToSpawn();
        BackgroundManager.Instance.ChangeStateBackground();
    }

    public void StartGame()
    {
        _gameOverScreen.SetActive(false);
        _winScreen.SetActive(false);
        _stateIndex = 0;
        GoToNextState();
        IsPlaying = true;
    }

    public void GameOver()
    {
        DisplayGameOverScreen();
        IsPlaying = false;
    }


    public void PlayButtonClick()
    {
        SceneManager.LoadScene("MainGameScene");
        StartGame();
    }

    public void DisplayGameOverScreen()
    {
        _gameOverScreen.SetActive(true);
    }

    public void DisplayWinScreen()
    {
        _winScreen.SetActive(true);
    }
}
