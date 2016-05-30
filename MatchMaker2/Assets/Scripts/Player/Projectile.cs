using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
    private Rigidbody2D rb;
    [SerializeField]
    private GameObject explosionParticle;
    [SerializeField]
    private float xSpeed = 5;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        SetVelocity(xSpeed, 0);
    }
    public void SetVelocity(float _xSpeed, float _ySpeed)
    {
        rb.velocity = new Vector2(_xSpeed,_ySpeed);
    }


    void OnCollisionEnter2D(Collision2D coll)
    {
        CreateExplosion();
        Destroy(this.gameObject);
    }
    void CreateExplosion()
    {
        for (int i = 0; i < 10; i ++)
        {
            GameObject temp =  Instantiate(explosionParticle, transform.position, Quaternion.identity)as GameObject;
            temp.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-10,10), Random.Range(-10, 10));
        }
    }
}
