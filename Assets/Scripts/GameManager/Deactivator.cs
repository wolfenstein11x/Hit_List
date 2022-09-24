using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivator : MonoBehaviour
{
    [SerializeField] GameObject[] objectsToDeactivate;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            foreach(GameObject thing in objectsToDeactivate)
            {
                if (thing == null) { continue; }
                Destroy(thing);
            }
        }
    }
}
