using UnityEngine;

[CreateAssetMenu(fileName = "NewIceArrow", menuName = "Magic/SpecialSpell/IceArrow")]
public class IceArrow : ProjectileSpell
{
    public override void Impact(Collider2D collision, GameObject projectile)
    {
        if(collision.gameObject.tag == "Mob" || collision.gameObject.tag == "Player")
        {
            StatsManager statsTarget = collision.gameObject.GetComponent<StatsManager>();
            
            // On g�le la cilble (faudras faire un �tat Gel dans Stat Manager, avec dedans la dru�e et le fait d'enlever l'�tat)
            statsTarget.Speed = 0;
            collision.gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0.5f, 1, 1);
        }
    }
}