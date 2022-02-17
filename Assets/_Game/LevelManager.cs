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

    /// <summary>
    /// Lance le Game Over
    /// </summary>
    public void GameOver()
    {
        Debug.Log("GAME OVER");
        _gameOverPanel.SetActive(true);
    }

    /// <summary>
    /// Recharge le premier niveau
    /// </summary>
    public void ReloadGame()
    {
        SceneManager.LoadScene("Level_1");
    }

    /// <summary>
    /// Ramene au Main Menu
    /// </summary>
    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}