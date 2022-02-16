using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField, Tooltip("Panel de game over")]
    private GameObject _gameOverPanel;

    private void Awake()
    {
        _gameOverPanel.SetActive(false);
    }

    public void GameOver()
    {
        Debug.Log("GAME OVER");
        _gameOverPanel.SetActive(true);
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Application quittée");
    }
}