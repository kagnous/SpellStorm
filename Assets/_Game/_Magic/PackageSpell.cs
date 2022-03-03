using UnityEngine;

[CreateAssetMenu(fileName = "NewPackageSpell", menuName = "Magic/PackageSpell")]
public class PackageSpell : ScriptableObject
{
    [SerializeField, Tooltip("Liste des variantes du spell")]
    private MagicSpell[] _spells;

    [SerializeField, Tooltip("Liste des caps de changement de puissance")]
    private float[] _powerGab;

    public MagicSpell GetGoodSpell(float power)
    {
        for (int i = 0; i < _spells.Length; i++)
        {
            if(power <= _powerGab[i])
            {
                Debug.Log($"Cast sort de niv{i+1}");
                return _spells[i];
            }
        }
        return _spells[0];
    }
}