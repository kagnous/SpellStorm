using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuController : MonoBehaviour
{
    [SerializeField, Tooltip("Nom de la Scene à charger en cas de Play")]
    private string levelToLoad = "Level_1";
    [Header("Menus :")]
    [SerializeField, Tooltip("Page de menu principal")]
    private GameObject mainWindow;
    [SerializeField, Tooltip("Page de menu des paramètres")]
    private GameObject settingsWindow;
    [SerializeField, Tooltip("Page de confirmation de Quit")]
    private GameObject closeWindow;

    #region EventSystemFirstSelected
    [Header("First selected")]
    [SerializeField, Tooltip("Boutton selectionné à l'apparition de la scène")]
    private GameObject loadFirstButton;
    [SerializeField, Tooltip("Boutton selectionné à l'apparition des options")]
    private GameObject optionFirstButton;
    [SerializeField, Tooltip("Boutton selectionné en quittant les options")]
    private GameObject optionCloseButton;
    [SerializeField, Tooltip("Boutton selectionné à l'apparition du quit")]
    private GameObject quitFirstButton;
    [SerializeField, Tooltip("Boutton selectionné en quittant le quit")]
    private GameObject quitCloseButton;
    #endregion

    private void Awake()
    {
        // Rend le curseur visible
        Cursor.visible = true;
        // Lock le curseur au sein de la fenêtre de jeu
        //Cursor.lockState = CursorLockMode.Confined;

        // Active le panneau principal et désactive les autres
        mainWindow.SetActive(true);
        settingsWindow.SetActive(false);
        closeWindow.SetActive(false);

        // Place le controller sur le premier boutton
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(loadFirstButton);
    }

    // ============================= MAIN MENU ===================================
    #region Main Menu

    /// <summary>
    /// Load la Scene de jeu
    /// </summary>
    public void ButtonPlay()
    {
        FindObjectOfType<CanvasGroup>().GetComponent<Animator>().SetTrigger("Start");
        Invoke(nameof(LoadNewScene), 1f);
    }

    private void LoadNewScene()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    /// <summary>
    /// Active la page des Settings et enlève le Main Menu
    /// </summary>
    public void ButtonSettings()
    {
        mainWindow.SetActive(false);
        settingsWindow.SetActive(true);

        // Place le controller sur le premier boutton option
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionFirstButton);
    }

    /// <summary>
    /// Load la scène de credits
    /// </summary>
    public void ButtonCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    /// <summary>
    /// Active la page de confirmation de Quit et enlève le Main Menu
    /// </summary>
    public void ButtonQuit()
    {
        mainWindow.SetActive(false);
        closeWindow.SetActive(true);

        // Place le controller sur le premier boutton quit
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(quitFirstButton);
    }
    #endregion

    // ============================= SETTINGS MENU ===============================
    #region Settings Menu

    /// <summary>
    /// Ferme la page des Settings et ouvre le Main Menu
    /// </summary>
    public void ButtonCloseSettings()
    {
        settingsWindow.SetActive(false);
        mainWindow.SetActive(true);

        // Place le controller sur le bon boutton MainMenu
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionCloseButton);
    }
    #endregion

    // ============================= QUIT MENU ===================================
    #region Quit Menu

    /// <summary>
    /// Enlève la page de confirmation et met le Main Menu
    /// </summary>
    public void ButtonCancelQuit()
    {
        closeWindow.SetActive(false);
        mainWindow.SetActive(true);

        // Place le controller sur le premier boutton
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(quitCloseButton);
    }

    /// <summary>
    /// Ferme l'application
    /// </summary>
    public void ButtonConfirmQuit()
    {
        Application.Quit();
        Debug.Log("Application quittée");
    }
    #endregion
}