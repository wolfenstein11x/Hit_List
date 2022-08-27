using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolGuard : Enemy
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
    }

    public override void TakeHit()
    {
        base.TakeHit();
        animator.SetTrigger("takeHit");
        isDead = true;
    }

}
