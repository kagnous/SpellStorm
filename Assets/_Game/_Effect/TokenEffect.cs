using UnityEngine;

[CreateAssetMenu(fileName = "NewTokenEffect", menuName = "Game/TokenEffect")]
public class TokenEffect : ScriptableObject
{
    [SerializeField]
    private int coup = 10;
    [SerializeField]
    private int cadence = 1;

    [SerializeField]
    private GameObject refEffect;

    /*public void Apply(StatsManager stats)
    {
        GameObject effect = Instantiate(refEffect,stats.transform);
        Effect effectController =  effect.GetComponent<Effect>();
        effectController.Coup = coup;
        effectController.Cadence = cadence;
        effectController.Target = stats;
    }*/
}