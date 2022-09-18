using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    [SerializeField] float bounceVelocityY = 5f;

    AudioSource bounceSound;

    private void Start()
    {
        bounceSound = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D body = collision.gameObject.GetComponent<Rigidbody2D>();

        if (body != null)
        {
            AddBounceForce(body);
            bounceSound.Play();
        }
    }

    private void AddBounceForce(Rigidbody2D body)
    {
        Vector2 bounce = new Vector2(body.velocity.x, bounceVelocityY);

        body.velocity = bounce;
    }
}
