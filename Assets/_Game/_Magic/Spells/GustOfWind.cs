using UnityEngine;

[CreateAssetMenu(fileName = "NewGustOfWind", menuName = "Magic/Spell/GustOfWind")]
public class GustOfWind : ProjectileSpell
{
    [SerializeField]
    private int intensity;

    public override void Impact(Collider2D collision, GameObject projectile)
    {
        if (collision.gameObject.tag == "Mob" || collision.gameObject.tag == "Player")
        {
            //collision.gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0.5f, 1, 1);
            Debug.Log("Woosh");
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(intensity * -Mathf.Sign(projectile.transform.localScale.x),0));
            Debug.Log(-Mathf.Sign(projectile.transform.localScale.x));
        }
    }
}