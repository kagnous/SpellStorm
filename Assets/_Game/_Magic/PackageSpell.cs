using UnityEngine;

[CreateAssetMenu(fileName = "NewPackageSpell", menuName = "Magic/PackageSpell")]
public class PackageSpell : ScriptableObject
{
    [SerializeField, Tooltip("Liste des variantes du spell par puissance croissante")]
    private MagicSpell[] _spells; public MagicSpell[] Spells => _spells;

    [SerializeField, Tooltip("Liste croissante des caps de changement de puissance")]
    private float[] _powerGap; public float[] PowerGab => _powerGap;

    /// <summary>
    /// Retourne le bon sort du pack selon la puissance
    /// </summary>
    /// <param name="power">Puissance du sort</param>
    /// <returns>Sort correspondant à la puissance</returns>
    public MagicSpell GetGoodSpell(float power)
    {
        // Pour chaque gap de puissance...                  (si la liste des gaps est vide, tirera automatiquement le sort de base)
        for (int i = _powerGap.Length; i > 0; i--)
        {
            // On teste si la puissance est supérieure au gap (-1 car Lenght commence à 1 et l'index à 0)
            if (power >= _powerGap[i-1])
            {
                // Si oui on retourne le spell en question
                Debug.Log($"Cast sort de niv{i+1}");
                return _spells[i];
            }
        }
        // Si aucun gab ne correspond, alors on retourne la version du sort la plus faible
        Debug.Log($"Cast sort de niv 1");
        return _spells[0];
    }
}