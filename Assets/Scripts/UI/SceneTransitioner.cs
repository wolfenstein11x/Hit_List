using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitioner : MonoBehaviour
{
    [SerializeField] GameObject standardScene;
    [SerializeField] GameObject loadingScene;

    // Start is called before the first frame update
    void Start()
    {
        standardScene.SetActive(true);
        loadingScene.SetActive(false);
    }

    public void LaunchLoadingScene()
    {
        loadingScene.SetActive(true);
        standardScene.SetActive(false);
    }
}
