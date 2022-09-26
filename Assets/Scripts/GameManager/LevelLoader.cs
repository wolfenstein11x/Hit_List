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

    public void ReloadLevel()
    {
        SceneManager.LoadScene(activeSceneIdx);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(activeSceneIdx + 1);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void LoadLevel(int levelIdx)
    {
        SceneManager.LoadScene(levelIdx);
    }

    public void PlayIntro()
    {
        SceneManager.LoadScene("Intro");
    }

    public void PlayOutro()
    {
        SceneManager.LoadScene("Outro");
    }

    

}
