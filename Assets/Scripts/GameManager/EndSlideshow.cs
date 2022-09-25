using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSlideshow : MonoBehaviour
{
    [SerializeField] GameObject loadingScreen;

    LevelLoader levelLoader;

    // Start is called before the first frame update
    void Start()
    {
        levelLoader = FindObjectOfType<LevelLoader>();

        loadingScreen.gameObject.SetActive(true);
        levelLoader.LoadLevel(1);

    }

}
