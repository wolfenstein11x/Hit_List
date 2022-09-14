using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteAssassin : Enemy
{
    [SerializeField] GameObject grenade;
    [SerializeField] Transform grenadeSpawnPoint;
    [SerializeField] float sightRange = 100;

    public bool grenadeThrower = true;

    PlayerMovement target;

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
        target = FindObjectOfType<PlayerMovement>();
    }

    public void ThrowGrenade()
    {
        GameObject liveGrenade = Instantiate(grenade, grenadeSpawnPoint.position, grenade.transform.rotation);
        liveGrenade.transform.parent = gameObject.transform;
    }

    // 0 = not spotted, 1 = spotted in front, 2 = spotted behind
    public bool TargetSpotted(int direction=1)
    {
        float orientation = orientationTracker.GetOrientation();

        RaycastHit2D hit = Physics2D.Raycast(gunmanTransform.position, Vector2.right * new Vector2(direction*orientation, 0f), sightRange, raycastLayers);

        //Debug.DrawRay(gunmanTransform.position, Vector2.right * hit.distance * new Vector2(direction*orientation, 0f), Color.red);

        if (!hit) { return false; }

        if (hit.collider.gameObject.tag == "Player")
        {
            //Debug.DrawRay(gunmanTransform.position, Vector2.right * hit.distance * new Vector2(orientation, 0f), Color.red);
            return true;
        }

        else
        {
            return false;
        }

    }


    
}
