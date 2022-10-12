using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
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
    Animator muzzleFlashAnimator;
    CapsuleCollider2D bodyCollider;
    BoxCollider2D feetCollider;
    AudioSource gunShotSound;
    float startingGravityScale;
    bool isShooting = false;
    bool isAlive = true;
    bool takingHit = false;
    bool dialogueMode = false;
    GrenadesTracker grenadesTracker;
    LivesTracker livesTracker;
    float epsilon = 0.01f;
    LayerMask jumpingLayers;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        sizeScale = new Vector2(transform.localScale.x, transform.localScale.y);
        animator = GetComponent<Animator>();
        muzzleFlashAnimator = muzzleFlash.GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        startingGravityScale = rigidBody.gravityScale;
        gunShotSound = GetComponent<AudioSource>();
        grenadesTracker = FindObjectOfType<GrenadesTracker>();
        livesTracker = FindObjectOfType<LivesTracker>();
        jumpingLayers = LayerMask.GetMask("Ground") | LayerMask.GetMask("Climbing") | LayerMask.GetMask("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) { return; }

        Run();
        FlipSprite();
        GrabLadder();
    }

    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }
        if (dialogueMode)
        {
            moveInput = new Vector2(0f, 0f);
            return;
        }

        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) { return; }

        bool onJumpableSurface = (feetCollider.IsTouchingLayers(jumpingLayers));

        // can only jump from off ground or ladder or on enemy
        if (!onJumpableSurface) { return; }

        // can't jump while actively climbing, must be idle on ladder
        if (animator.GetBool("climbing")) { return; }

        if (value.isPressed)
        { 
            animator.SetTrigger("jump");
            rigidBody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void OnFire(InputValue value)
    {
        if (dialogueMode) { return; }

        animator.SetTrigger("shoot");
    }

    public void FireRound()
    {
        gunShotSound.Play();
        muzzleFlashAnimator.SetTrigger("flash");
        GameObject firedBullet = Instantiate(bullet, bulletSpawnPoint.position, bullet.transform.rotation);
        firedBullet.transform.parent = gameObject.transform;
    }

    void OnThrow(InputValue value)
    {
        if (!grenadesTracker.HasGrenade()) { return; }
        if (dialogueMode) { return; }

        animator.SetTrigger("throw");
    }

    void OnPause(InputValue value)
    {
        FindObjectOfType<PopupManager>().Pause(true);
    }

    public void ThrowGrenade()
    {
        GameObject liveGrenade = Instantiate(grenade, grenadeSpawnPoint.position, grenade.transform.rotation);
        liveGrenade.transform.parent = gameObject.transform;
        grenadesTracker.LoseGrenade();
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

        bool playerHasHorizontalSpeed = Mathf.Abs(rigidBody.velocity.x ) > epsilon;

        if (playerHasHorizontalSpeed)
        {
            // flip sprite
            transform.localScale = new Vector2(Mathf.Sign(rigidBody.velocity.x) * sizeScale.x, 1f * sizeScale.y);
        }

    }


    public void Climb()
    {
        Vector2 climbVelocity = new Vector2(rigidBody.velocity.x, moveInput.y * climbSpeed);
        rigidBody.velocity = climbVelocity;
    }

    public bool AttemptingToClimb()
    {
        return (Mathf.Abs(moveInput.y) > Mathf.Epsilon);
    }

    public bool HasVerticalSpeed()
    {
        return Mathf.Abs(rigidBody.velocity.y) > Mathf.Epsilon;
    }

    public void ZeroGravity()
    {
        rigidBody.gravityScale = 0f;
    }

    public void ResetGravity()
    {
        rigidBody.gravityScale = startingGravityScale;
    }

    void GrabLadder()
    {
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            if (moveInput.y > Mathf.Epsilon)
            {
                animator.SetBool("climbing_idle", true);
            }
        }

        else
        {
            animator.SetBool("climbing_idle", false);
            animator.SetBool("climbing", false);
        }
    }

    public bool Landed()
    {
        //LayerMask landingLayers = LayerMask.GetMask("Ground");

        return (rigidBody.IsTouchingLayers(jumpingLayers));
    }

    public void Die(bool doDeathKick=true)
    {
        isAlive = false;
        animator.SetTrigger("die");

        if (doDeathKick) { rigidBody.velocity = deathKick; }

        FindObjectOfType<PopupManager>().GameOver(true);
    }

    public void TakeHit()
    {
        if (!isAlive) { return; }
        livesTracker.LoseLife();
        if (!isAlive) { return; }

        animator.SetTrigger("hit");
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

    public void SetDialogueMode(bool status)
    {
        dialogueMode = status;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Hazards")))
        {
            livesTracker.LoseAllLives();
            Die();
        }

        else if (feetCollider.IsTouchingLayers(LayerMask.GetMask("Hazards")))
        {
            livesTracker.LoseAllLives();
            Die();
        }

        else if (collision.gameObject.tag == "Bullet")
        {
            TakeHit();
            rigidBody.velocity = deathKick;
        }
    }


}
