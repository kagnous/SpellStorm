using UnityEngine;

[CreateAssetMenu(fileName = "NewEffectRepeat", menuName = "Game/EffectRepeat")]
public class EffectRepeat : EffectMother
{
    [SerializeField]
    private int coup = 10;
    [SerializeField]
    private int cadence = 1;

    public override void AddEffectController(GameObject effect)
    {
        EffectRepeatController effectController = effect.AddComponent<EffectRepeatController>();
        effectController.Coup = coup;
        effectController.Cadence = cadence;
    }
}