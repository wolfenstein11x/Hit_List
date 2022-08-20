using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadePickup : MonoBehaviour
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
            // can't get grenade if dead
            if (!collision.gameObject.GetComponent<PlayerMovement>().IsAlive()) { return; }

            // add grenade and play sound effect
            gameSession.AddGrenade();
            GetComponent<AudioSource>().Play();

            // make grenade disappear
            GetComponent<CapsuleCollider2D>().enabled = false;
            GetComponentInChildren<SpriteRenderer>().enabled = false;

            // destroy grenade image after delay, so sound effect isn't cut off
            Destroy(gameObject, 0.5f);
        }
    }
}
