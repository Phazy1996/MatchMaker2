using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
    private int killCount = 0;
    private bool isAlive = true;
    private Vector2 spawnPos;

    private Animator animator;

    [SerializeField]
    private float playerSpeed;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float gravity = 0.5f;
    [SerializeField]
    private float maxGravity = -40;
    
    private float ySpeed,xSpeed, xSpeedExtra = 0;
    private Vector2 movement = Vector2.zero;
    private bool canFallTroughSmallPlatforms = false;
    private bool isGrounded = false;

    [SerializeField]
    private ParticleSystem runCloud;
    [SerializeField]
    private GameObject impactCloud;

    [SerializeField]
    private int playerId = 1;


    private Vector3 scale;
    private Vector3 leftScale;

    private bool canShoot = true;
    [SerializeField]
    private float shootCooldown = 0.4f;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private Transform gunPoint;

    void Start()
    {
        spawnPos = transform.position;
        animator = GetComponent<Animator>();
        scale = leftScale = transform.localScale;
        leftScale.x *= -1;
    }
    void FixedUpdate()
    {
        transform.Translate(movement);
    }
    void Update () {

        if (transform.position.y < -10f)
            Die();

        if (xSpeed == 0)
        {
            animator.SetBool("moving", false);
            runCloud.enableEmission = false;
        }
        else
        {
            animator.SetBool("moving", true);
            runCloud.enableEmission = true;
        }

        if (xSpeed < 0)
        {
            transform.localScale = leftScale;
        }
        else if (xSpeed> 0)
        {
            transform.localScale = scale;
        }
        
        if(!isGrounded && ySpeed > maxGravity/100)
        {
            ySpeed -= gravity/100;
        }
        if(Input.GetButtonDown("Fire1_P" + playerId.ToString()) && canShoot)
        {
            Shoot();
        }
        if (Input.GetButton("Jump_P" + playerId.ToString()) && isGrounded)
        {
            Jump();
        }
        if(Input.GetButton("Down_P" + playerId.ToString()) &&  !canFallTroughSmallPlatforms)
        {
            StartCoroutine(FallsTroughSmallPPlatforms());
        }

        xSpeed = Input.GetAxis("Horizontal_P" + playerId.ToString()) * playerSpeed / 100;

        movement.x = xSpeed + xSpeedExtra;
        movement.y = ySpeed;
    }
    void Shoot()
    {
        animator.SetBool("shooting", true);
        GameObject temp = Instantiate(bullet, gunPoint.position, Quaternion.identity) as GameObject;
        temp.GetComponent<Projectile>().SetVelocity(transform.localScale.x *20, Random.value);
        temp.GetComponent<Projectile>().PlayerWhoShootYou = this;
        ySpeed += 0.1f;
        xSpeedExtra -= transform.localScale.x/50;
        StartCoroutine(ResetShooting());
        StartCoroutine(ResetShootingAnimation());
    }
    IEnumerator ResetShooting()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
    }
    IEnumerator ResetShootingAnimation()
    {
        
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length*0.5f);
        animator.SetBool("shooting", false);
    }

    IEnumerator FallsTroughSmallPPlatforms()
    {
        canFallTroughSmallPlatforms = true;
        isGrounded = false;
        yield return new WaitForSeconds(1f);
        canFallTroughSmallPlatforms = false;
    }
    void Jump()
    {
        ySpeed = jumpForce / 100;
        Instantiate(impactCloud, transform.position, Quaternion.identity);
        animator.SetBool("grounded", false);
    }
    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == Tags.ground && ySpeed <= 0 && !canFallTroughSmallPlatforms)
        {
            transform.Translate(new Vector2(0, -ySpeed));
            ySpeed = 0;
            xSpeedExtra = 0;
            isGrounded = true;
            animator.SetBool("grounded", true);
        }
    }
    void OnCollisionStay2D(Collision2D coll)
    {
        if(coll.gameObject.tag == Tags.ceiling || coll.gameObject.tag == Tags.ground)
        {
            ySpeed = 0;

        }

        if (coll.gameObject.tag == Tags.ground && ySpeed <= 0 && !isGrounded)
        {
            animator.SetBool("grounded", true);
            isGrounded = true;
            xSpeedExtra = 0;
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
        get { return isAlive; }
    }
    public int KillCount
    {
        set{ killCount = value; }
        get { return killCount; }
    }
    public void Die()
    {
        isAlive = false;
        transform.position = new Vector2(100, 100);
        StartCoroutine(Respawning());
    }
    IEnumerator Respawning()
    {
        yield return new WaitForSeconds(2f);
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        isAlive = true;
        transform.position = spawnPos;
    }
}
