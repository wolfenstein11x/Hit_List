using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] GameObject levelButton;
    [SerializeField] GameObject lockButton;

    public void Unlock()
    {
        levelButton.SetActive(true);
        lockButton.SetActive(false);
    }

    public void Lock()
    {
        levelButton.SetActive(false);
        lockButton.SetActive(true);
    }
}
