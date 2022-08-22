using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Transform BulletSpawnPoint;
    protected GameObject Bullet;
    protected GameObject MuzzleFlash;
    protected Animator MuzzleFlashAnimator;
    protected AudioSource GunShotSound;
    protected float FireRange;
    
    protected OrientationTracker orientationTracker;
    protected Transform gunmanTransform;

    protected bool isDead = false;
    protected LayerMask raycastLayers;

    private float flipCorrection = 0.02f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void TakeHit()
    {
        if (isDead) { return; }
    }

    public virtual void Walk(Rigidbody2D body, float walkSpeed)
    {
        Vector2 walkVelocity = new Vector2(walkSpeed, body.velocity.y);
        body.velocity = walkVelocity;
    }

    public virtual void ReverseDirection()
    {
        
    }

    public virtual float GetRange()
    {
        return 0;
    }

    public virtual float GetWalkSpeed()
    {
        return 0;
    }

    public bool IsDead()
    {
        return isDead;
    }

    public bool TargetInSights()
    {
        float orientation = orientationTracker.GetOrientation();
        
        RaycastHit2D hit = Physics2D.Raycast(gunmanTransform.position, Vector2.right * new Vector2(orientation, 0f), FireRange, raycastLayers);
        
        if (hit.collider.gameObject.tag == "Player")
        {
            return true;
            //Debug.DrawRay(gunmanTransform.position, Vector2.right * hit.distance * new Vector2(orientation, 0f), Color.red);
        }
        else
        {
            return false;
            //Debug.DrawRay(gunmanTransform.position, Vector2.right * hit.distance * new Vector2(orientation, 0f), Color.green);
        }

    }

    public void RemoveFromPlay()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) { return; }

        if (collision.gameObject.tag == "Bullet")
        {
            TakeHit();
        }
    }

    public void FlipSprite(Rigidbody2D body)
    {
        float xScale = body.transform.localScale.x;
        float yScale = body.transform.localScale.y;

        // correct for the sprite flip point not being at its center
        float xPosCorrected = body.transform.position.x + (flipCorrection * Mathf.Sign(body.velocity.x));
        body.transform.position = new Vector2(xPosCorrected, transform.position.y);
        
        // flip sprite
        body.transform.localScale = new Vector2(-1.0f * xScale, yScale);
    }

    
    public void FireRound()
    {
        GunShotSound.Play();
        MuzzleFlashAnimator.SetTrigger("flash");
        GameObject firedBullet = Instantiate(Bullet, BulletSpawnPoint.position, Bullet.transform.rotation);
        firedBullet.transform.parent = gunmanTransform;
    }
}
