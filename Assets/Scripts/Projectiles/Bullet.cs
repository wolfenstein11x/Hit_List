using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] GameObject explosion;

    Rigidbody2D rigidBody;
    float bulletOrientation;

    void Start()
    {
        // TODO fix problem with bullet changing direction midair
        bulletOrientation = GetComponentInParent<OrientationTracker>().GetOrientation();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rigidBody.velocity = new Vector2(bulletSpeed * bulletOrientation, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // TODO get symmetrical, small simple explosion for bullet
        //GameObject bulletExplosion = Instantiate(explosion, transform.position, transform.rotation);
        //Destroy(bulletExplosion, 0.2f);
        Destroy(gameObject);
    }


}
