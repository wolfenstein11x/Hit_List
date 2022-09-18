using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 4f;
    public Rigidbody2D body;
    public float fireRange = 10f;
    public Transform bulletSpawnPoint;
    public GameObject muzzleFlash;
    public bool grenadeImmune = false;

    [SerializeField] GameObject bullet;

    protected Animator animator;
    protected Animator muzzleFlashAnimator;
    protected AudioSource gunShotSound;
    protected OrientationTracker orientationTracker;
    protected Transform gunmanTransform;
    protected Collider2D bodyCollider;

    protected bool isDead = false;
    protected LayerMask raycastLayers;

    

    void Start()
    {

    }

    void Update()
    {
        
    }

    public virtual void TakeHit()
    {
        if (isDead) { return; }
        animator.SetTrigger("takeHit");
        isDead = true;
    }

    public virtual void Move()
    {
        Vector2 moveVelocity = new Vector2(moveSpeed, body.velocity.y);
        body.velocity = moveVelocity;
    }

    public virtual void ReverseDirection()
    {
        moveSpeed *= -1.0f;
    }

    public bool IsDead()
    {
        return isDead;
    }

    public virtual bool TargetInSights()
    {
        float orientation = orientationTracker.GetOrientation();
        
        RaycastHit2D hit = Physics2D.Raycast(gunmanTransform.position, Vector2.right * new Vector2(orientation, 0f), fireRange, raycastLayers);
        
        if (!hit) { return false; }

        if (hit.collider.gameObject.tag == "Player")
        {
            //Debug.DrawRay(gunmanTransform.position, Vector2.right * hit.distance * new Vector2(orientation, 0f), Color.red);
            return true;
        }
        else
        {
            //Debug.DrawRay(gunmanTransform.position, Vector2.right * hit.distance * new Vector2(orientation, 0f), Color.green);
            return false;
        }

    }

    public void RemoveFromPlay()
    {
        Destroy(gameObject);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) { return; }

        if (collision.gameObject.tag == "Bullet")
        {
            TakeHit();
        }
    }

    public void FlipSprite()
    {
        float xScale = body.transform.localScale.x;
        float yScale = body.transform.localScale.y;

        // correct for the sprite flip point not being at its center
        //float xPosCorrected = body.transform.position.x + (flipCorrection * Mathf.Sign(body.velocity.x));
        //body.transform.position = new Vector2(xPosCorrected, transform.position.y);

        // flip sprite
        body.transform.localScale = new Vector2(-1.0f * xScale, yScale);
    }

    
    public void FireRound()
    {
        gunShotSound.Play();
        muzzleFlashAnimator.SetTrigger("flash");
        GameObject firedBullet = Instantiate(bullet, bulletSpawnPoint.position, bullet.transform.rotation);
        firedBullet.transform.parent = gunmanTransform;
    }

    public bool TargetDead()
    {
        return !FindObjectOfType<PlayerMovement>().IsAlive();
    }

    public void HoldPosition()
    {
        body.velocity = new Vector2(0f, 0f);
    }

    public virtual void RemoveColliders()
    {
        
    }
}
