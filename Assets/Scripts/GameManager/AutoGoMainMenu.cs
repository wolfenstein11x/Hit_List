using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoGoMainMenu : MonoBehaviour
{
    private void OnEnable()
    {
        FindObjectOfType<LevelLoader>().LoadMainMenu();
    }
}
