using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Enemy
{
    
    void Start()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        muzzleFlashAnimator = muzzleFlash.GetComponent<Animator>();
        gunShotSound = GetComponent<AudioSource>();
        gunmanTransform = gameObject.transform;
        orientationTracker = GetComponent<OrientationTracker>();
        raycastLayers = LayerMask.GetMask("Player") | LayerMask.GetMask("Ground");
        bodyCollider = GetComponent<Collider2D>();
    }



    public override bool TargetInSights()
    {
        RaycastHit2D hit = Physics2D.Raycast(gunmanTransform.position, Vector2.left, fireRange, raycastLayers);

        if (!hit) { return false; }

        if (hit.collider.gameObject.tag == "Player")
        {
            //Debug.DrawRay(gunmanTransform.position, Vector2.left * hit.distance, Color.red);
            return true;
        }
        else
        {
            //Debug.DrawRay(gunmanTransform.position, Vector2.left * hit.distance, Color.green);
            return false;
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) { return; }

        if (collision.gameObject.tag == "Bullet")
        {
            TakeHit();
        }
    }


}
