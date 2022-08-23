using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    int activeSceneIdx;

    // Start is called before the first frame update
    void Start()
    {
        activeSceneIdx = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(activeSceneIdx);
    }
}
