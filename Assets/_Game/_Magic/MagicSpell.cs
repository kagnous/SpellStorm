using UnityEngine;

abstract public class MagicSpell : ScriptableObject
{
    [SerializeField, Tooltip("Logo du sort")]
    protected Sprite spellSprite; public Sprite Sprite => spellSprite;

    [SerializeField, Tooltip("Forme du sort")]
    protected MagicForm form; public MagicForm Form => form;

    [SerializeField, Tooltip("Element du sort")]
    protected MagicElement element;   public MagicElement Element => element;

    [SerializeField, Tooltip("Coût en Mana")]
    protected int _mana; public int Mana => _mana;

    // A confirmer...
    //[SerializeField, Tooltip("Durée du sort ou du blocage avant de prochain tir")]
    //protected float _duration; public float Duration => _duration;

    /// <summary>
    /// Fonction appelée à l'invocation du sort
    /// </summary>
    /// <param name="spell">Le sort concerné</param>
    /// <param name="caster">L'objet qui a lancé le sort</param>
    virtual public void Cast(GameObject caster)
    {
        Debug.Log($"{name} ({form.name} + {element.name}) !");
    }
}