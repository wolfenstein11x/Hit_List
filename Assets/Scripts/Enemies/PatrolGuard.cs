using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolGuard : Enemy
{
    [SerializeField] float walkSpeed = 4f;
    [SerializeField] float fireRange = 100f;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject muzzleFlash;

    Animator animator;
    CapsuleCollider2D bodyCollider;
    Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        body = GetComponent<Rigidbody2D>();

        BulletSpawnPoint = bulletSpawnPoint;
        Bullet = bullet;
        MuzzleFlash = muzzleFlash;
        GunShotSound = GetComponent<AudioSource>();
        gunmanTransform = gameObject.transform;
    }

    public override void TakeHit()
    {
        base.TakeHit();
        animator.SetTrigger("takeHit");
        isDead = true;
    }

    public override void ReverseDirection()
    {
        base.ReverseDirection();
        walkSpeed *= -1.0f;
    }


    public override float GetRange()
    {
        return fireRange;
    }

    public override float GetWalkSpeed()
    {
        return walkSpeed;
    }

    

}
