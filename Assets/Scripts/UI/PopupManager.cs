using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverPopup;
    [SerializeField] GameObject levelCompletePopup;
 
    // Start is called before the first frame update
    void Start()
    {
        GameOver(false);
        LevelComplete(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver(bool isOn)
    {
        gameOverPopup.SetActive(isOn);
    }

    public void LevelComplete(bool isOn)
    {
        levelCompletePopup.SetActive(isOn);
    }
}
