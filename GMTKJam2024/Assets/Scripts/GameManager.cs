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

    public StateStats CurrentStateStats { get { return _currentStateStats; } }
    public int CurrentStateIndex { get { return _stateIndex; } }

    public bool IsPlaying { get; private set; }

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
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        StartGame();
        GoToNextState();
    }


    public void GoToNextState()
    {
        _stateIndex++;
        switch (_stateIndex)
        {
            case 1:
                _currentStateStats = new StateStats(7, _state1Obstacles, "OnMid", 10, 7);
                break;
            case 2:
                _currentStateStats = new StateStats(20, _state2Obstacles, "OnBig", 30, 15);
                break;
            case 3:
                _currentStateStats = new StateStats(30, _state3Obstacles, "OnGiant", 50, 23);
                break;
            default:
                break;
        }
        ObstacleSpawner.Instance.UpdateObstaclesToSpawn();
    }

    public void StartGame()
    {
        IsPlaying = true;
    }

    public void GameOver()
    {
        IsPlaying = false;
    }


    public void PlayButtonClick()
    {
        SceneManager.LoadScene("MainGameScene");
        StartGame();
    }
}
