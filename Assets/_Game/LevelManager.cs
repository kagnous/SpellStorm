using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

[DisallowMultipleComponent]
public class LevelManager : MonoBehaviour
{
    private GameObject player;

    [SerializeField, Tooltip("Panel de game over")]
    private GameObject _gameOverPanel;

    [SerializeField, Tooltip("Bouton par défaut pour le panneau GameOver")]
    private GameObject _firtGameOverButton;

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

        Debug.Log("là !");
        // Place le controller sur le premier boutton
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_firtGameOverButton);
    }

    /// <summary>
    /// Recharge le premier niveau
    /// </summary>
    public void ReloadGame()
    {
        EventSystem.current.SetSelectedGameObject(null);
        SceneManager.LoadScene("Level_1");
    }

    /// <summary>
    /// Ramene au Main Menu
    /// </summary>
    public void Quit()
    {
        EventSystem.current.SetSelectedGameObject(null);
        SceneManager.LoadScene("MainMenu");
    }
}