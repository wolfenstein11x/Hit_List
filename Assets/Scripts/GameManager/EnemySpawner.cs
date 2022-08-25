using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Enemy[] enemies;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] bool[] facingLeft;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i=0; i < enemies.Length; i++)
        {
            Enemy enemy = Instantiate(enemies[i], spawnPoints[i].position, transform.rotation);
        }

        Destroy(gameObject);
    }
}
