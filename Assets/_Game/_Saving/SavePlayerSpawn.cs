using System.IO;

using UnityEngine;

[CreateAssetMenu(fileName = "NewSavePlayerSpawn", menuName = "Game/SavePlayerSpawn")]
public class SavePlayerSpawn : ScriptableObject
{
    // On crée la variable du chemin vers le fichier de sauvegarde en statique (une fois malgré plusieurs instances) et intouchable (readonly)
    // et on en fait un acesseur pour ne le chercher que quand on en a besoin
    public static string SaveFilePath => Path.Combine(Application.persistentDataPath, "save.json");

    [SerializeField]
    private Vector3 playerSpawnPosition; public Vector3 PlayerSpawnPosition { get { return playerSpawnPosition; } set { playerSpawnPosition = value; } }

    public void Save()
    {
        string json = JsonUtility.ToJson(this, true);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "save.json"), json);

            //Debug.Log("PERSISTENT DATA PATH : " + Application.persistentDataPath);
    }

    public void Load()
    {
        string jsonToLoad = File.ReadAllText(SaveFilePath);
        JsonUtility.FromJsonOverwrite(jsonToLoad, this);
    }
}