using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterCasting : MonoBehaviour
{
    private GameInput _inputsInstance = null;

    private StatsManager _casterStats;

    [SerializeField, Tooltip("Forme actuellement s�lectionn�e dans le grimoire")]
    private MagicForm actualForm;
    private int actualFormIndex = 0;

    [SerializeField, Tooltip("Element actuellement s�lectionn�e dans le grimoire")]
    private MagicElement actualElement;
    private int actualElementIndex = 0;

    #region Grimoire
    [Header("Grimoire :")]
    [SerializeField, Tooltip("Liste de forme")]
    private List<MagicForm> forms = new List<MagicForm>();

    [SerializeField, Tooltip("Liste d'�l�ments")]
    private List<MagicElement> elements = new List<MagicElement>();

    [SerializeField, Tooltip("Liste sorts")]
    private List<MagicSpell> spells = new List<MagicSpell>();

    // Script de la gestion graphique du grimoire (UI)
    private SpellBookDisplayer _spellBookDisplayer;
    #endregion

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
        _inputsInstance.Player.Cast.performed += Cast;
        _inputsInstance.Player.RollForm.performed += RollForm;
        _inputsInstance.Player.RollElement.performed += RollElement;
    }
    private void OnDisable()
    {
        // D�sassignation des fonctions aux Inputs
        _inputsInstance.Player.Cast.performed -= Cast;
        _inputsInstance.Player.RollForm.performed -= RollForm;
        _inputsInstance.Player.RollElement.performed -= RollElement;
    }

    /// <summary>
    /// Cast du sort correspondant � la forme/�l�ment selectionn�
    /// </summary>
    private void Cast(InputAction.CallbackContext context)
    {
        //Pour chaque sort connu dans le grimoire...
        for (int i = 0; i < spells.Count; i++)
        {
            // Si la forme et l'�l�ment s�lectionn� correspondent...
            if (spells[i].Form == actualForm && spells[i].Element == actualElement)
            {
                // On v�rifie si on peut bien caster ce sort
                if(CanCast(spells[i]))
                {
                    // On appelle la fonction Cast du sort en question
                    spells[i].Cast(gameObject);
                    // On enl�ve le mana correspondant au caster (s'il en a)
                    if (_casterStats != null)
                    {
                        _casterStats.SetMana(-spells[i].Mana);
                    }
                }
                return;
            }
        }
        Debug.Log("Aucun sort correspondant trouv�");
    }

    /// <summary>
    /// Test si le sort peu bien �tre cast�
    /// </summary>
    /// <param name="spell">Le sort en question</param>
    /// <returns>Si le sort peut �tre cast�</returns>
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

        // On v�rifie si le caster (player uniquement) est freeze
        if(gameObject.tag == "Player")
        {
            CharacterMovement playerMovment = GetComponent<CharacterMovement>();
            // Si freeze, on v�rifie si le sort cast� est personnel, sinon on retourne false
            if (!playerMovment.isActiveAndEnabled)
            {
                if (spell.Form.name != "FormePersonnelle")
                {
                    return false;
                }
            }
        }

        // Si tout les test on �t� pass�s avec succ�s, on peut bien caster le sort
        return true;
    }

    /// <summary>
    /// On saute de 1 dans la liste des formes de sorts
    /// </summary>
    private void RollForm(InputAction.CallbackContext context)
    {
        // On incr�mente ou on remet � 0 l'index de la liste des formes
        if (actualFormIndex >= forms.Count - 1) actualFormIndex = 0;
        else actualFormIndex++;

        // On met � jours les assets et le visuel du grimoire
        actualForm = forms[actualFormIndex];
        _spellBookDisplayer.DisplaySpellBook(actualForm, actualElement);
        //Debug.Log(forms[actualFormIndex].name);
    }

    /// <summary>
    /// On saute de 1 dans la liste des �l�ments de sorts
    /// </summary>
    private void RollElement(InputAction.CallbackContext context)
    {
        // On incr�mente ou on remet � 0 l'index de la liste des �l�ments
        if (actualElementIndex >= elements.Count - 1) actualElementIndex = 0;
        else actualElementIndex++;

        // On met � jours les assets et le visuel du grimoire
        actualElement = elements[actualElementIndex];
        _spellBookDisplayer.DisplaySpellBook(actualForm, actualElement);
        //Debug.Log(elements[actualElementIndex].name);
    }
}