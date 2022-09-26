using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMenu : MonoBehaviour
{
    [SerializeField] Level[] levels;
    [SerializeField] SavedData savedData;

    // Start is called before the first frame update
    void Start()
    {
        LoadUnlockedLevels();
    }

    public void LoadUnlockedLevels()
    {
        int highestUnlockedLevel = PlayerPrefs.GetInt("highestUnlockedLevel", 1);

        for (int i = 0; i < levels.Length; i++)
        {
            if (i < highestUnlockedLevel)
            {
                levels[i].Unlock();
            }

            else
            {
                levels[i].Lock();
            }
        }
    }

    public void Reset()
    {
        PlayerPrefs.SetInt("highestUnlockedLevel", 1);
        LoadUnlockedLevels();
    }

}
