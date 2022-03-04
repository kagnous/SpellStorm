using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
public class PlayerCasting : MonoBehaviour
{
    private bool _isCasting = false;

    private GameInput _inputsInstance = null;

    private StatsManager _casterStats;

    [SerializeField, Tooltip("Forme actuellement sélectionnée dans le grimoire")]
    private MagicForm actualForm;
    private int actualFormIndex = 0;

    [SerializeField, Tooltip("Element actuellement sélectionnée dans le grimoire")]
    private MagicElement actualElement;
    private int actualElementIndex = 0;

    #region Grimoire
    [Header("Grimoire :")]
    [SerializeField, Tooltip("Liste de forme")]
    private List<MagicForm> forms = new List<MagicForm>();

    [SerializeField, Tooltip("Liste d'éléments")]
    private List<MagicElement> elements = new List<MagicElement>();

    [SerializeField, Tooltip("Liste sorts")]
    private List<PackageSpell> spells = new List<PackageSpell>();

    // Script de la gestion graphique du grimoire (UI)
    private SpellBookDisplayer _spellBookDisplayer;
    #endregion

    // Event
    public delegate void SpellDelegate(MagicSpell spell);
    public event SpellDelegate eventCast;

    private void Awake()
    {
        _inputsInstance = new GameInput();

        if(gameObject.tag == "Player")
        {
            _casterStats = GetComponent<StatsPlayerManager>();
        }
        else
        {
            _casterStats = GetComponent<StatsManager>();
        }
    }
    private void Start()
    {
        // Initialisation des assets et du visuel du grimoire
        actualForm = forms[0]; actualElement = elements[0];
        _spellBookDisplayer = FindObjectOfType<SpellBookDisplayer>();
        _spellBookDisplayer.DisplaySpellBook(actualForm, actualElement);
    }

    private void OnEnable()
    {
        // Assignation des fonctions aux Inputs
        _inputsInstance.Player.Enable();
        _inputsInstance.Player.Cast.performed += Casting;
        _inputsInstance.Player.RollForm.performed += RollForm;
        _inputsInstance.Player.RollElement.performed += RollElement;
    }
    private void OnDisable()
    {
        // Désassignation des fonctions aux Inputs
        _inputsInstance.Player.Cast.performed -= Casting;
        _inputsInstance.Player.RollForm.performed -= RollForm;
        _inputsInstance.Player.RollElement.performed -= RollElement;
    }

    /// <summary>
    /// Charge le sort ou le lance si il était déjà en train de charger
    /// </summary>
    public void Casting(InputAction.CallbackContext context)
    {
        _isCasting = !_isCasting;
        if(_isCasting)
        StartCoroutine(CastingCoroutine());
    }

    /// <summary>
    /// Cast du sort correspondant à la forme/élément selectionné
    /// </summary>
    private void Cast(float power)
    {
        //Pour chaque sort connu dans le grimoire...
        for (int i = 0; i < spells.Count; i++)
        {
            // Si la forme et l'élément sélectionné correspondent...
            if (spells[i].Spells[0].Form == actualForm && spells[i].Spells[0].Element == actualElement)
            {
                MagicSpell spell = spells[i].GetGoodSpell(power);

                // On vérifie si on peut bien caster ce sort
                if(CanCast(spell))
                {
                    // On appelle la fonction Cast du sort en question
                    spell.Cast(gameObject);
                    // On enlève le mana correspondant au caster (s'il en a)
                    if (_casterStats != null)
                    {
                        _casterStats.SetMana(-spell.Mana);
                    }
                    // On prévient l'event de Cast
                    eventCast?.Invoke(spell);
                }
                return;
            }
        }
        Debug.Log("Aucun sort correspondant trouvé");
    }

    /// <summary>
    /// Test si le sort peu bien être casté
    /// </summary>
    /// <param name="spell">Le sort en question</param>
    /// <returns>Si le sort peut être casté</returns>
    private bool CanCast(MagicSpell spell)
    {
        // On compare le mana du sort et du caster (si pas de stats, cast)
        if(_casterStats != null)
        {
            if(spell.Mana > _casterStats.Mana)
            {
                return false;
            }
        }

        // On vérifie si le caster (player uniquement) est freeze
        if(gameObject.tag == "Player")
        {
            PlayerController playerMovment = GetComponent<PlayerController>();
            // Si freeze, on vérifie si le sort casté est personnel, sinon on retourne false
            if (!playerMovment.isActiveAndEnabled)
            {
                if (spell.Form.name != "FormePersonnelle")
                {
                    return false;
                }
            }
        }

        // Si tout les test on été passés avec succès, on peut bien caster le sort
        return true;
    }

    /// <summary>
    /// On saute de 1 dans la liste des formes de sorts
    /// </summary>
    private void RollForm(InputAction.CallbackContext context)
    {
        // On incrémente ou on remet à 0 l'index de la liste des formes
        if (actualFormIndex >= forms.Count - 1) actualFormIndex = 0;
        else actualFormIndex++;

        // On met à jours les assets et le visuel du grimoire
        actualForm = forms[actualFormIndex];
        _spellBookDisplayer.DisplaySpellBook(actualForm, actualElement);
        //Debug.Log(forms[actualFormIndex].name);
    }

    /// <summary>
    /// On saute de 1 dans la liste des éléments de sorts
    /// </summary>
    private void RollElement(InputAction.CallbackContext context)
    {
        // On incrémente ou on remet à 0 l'index de la liste des éléments
        if (actualElementIndex >= elements.Count - 1) actualElementIndex = 0;
        else actualElementIndex++;

        // On met à jours les assets et le visuel du grimoire
        actualElement = elements[actualElementIndex];
        _spellBookDisplayer.DisplaySpellBook(actualForm, actualElement);
        //Debug.Log(elements[actualElementIndex].name);
    }

    /// <summary>
    /// Désative les Input de Cast
    /// </summary>
    public void StopCastInput()
    {
        // Désassignation des fonctions aux Inputs
        _inputsInstance.Player.Cast.performed -= Casting;
        _inputsInstance.Player.RollForm.performed -= RollForm;
        _inputsInstance.Player.RollElement.performed -= RollElement;
    }

    /// <summary>
    /// Réactive les Input de Cast
    /// </summary>
    public void ResumeCastInput()
    {
        // Réassignation des fonctions aux Inputs
        _inputsInstance.Player.Cast.performed += Casting;
        _inputsInstance.Player.RollForm.performed += RollForm;
        _inputsInstance.Player.RollElement.performed += RollElement;
    }

    /// <summary>
    /// Accumule le temps entre l'Input tenu et laché, puis appelle Cast() quand laché
    /// </summary>
    IEnumerator CastingCoroutine()
    {
        float timer = 0;
        while(_isCasting)
        {
            yield return null;
            timer += Time.deltaTime;
        }
        //Debug.Log(timer);
        Cast(timer);
    }
}