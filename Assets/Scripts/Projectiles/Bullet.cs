using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] GameObject explosion;
    [SerializeField] float maxLifetime = 2f;

    Rigidbody2D rigidBody;
    float bulletOrientation;

    void Start()
    {
        bulletOrientation = GetComponentInParent<OrientationTracker>().GetOrientation();
        rigidBody = GetComponent<Rigidbody2D>();

        // de-child bullet from shooter so it does not move with shooter
        transform.parent = null;

        // destroy bullet at pre-determined time so it doesn't fly until it hits something/forever
        Destroy(gameObject, maxLifetime);
    }

    void Update()
    {
        rigidBody.velocity = new Vector2(bulletSpeed * bulletOrientation, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "WallBuffer") { return; }

        // TODO get symmetrical, small simple explosion for bullet
        //GameObject bulletExplosion = Instantiate(explosion, transform.position, transform.rotation);
        //Destroy(bulletExplosion, 0.2f);
        Destroy(gameObject);
    }



}
