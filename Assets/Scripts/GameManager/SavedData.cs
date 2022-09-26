using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SavedData", menuName = "ScriptableObjects/SavedData")]
public class SavedData : ScriptableObject
{
    public int highestUnlockedLevel = 1;

    public void Save()
    {
        PlayerPrefs.SetInt("highestUnlockedLevel", highestUnlockedLevel);
        PlayerPrefs.Save();
        //Debug.Log("Saved progress, unlocked level " + highestUnlockedLevel);
    }
    
}
