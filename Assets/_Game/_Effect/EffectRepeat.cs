using UnityEngine;

[CreateAssetMenu(fileName = "NewEffectRepeat", menuName = "Game/EffectRepeat")]
public class EffectRepeat : EffectMother
{
    [SerializeField, Tooltip("Nombre de fois que l'effet va être appliqué")]
    private int coup = 10;
    [SerializeField, Tooltip("Intervalle de temps entre les application")]
    private int cadence = 1;

    public override void AddEffectController(GameObject effect)
    {
        EffectRepeatController effectController = effect.AddComponent<EffectRepeatController>();
        effectController.Coup = coup;
        effectController.Cadence = cadence;
    }

    public override void EndEffect(StatsManager target)
    {
        target.GetComponent<SpriteRenderer>().color = Color.white;
    }
}