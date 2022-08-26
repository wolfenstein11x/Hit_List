using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Enemy
{
    [SerializeField] float fireRange = 100f;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject muzzleFlash;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        gunmanTransform = gameObject.transform;
        raycastLayers = LayerMask.GetMask("Player") | LayerMask.GetMask("Ground");
        GunShotSound = GetComponent<AudioSource>();
        MuzzleFlashAnimator = muzzleFlash.GetComponent<Animator>();
        BulletSpawnPoint = bulletSpawnPoint;
        Bullet = bullet;
    }

    public override void TakeHit()
    {
        base.TakeHit();
        animator.SetTrigger("takeHit");
        isDead = true;
    }

    public override bool TargetInSights()
    {
        RaycastHit2D hit = Physics2D.Raycast(gunmanTransform.position, Vector2.left, fireRange, raycastLayers);

        if (!hit) { return false; }

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


}
