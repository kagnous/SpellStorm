using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterCasting : MonoBehaviour
{
    private GameInput _inputsInstance = null;

    [SerializeField]
    private MagicForm actualForm;
    private int actualFormIndex = 0;

    [SerializeField]
    private MagicElement actualElement;
    private int actualElementIndex = 0;

    private bool _canCast = true; public bool CanCast { get { return _canCast; } set { _canCast = value; } }

    #region Grimoire
    [Header("Grimoire :")]
    [SerializeField, Tooltip("Liste de forme")]
    private List<MagicForm> forms = new List<MagicForm>();

    [SerializeField, Tooltip("Liste d'éléments")]
    private List<MagicElement> elements = new List<MagicElement>();

    [SerializeField, Tooltip("Liste sorts")]
    private List<MagicSpell> spells = new List<MagicSpell>();

    [SerializeField, Tooltip("Visuel du grimoire")]
    private SpellBookDisplayer _spellBookDisplayer;
    #endregion

    private void Awake()
    {
        _inputsInstance = new GameInput();
        _spellBookDisplayer = FindObjectOfType<SpellBookDisplayer>();
    }
    private void Start()
    {
        actualForm = forms[0];
        actualElement = elements[0];
        _spellBookDisplayer.DisplaySpellBook(actualForm, actualElement);
    }

    private void OnEnable()
    {
        _inputsInstance.Player.Enable();
        _inputsInstance.Player.Cast.performed += Cast;
        _inputsInstance.Player.RollForm.performed += RollForm;
        _inputsInstance.Player.RollElement.performed += RollElement;
    }
    private void OnDisable()
    {
        _inputsInstance.Player.Cast.performed -= Cast;
        _inputsInstance.Player.RollForm.performed -= RollForm;
        _inputsInstance.Player.RollElement.performed -= RollElement;
    }

    private void Cast(InputAction.CallbackContext context)
    {
        for (int i = 0; i < spells.Count; i++)
        {
            if (spells[i].Form == actualForm && spells[i].Element == actualElement)
            {
                spells[i].Cast(spells[i], gameObject);
                StartCoroutine(EndSpellCoroutine(spells[i].Duration, spells[i]));
                return;
            }
        }
        Debug.Log("Aucun sort correspondant trouvé");
    }

    private void RollForm(InputAction.CallbackContext context)
    {
        if (actualFormIndex >= forms.Count -1) actualFormIndex = 0;
        else actualFormIndex++;

        actualForm = forms[actualFormIndex];
        //Debug.Log(forms[actualFormIndex].name);
        _spellBookDisplayer.DisplaySpellBook(actualForm, actualElement);
    }

    private void RollElement(InputAction.CallbackContext context)
    {
        if (actualElementIndex >= elements.Count -1) actualElementIndex = 0;
        else actualElementIndex++;

        actualElement = elements[actualElementIndex];
        //Debug.Log(elements[actualElementIndex].name);
        _spellBookDisplayer.DisplaySpellBook(actualForm, actualElement);
    }

    IEnumerator EndSpellCoroutine(float duration, MagicSpell spell)
    {
        yield return new WaitForSeconds(duration);
        spell.EndSpell(spell, gameObject);
    }
}