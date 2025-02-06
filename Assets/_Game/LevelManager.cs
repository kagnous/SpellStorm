using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.IO;

[DisallowMultipleComponent]
public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private SavePlayerSpawn savePlayerSpawn;
    private Transform playerSpawnPosition;
    [SerializeField]
    private Vector3 firstPlayerSpawnPosition;

    private Animator transitionAnimator;

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

        playerSpawnPosition = FindObjectOfType<PlayerSpawn>().transform;
        playerSpawnPosition.position = savePlayerSpawn.PlayerSpawnPosition;

        player = FindObjectOfType<PlayerController>().gameObject;

        _gameOverPanel.SetActive(false);

        // On récupère l'animator de transition en choppant le CanvasGroup, vu que seul le transition l'utilise
        transitionAnimator = FindObjectOfType<CanvasGroup>().GetComponent<Animator>();
    }

    /// <summary>
    /// Lance le Game Over
    /// </summary>
    public void GameOver()
    {
            //Debug.Log("GAME OVER");
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<PlayerCasting>().enabled = false;

        ResetAllEnnemies();

        Saving();

        _gameOverPanel.SetActive(true);

        // Place le controller sur le premier boutton
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_firtGameOverButton);
    }

    private void ResetAllEnnemies()
    {
        List<EnnemiController> ennemies = new List<EnnemiController>();
        ennemies.AddRange(FindObjectsOfType<EnnemiController>());

        for (int i = 0; i < ennemies.Count; i++)
        {
            //Debug.Log("Reset comportement");
            ennemies[i].State = EnnemiController.EnnemiState.None;
        }
    }

    /// <summary>
    /// Recharge le premier niveau
    /// </summary>
    public void ReloadGame()
    {
        EventSystem.current.SetSelectedGameObject(null);
        StartCoroutine(LoadNewScene("Level_1"));
    }

    /// <summary>
    /// Ramene au Main Menu
    /// </summary>
    public void Quit()
    {
        EventSystem.current.SetSelectedGameObject(null);
        savePlayerSpawn.PlayerSpawnPosition = firstPlayerSpawnPosition;
        StartCoroutine(LoadNewScene("MainMenu"));
    }

    public void Saving()
    {
        savePlayerSpawn.PlayerSpawnPosition = playerSpawnPosition.position;
        savePlayerSpawn.Save();
    }

    IEnumerator LoadNewScene(string levelName)
    {
        transitionAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(levelName);
    }
}