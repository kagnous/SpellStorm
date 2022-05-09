using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [SerializeField, Tooltip("Le nom affich� du personnage")]
    private string name; public string Name => name;

    [SerializeField, Tooltip("Les diff�rentes phrases du personnage"), TextArea(3, 10)]
    private string[] sentences; public string[] Sentences => sentences;
}