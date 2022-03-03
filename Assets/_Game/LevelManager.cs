using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class LevelManager : MonoBehaviour
{
    private GameObject player;

    [SerializeField, Tooltip("Panel de game over")]
    private GameObject _gameOverPanel;

    // La seule et unique instance de Level Manager
    public static LevelManager instance;

    private void Awake()
    {
        // Permet de vérifier qu'il n'y a qu'une seule instance de la classe dans la scène
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de LevelManager dans la scène");
            return;
        }
        instance = this;

        player = FindObjectOfType<PlayerController>().gameObject;

        _gameOverPanel.SetActive(false);
    }

    /// <summary>
    /// Lance le Game Over
    /// </summary>
    public void GameOver()
    {
        Debug.Log("GAME OVER");
        player.SetActive(false);
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