using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[DisallowMultipleComponent]
public class DialogueManager : MonoBehaviour
{
    private GameObject _player;

    private DialogueController _NPCtalking;

    [SerializeField, Tooltip("Intervalle de temps entre 2 charactères"), Min(0)]
    private float _dialogueSpeed = 0.002f;

    // Tout les éléments utile de l'UI de dialogue
    #region UIcomponent
    private GameObject _dialoguePanel;
    private Animator _animator;
    private Text _NPCName;
    private Text _dialogue;
    private GameObject _continueButton;
    #endregion

    // Queue (comme une List, mais plus orientée file d'attente) des phrases à afficher
    private Queue<string> sentences;

    // La seule et unique instance de dialogue Manager
    public static DialogueManager instance;

    private void Awake()
    {
        // Permet de vérifier qu'il n'y a qu'une seule instance de la classe dans la scène
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de DialogueManager dans la scène");
            return;
        }
        instance = this;

        // On initialise la queue
        sentences = new Queue<string>();

        // Récupère le DialoguePanel et ses Composants
        _dialoguePanel = GameObject.FindGameObjectWithTag("MainUI").transform.Find("DialoguePanel").gameObject;
        //_dialoguePanel = FindObjectOfType<Canvas>().transform.Find("DialoguePanel").gameObject;
        _animator = _dialoguePanel.GetComponent<Animator>();
        _NPCName = _dialoguePanel.transform.Find("NPCname").GetComponent<Text>();
        _dialogue = _dialoguePanel.transform.Find("DialogueText").GetComponent<Text>();
        _continueButton = _dialoguePanel.transform.Find("ContinueButton").gameObject;

        // On récupère le Player
        _player = FindObjectOfType<PlayerController>().gameObject;
    }

    /// <summary>
    /// Lance un dialogue avec l'UI de dialogue
    /// </summary>
    /// <param name="dialogue">Le dialogue à afficher</param>
    public void StartDialogue(Dialogue dialogue, DialogueController NPC)
    {
        // On met à jour le PNJ qui parle
        _NPCtalking = NPC;

        // On désactive les input inutiles en dialogue du Player
        _player.GetComponent<PlayerController>().StopMoveInput();
        _player.GetComponent<PlayerCasting>().StopCastInput();

        // Place le controller sur le boutton Continue
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_continueButton);

        // On approche le panel avec l'anim'
        _animator.SetBool("IsOpen", true);

        // On vide les phrases du dernier dialogue
        sentences.Clear();

        // On affiche le nom du PNJ
        _NPCName.text = dialogue.Name;
        
        // Met (dans l'ordre où elles existent) les phrases de sentence dans la file d'attente
        foreach (string sentence in dialogue.Sentences)
        {
            sentences.Enqueue(sentence);
        }

        // On affiche la première phrase
        DisplayNextSentence();
    }

    /// <summary>
    /// Affiche la phrase suivante du dialogue si il y en a
    /// </summary>
    public void DisplayNextSentence()
    {
        // On vérifie si la phrase n'est pas la dernière
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        // Sinon
        // On récupère la prochaine phrase dans la file d'attente (ce qui la vire de la file au passage)
        string sentence = sentences.Dequeue();
        // On arrête la coroutine qui affichait le texte (si elle tournait encore)
        StopAllCoroutines();
        // On affiche la nouvelle phrase
        StartCoroutine(TypeSentence(sentence));
    }

    /// <summary>
    /// Met fin au dialogue et cache l'UI
    /// </summary>
    private void EndDialogue()
    {
        _animator.SetBool("IsOpen", false);

        // Place le controller sur aucun bouton
        EventSystem.current.SetSelectedGameObject(null);

        // On réactive les input inutiles en dialogue du Player
        _player.GetComponent<PlayerController>().ResumeMoveInput();
        _player.GetComponent<PlayerCasting>().ResumeCastInput();

        // On dit au PNJ d'arrêter de parler
        _NPCtalking.IsTalking = false;
    }

    /// <summary>
    /// Affiche la phrase charactère par charactère
    /// </summary>
    /// <param name="sentence">La phrase à afficher</param>
    IEnumerator TypeSentence(string sentence)
    {
        // On clean le texte
        _dialogue.text = "";
        
        // Affiche un par un chaque charactère de la sentence
        foreach (char letter in sentence.ToCharArray())
        {
            _dialogue.text += letter;
            yield return new WaitForSeconds(_dialogueSpeed);
        }
    }
}