using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance;

    [SerializeField] private List<GameObject> _livesImages;

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


    public void DisplayLives(int lives)
    {
        foreach (GameObject item in _livesImages)
        {
            item.SetActive(false);
        }

        for (int i = 0; i < lives; i++)
        {
            _livesImages[i].SetActive(true);
        }
    }

    public void PlayButtonClick()
    {
        SceneManager.LoadScene("MainGameScene");
    }

    public void QuitButton()
    {
        Application.Quit();
    }

}
