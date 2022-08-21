using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBuffer : MonoBehaviour
{
    Enemy enemy;
    Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
        body = GetComponentInParent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (enemy.IsDead()) { return; }

        if (collision.gameObject.tag != "TurnaroundPoint") { return; }

        enemy.ReverseDirection();
        enemy.FlipSprite(body);
    }
}
