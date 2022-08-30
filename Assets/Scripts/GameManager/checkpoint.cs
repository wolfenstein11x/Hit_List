using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
    [SerializeField] SavedData savedData;
    [SerializeField] int levelToUnlock;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            UnlockLevel();
        }
    }

    private void UnlockLevel()
    {
        if (levelToUnlock <= savedData.highestUnlockedLevel) { return; }

        savedData.highestUnlockedLevel = levelToUnlock;
    }
}
