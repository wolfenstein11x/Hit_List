using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] float flipCorrection = 1f;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject muzzleFlash;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] GameObject grenade;
    [SerializeField] Transform grenadeSpawnPoint;
    [SerializeField] Vector2 deathKick = new Vector2(10f, 10f);

    Vector2 moveInput;
    Rigidbody2D rigidBody;
    Vector2 sizeScale;
    Animator animator;
    CapsuleCollider2D bodyCollider;
    float startingGravityScale;
    AudioSource gunShotSound;
    bool isShooting = false;
    bool isAlive = true;
    bool takingHit = false;
    GameSession gameSession;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        sizeScale = new Vector2(transform.localScale.x, transform.localScale.y);
        animator = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        startingGravityScale = rigidBody.gravityScale;
        gunShotSound = GetComponent<AudioSource>();
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) { return; }

        Run();
        FlipSprite();
        ClimbLadder();
    }

    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }

        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) { return; }

        bool onGround = bodyCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
        //bool onLadder = playerCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"));

        // can only jump from off ground 
        if (!onGround) { return; }

        if (value.isPressed)
        { 
            animator.SetTrigger("jump");
            rigidBody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void OnFire(InputValue value)
    {
        animator.SetTrigger("shoot");
    }

    public void FireRound()
    {
        gunShotSound.Play();
        GameObject muzzleFlashInstance = Instantiate(muzzleFlash, bulletSpawnPoint.position, muzzleFlash.transform.rotation);
        Destroy(muzzleFlashInstance, 0.2f);
        GameObject firedBullet = Instantiate(bullet, bulletSpawnPoint.position, bullet.transform.rotation);
        firedBullet.transform.parent = gameObject.transform;
    }

    void OnThrow(InputValue value)
    {
        if (!gameSession.HasGrenade()) { return; }

        animator.SetTrigger("throw");
    }

    public void ThrowGrenade()
    {
        GameObject liveGrenade = Instantiate(grenade, grenadeSpawnPoint.position, grenade.transform.rotation);
        liveGrenade.transform.parent = gameObject.transform;
        gameSession.LoseGrenade();
    }

    void Run()
    {
        // can't run while shooting
        if (isShooting) { return; }

        // can't run while getting blasted up into the air
        if (takingHit) { return; }

        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, rigidBody.velocity.y);
        rigidBody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(rigidBody.velocity.x) > Mathf.Epsilon;
        animator.SetBool("running", playerHasHorizontalSpeed);
    }

    void FlipSprite()
    {
        if (takingHit) { return; }

        bool playerHasHorizontalSpeed = Mathf.Abs(rigidBody.velocity.x ) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            // correct for the sprite flip point not being at its center
            float xPosCorrected = transform.position.x + (flipCorrection * Mathf.Sign(rigidBody.velocity.x));  
            transform.position = new Vector2(xPosCorrected, transform.position.y);

            // flip sprite
            transform.localScale = new Vector2(Mathf.Sign(rigidBody.velocity.x) * sizeScale.x, 1f * sizeScale.y);

            // also have to flip muzzle flash
            muzzleFlash.transform.localScale = new Vector2(Mathf.Sign(rigidBody.velocity.x), 1f);

        }

    }

    void ClimbLadder()
    {
        // only climb if player is touching a ladder
        if (!bodyCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))) 
        {
            rigidBody.gravityScale = startingGravityScale;
            animator.SetBool("climbing", false);
            animator.SetBool("climbing_idle", false);
            return; 
        }

        // handle upward motion
        Vector2 climbVelocity = new Vector2(rigidBody.velocity.x, moveInput.y * climbSpeed);
        rigidBody.velocity = climbVelocity;
        rigidBody.gravityScale = 0f;

        // play climbing animation if moving up, play climbing idle animation if not moving
        bool playerHasVerticalalSpeed = Mathf.Abs(rigidBody.velocity.y) > Mathf.Epsilon;
        animator.SetBool("climbing", playerHasVerticalalSpeed);
        animator.SetBool("climbing_idle", !playerHasVerticalalSpeed);
    }

    public void Die()
    {
        isAlive = false;
        animator.SetTrigger("die");
        rigidBody.velocity = deathKick;

        gameSession.LoseAllLives();
    }

    public void TakeHit()
    {
        animator.SetTrigger("hit");
        gameSession.LoseLife();
    }

    public bool IsAlive()
    {
        return isAlive;
    }

    public void SetIsShooting(bool status)
    {
        isShooting = status;
    }

    public void SetTakingHit(bool status)
    {
        takingHit = status;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Hazards")))
        {
            Die();
        }

        else if (collision.gameObject.tag == "Bullet")
        {
            TakeHit();
            rigidBody.velocity = deathKick;
        }
    }


}
