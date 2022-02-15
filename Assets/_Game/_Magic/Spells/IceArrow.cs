using UnityEngine;

[CreateAssetMenu(fileName = "NewIceArrow", menuName = "Magic/Spell/IceArrow")]
public class IceArrow : ProjectileSpell
{
    [SerializeField]
    private EffectMother effect;

    public override void Impact(Collider2D collision, GameObject projectile)
    {
        if(collision.gameObject.tag == "Mob" || collision.gameObject.tag == "Player")
        {
            //collision.gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0.5f, 1, 1);

            collision.gameObject.GetComponent<StatsManager>().AddEffect(effect);
        }
    }
}