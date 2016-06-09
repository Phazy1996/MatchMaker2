using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    private Movement playerWhoShootYou;
    private Rigidbody2D rb;
    [SerializeField]
    private GameObject physicObject;
    [SerializeField]
    private GameObject explosionParticles;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void SetVelocity(float _xSpeed, float _ySpeed)
    {
        rb.velocity = new Vector2(_xSpeed,_ySpeed);
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        CreateExplosion();
        if(coll.gameObject.tag == Tags.player)
        {
            coll.gameObject.GetComponent<Movement>().Die();
            playerWhoShootYou.KillCount++;
        }
        Destroy(this.gameObject);
    }
    void CreateExplosion()
    {
        EventManager.ScreenShake();
        Instantiate(explosionParticles, transform.position, Quaternion.identity);
        for (int i = 0; i < 5; i ++)
        {
            GameObject temp =  Instantiate(physicObject, transform.position, Quaternion.identity)as GameObject;
            temp.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-10,10), Random.Range(-10, 10));
        }
    }

    public Movement PlayerWhoShootYou
    {
        set { playerWhoShootYou = value; }
    }
}
