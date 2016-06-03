using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    private bool isAlive = true;

    private Animator animator;
    [SerializeField]
    private Transform gunPoint;
    [SerializeField]
    private float playerSpeed;
    [SerializeField]
    private float jumpForce;
    private Vector2 movement = Vector2.zero;
    private bool isGrounded = false;
    private float ySpeed = 0;
    [SerializeField]
    private float gravity = 0.5f;
    private Vector3 scale;
    private Vector3 leftScale;
    [SerializeField]
    private float maxGravity = -40;
    [SerializeField]
    private int playerId = 1;

    [SerializeField]
    private GameObject bullet;
    void Start()
    {
        animator = GetComponent<Animator>();
        scale = leftScale = transform.localScale;
        leftScale.x *= -1;
    }
    void FixedUpdate()
    {
        if(Input.GetAxisRaw("Horizontal_P" + playerId.ToString()) == 0)
        {
            animator.SetBool("moving", false);
        }
        else
        {
            animator.SetBool("moving", true);
        }
        movement = new Vector2(Input.GetAxis("Horizontal_P"+playerId.ToString()) * playerSpeed/100, ySpeed);
        transform.Translate(movement);
    }
    void Update () {

        if(movement.x < 0)
        {
            transform.localScale = leftScale;
        }
        else if (movement.x > 0)
        {
            transform.localScale = scale;
        }
        
        if(!isGrounded && ySpeed > maxGravity/100)
        {
            ySpeed -= gravity/100;
        }
        if(Input.GetButtonDown("Fire1_P" + playerId.ToString()))
        {
            Shoot();
        }
        if (Input.GetButton("Jump_P" + playerId.ToString()) && isGrounded)
        {
            Jump();
        }
    }
    void Shoot()
    {
        animator.SetBool("shooting", true);
        GameObject temp = Instantiate(bullet, gunPoint.position, Quaternion.identity) as GameObject;
        temp.GetComponent<Projectile>().SetVelocity(transform.localScale.x *20, 0);
        StartCoroutine(Shooting());
    }
    IEnumerator Shooting()
    {
        yield return new WaitForSeconds(0.4f);
        animator.SetBool("shooting", false);
    }
    void Jump()
    {
        ySpeed = jumpForce / 100;
    }
    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == Tags.ground && ySpeed <= 0)
        {
            //transform.Translate(new Vector2(0, -ySpeed));
            ySpeed = 0;
            isGrounded = true;
        }
    }
    void OnCollisionStay2D(Collision2D coll)
    {
        if(coll.gameObject.tag == Tags.ceiling || coll.gameObject.tag == Tags.ground)
        {
            ySpeed = 0;
        }

        if (coll.gameObject.tag == Tags.ground && ySpeed <= 0)
        {
            isGrounded = true;
        }
    }
    void OnTriggerExit2D(Collider2D coll)
    {
        isGrounded = false;
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        isGrounded = false;
    }

    public float YSpeed
    {
        set { ySpeed = value; }
    }
    public bool IsAlive
    {
        set { isAlive = value; }
        get { return isAlive; }
    }
}
