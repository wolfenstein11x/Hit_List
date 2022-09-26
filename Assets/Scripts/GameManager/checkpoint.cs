using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] SavedData savedData;
    [SerializeField] int levelToUnlock;

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            UnlockLevel();
        }
    }
    */

    private void Start()
    {
        UnlockLevel();
    }

    private void UnlockLevel()
    {
        int highestUnlockedLevel = PlayerPrefs.GetInt("highestUnlockedLevel", 1);

        //Debug.Log("levelToUnlock: " + levelToUnlock);
        //Debug.Log("highestUnlockedLevel: " + savedData.highestUnlockedLevel);
        if (levelToUnlock <= highestUnlockedLevel) { return; }

        PlayerPrefs.SetInt("highestUnlockedLevel", levelToUnlock);
        PlayerPrefs.Save();

        //savedData.highestUnlockedLevel = levelToUnlock;
        //savedData.Save();
        //Debug.Log("Unlocked level " + savedData.highestUnlockedLevel);
    }
}
