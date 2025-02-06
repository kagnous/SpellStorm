using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [SerializeField, Tooltip("Dropdown (menu d�roulant) de la r�solution d'�cran")]
    private Dropdown resolutionsDropdown;

    [SerializeField, Tooltip("AudioMixer principal")]
    private AudioMixer audioMixer;

    [Header("Slider son :")]
    [SerializeField, Tooltip("Slider du son g�n�ral")]
    private Slider generalSoundSlider;
    //[SerializeField, Tooltip("Slider du son de la musique")]
    //private Slider musicSlider;
    //[SerializeField, Tooltip("Slider du son bruitage")]
    //private Slider soundSlider;

    private void Awake()
    {
        Screen.fullScreen = true;
    }

    /// <summary>
    /// Set la r�solution d'�cran selon l'index d'un dropdown
    /// </summary>
    /// <param name="resolutionIndex">index s�lectionn� du dropdown</param>
    public void SetResolution(int resolutionIndex)
    {
        // On set aux bonnes valeurs selon l'index re�u
        switch (resolutionIndex)
        {
            case 0:
                Screen.SetResolution(1920, 1080, Screen.fullScreen);
                Debug.Log("1920x1080");
                break;
            case 1:
                Screen.SetResolution(1680, 1050, Screen.fullScreen);
                Debug.Log("1680x1050");
                break;
            case 2:
                Screen.SetResolution(1280, 720, Screen.fullScreen);
                Debug.Log("1280x720");
                break;
            case 3:
                Screen.SetResolution(720, 480, Screen.fullScreen);
                Debug.Log("720x480");
                break;
            default:
                Debug.LogError("Index de dropdown non compris");
                break;
        }
    }

    /// <summary>
    /// Set le plein �cran ou l'enl�ve selon un bool�en
    /// </summary>
    /// <param name="isFullScreen">bool�en re�u</param>
    public void FullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        Debug.Log("Plein �cran = " + isFullScreen);
    }

    /// <summary>
    /// Set la valeur du son du mixer
    /// </summary>
    /// <param name="volume">volume re�u</param>
    public void SetGeneralSound(float volume)
    {
        // Si le son est inf�rieur � -30 (presque inaudible), on le d�sactive (-80, le minimum)
        if (volume <= -30)
        {
            volume = -80;
        }
        audioMixer.SetFloat("Music", volume);
    }
    public void SetMusic(float volume)
    {
        if (volume <= -30)
        {
            volume = -80;
        }
        audioMixer.SetFloat("Music", volume);
    }
    public void SetBruitage(float volume)
    {
        if (volume <= -30)
        {
            volume = -80;
        }
        audioMixer.SetFloat("Sound", volume);
    }
}