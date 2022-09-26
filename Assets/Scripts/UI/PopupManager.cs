using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverPopup;
    [SerializeField] GameObject pauseMenuPopup;
    [SerializeField] GameObject controlsMenuPopup;
    [SerializeField] GameObject warningPopup;
 
    LevelLoader levelLoader;
 
    // Start is called before the first frame update
    void Start()
    {
        GameOver(false);
        Pause(false);
        ShowControls(false);
        ShowWarning(false);

        levelLoader = FindObjectOfType<LevelLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver(bool isOn)
    {
        gameOverPopup.SetActive(isOn);
    }

    public void Pause(bool isOn)
    {
        pauseMenuPopup.SetActive(isOn);

        if (isOn) { Time.timeScale = 0; }
        else { Time.timeScale = 1; }

    }

    public void MainMenu()
    {
        levelLoader.LoadMainMenu();
    }

    public void ShowControls(bool isOn)
    {
        controlsMenuPopup.SetActive(isOn);
    }

    public void ShowWarning(bool isOn)
    {
        if (warningPopup == null) { return; }

        warningPopup.SetActive(isOn);
    }
}
