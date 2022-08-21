using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Transform BulletSpawnPoint;
    protected GameObject Bullet;
    protected GameObject MuzzleFlash;
    protected AudioSource GunShotSound;
    protected Transform gunmanTransform;

    protected bool isDead = false;
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

    public void ShootRaycasts(float range)
    {
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * range);
        Ray ray = new Ray(transform.position, Vector2.right);
        Debug.DrawRay(ray.origin, ray.direction * range, Color.blue);
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

        // also have to flip muzzle flash
        MuzzleFlash.transform.localScale = new Vector2(Mathf.Sign(body.velocity.x), 1f);
    }

    public void FireRound()
    {
        GunShotSound.Play();
        GameObject muzzleFlashInstance = Instantiate(MuzzleFlash, BulletSpawnPoint.position, MuzzleFlash.transform.rotation);
        Destroy(muzzleFlashInstance, 0.2f);
        GameObject firedBullet = Instantiate(Bullet, BulletSpawnPoint.position, Bullet.transform.rotation);
        firedBullet.transform.parent = gunmanTransform;
    }
}