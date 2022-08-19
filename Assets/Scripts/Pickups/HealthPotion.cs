using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // restore health and play sound effect
            FindObjectOfType<LivesHandler>().RestoreLife();
            GetComponent<AudioSource>().Play();

            // make potion disappear
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponentInChildren<SpriteRenderer>().enabled = false;


            // destroy potion after delay, so sound effect isn't cut off
            Destroy(gameObject, 2f);
        }
    }
}
