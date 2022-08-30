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
        for(int i=0; i < levels.Length; i++)
        {
            if (i < savedData.highestUnlockedLevel)
            {
                levels[i].Unlock();
            }

            else
            {
                levels[i].Lock();
            }
        }
    }

}
