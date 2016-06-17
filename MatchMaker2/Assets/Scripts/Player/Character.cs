using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
    private int killCount = 0;
    private bool isAlive = false;
    private bool inControl = false;
    [SerializeField]
    private SpawnPointData spawnPointData;

    private Animator animator;

    [SerializeField]
    private float playerSpeed;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float gravity = 0.5f;
    [SerializeField]
    private float maxGravity = -40;

    private float ySpeed, xSpeed, xSpeedExtra = 0;
    private Vector2 movement;
    private bool canFallTroughSmallPlatforms = false;
    private bool isGrounded = false;


    [SerializeField]
    private GameObject spawnCloud;
    [SerializeField]
    private ParticleSystem runCloud;
    [SerializeField]
    private GameObject impactCloud;
    [SerializeField]
    private GameObject deathCloud;
    [SerializeField]
    private GameObject gunFlashCloud;

    private Vector3 scale;
    private Vector3 leftScale;

    private bool canShoot = true;
    [SerializeField]
    private float shootCooldown = 0.4f;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private Transform gunPoint;
    [SerializeField]
    private AudioSource gunSound;
    void Start()
    {
        animator = GetComponent<Animator>();
        scale = leftScale = transform.localScale;
        leftScale.x *= -1;
        gravity = gravity / 100;
        maxGravity = maxGravity / 100;
        playerSpeed = playerSpeed / 100;
        jumpForce = jumpForce / 100;
    }
    void FixedUpdate()
    {
        transform.Translate(movement);
    }
    void Update()
    {

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
            if(isGrounded)
                runCloud.enableEmission = true;
            else
                runCloud.enableEmission = false;
        }

        if (xSpeed < 0)
        {
            transform.localScale = leftScale;
        }
        else if (xSpeed > 0)
        {
            transform.localScale = scale;
        }

        if (!isGrounded && ySpeed > maxGravity)
        {
            ySpeed -= gravity;
        }

        movement.x = (xSpeed*playerSpeed);
        movement.y = ySpeed;
    }
    public void Shoot()
    {
        if (canShoot)
        {
            gunSound.Play();
            animator.SetBool("shooting", true);
            Instantiate(gunFlashCloud, gunPoint.position,gunFlashCloud.transform.rotation);
            GameObject temp = Instantiate(bullet, gunPoint.position, Quaternion.identity) as GameObject;
            temp.GetComponent<Projectile>().SetVelocity(transform.localScale.x * 20, Random.value);
            temp.GetComponent<Projectile>().PlayerWhoShootYou = this;
            ySpeed += 0.1f;
            temp.transform.localScale = new Vector2(temp.transform.localScale.x *transform.localScale.x, temp.transform.localScale.y);
            StartCoroutine(ResetShooting());
        }
    }
    IEnumerator ResetShooting()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
        animator.SetBool("shooting", false);
    }

    public void Down()
    {
        if(!canFallTroughSmallPlatforms)
        {
            StartCoroutine(FallsTroughSmallPPlatforms());
        }
    }
    IEnumerator FallsTroughSmallPPlatforms()
    {
        canFallTroughSmallPlatforms = true;
        isGrounded = false;
        yield return new WaitForSeconds(1f);
        canFallTroughSmallPlatforms = false;
    }
    public void Jump()
    {
        if(isGrounded)
        {
            ySpeed = jumpForce;
            Instantiate(impactCloud, transform.position, Quaternion.identity);
            animator.SetBool("grounded", false);
        }
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
        if (coll.gameObject.tag == Tags.ceiling || coll.gameObject.tag == Tags.ground)
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
    public float XSpeed
    {
        get { return xSpeed; }
        set { xSpeed = value; }
    }
    public bool IsAlive
    {
        get { return isAlive; }
        set { isAlive = value; }
    }
    public bool InControl
    {
        get { return inControl; }
        set { inControl = value; }
    }
    public int KillCount
    {
        set { killCount = value; }
        get { return killCount; }
    }

    public void Die()
    {
        isAlive = false;
        Instantiate(deathCloud, transform.position, Quaternion.identity);
        transform.position = new Vector2(100, 100);
        StartCoroutine(WaitForRespawning());
    }
    public IEnumerator WaitForRespawning()
    {
        yield return new WaitForSeconds(2f);
        if (InControl)
            Respawn();
    }
    public void Respawn()
    {
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        isAlive = true;
        transform.position = spawnPointData.SpawnPoints[Random.Range(0,spawnPointData.SpawnPoints.Length)].transform.position;
        Instantiate(spawnCloud, transform.position, Quaternion.identity);
    }
}
