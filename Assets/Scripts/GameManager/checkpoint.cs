using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] int levelToUnlock;

    private void Start()
    {
        UnlockLevel();
    }

    private void UnlockLevel()
    {
        int highestUnlockedLevel = PlayerPrefs.GetInt("highestUnlockedLevel", 1);

        //Debug.Log("levelToUnlock: " + levelToUnlock);
        if (levelToUnlock <= highestUnlockedLevel) { return; }

        PlayerPrefs.SetInt("highestUnlockedLevel", levelToUnlock);
        PlayerPrefs.Save();

    }
}
