using UnityEngine;

[CreateAssetMenu(fileName = "NewGustOfWind", menuName = "Magic/Spell/GustOfWind")]
public class GustOfWind : ProjectileSpell
{
    [SerializeField]
    private int intensity;

    [SerializeField]
    private EffectMother effect;

    public override void Impact(Collider2D collision, GameObject projectile)
    {
        if (collision.gameObject.tag == "Mob" || collision.gameObject.tag == "Player")
        {
                //Debug.Log("Woosh");
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(intensity * -Mathf.Sign(projectile.transform.localScale.x),0));
            //Debug.Log(-Mathf.Sign(projectile.transform.localScale.x));
            if (collision.gameObject.tag == "Player")
            {
                collision.GetComponent<StatsManager>().AddEffect(effect);
                collision.GetComponent<PlayerController>().DirectionMovment = Vector2.zero;
            }
        }
    }
}