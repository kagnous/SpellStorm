using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [SerializeField, Tooltip("Le nom affiché du personnage")]
    private string name; public string Name => name;

    [SerializeField, Tooltip("Les différentes phrases du personnage"), TextArea(3, 10)]
    private string[] sentences; public string[] Sentences => sentences;
}