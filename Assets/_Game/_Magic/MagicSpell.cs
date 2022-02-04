using UnityEngine;

[CreateAssetMenu(fileName = "NewSpell", menuName = "Magic/DefaultSpell")]
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

    virtual public void Cast(MagicSpell magicSpell)
    {
        Debug.Log($"Cast {name} : {form} + {element} !");
    }
}