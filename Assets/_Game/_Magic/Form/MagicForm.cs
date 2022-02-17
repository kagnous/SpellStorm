using UnityEngine;

[CreateAssetMenu(fileName = "NewMagicForm", menuName = "Magic/MagicForm")]
public class MagicForm : ScriptableObject
{
    [SerializeField, Tooltip("Logo de la forme sur le grimoire")]
    private Sprite sprite; public Sprite Sprite => sprite;


}