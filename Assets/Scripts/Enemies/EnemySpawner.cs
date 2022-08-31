using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Enemy[] enemies;
    [SerializeField] Transform[] spawnPoints;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                Enemy enemy = Instantiate(enemies[i], spawnPoints[i].position, transform.rotation);
            }

            Destroy(gameObject);
        }
    }

}
