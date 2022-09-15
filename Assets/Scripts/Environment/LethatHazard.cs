using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LethatHazard : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();

        if (player != null)
        {
            FindObjectOfType<LivesTracker>().LoseAllLives();
            player.Die(false);
        }

        if (enemy != null)
        {
            enemy.TakeHit();
        }
    }

}
