using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    GameSession gameSession;
    private void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // can't get potion if dead
            if (!collision.gameObject.GetComponent<PlayerMovement>().IsAlive()) { return; }

            // restore health and play sound effect
            gameSession.RestoreLife();
            GetComponent<AudioSource>().Play();

            // make potion disappear
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponentInChildren<SpriteRenderer>().enabled = false;


            // destroy potion after delay, so sound effect isn't cut off
            Destroy(gameObject, 2f);
        }
    }
}
