using UnityEngine;

[CreateAssetMenu(fileName = "NewMagicElement", menuName = "Magic/MagicElement")]
public class MagicElement : ScriptableObject
{
    [SerializeField, Tooltip("Logo du l'élément sur le grimoire")]
    private Sprite sprite; public Sprite Sprite => sprite;

}