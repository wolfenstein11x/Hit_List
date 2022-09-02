using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LethatHazard : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

        if (player != null)
        {
            FindObjectOfType<LivesTracker>().LoseAllLives();
            player.Die(false);
        }
    }

}
