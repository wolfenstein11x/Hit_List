using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadePickup : MonoBehaviour
{
    GrenadesTracker grenadesTracker;
    private void Start()
    {
        grenadesTracker = FindObjectOfType<GrenadesTracker>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // can't get grenade if dead
            if (!collision.gameObject.GetComponent<PlayerMovement>().IsAlive()) { return; }

            // can't get grenade if grenade slots full
            if (grenadesTracker.GrenadesMaxed()) { return; }

            // add grenade and play sound effect
            grenadesTracker.AddGrenade();
            GetComponent<AudioSource>().Play();

            // make grenade disappear
            GetComponent<CapsuleCollider2D>().enabled = false;
            GetComponentInChildren<SpriteRenderer>().enabled = false;

            // destroy grenade image after delay, so sound effect isn't cut off
            Destroy(gameObject, 0.5f);
        }
    }
}
