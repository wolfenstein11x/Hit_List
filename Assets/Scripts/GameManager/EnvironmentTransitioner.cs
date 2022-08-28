using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentTransitioner : MonoBehaviour
{
    [SerializeField] GameObject outsideEnvironment;
    [SerializeField] GameObject insideEnvironment;
    [SerializeField] GameObject outsideCamera;
    [SerializeField] GameObject insideCamera;
    

    void Start()
    {
        outsideEnvironment.SetActive(true);
        insideEnvironment.SetActive(false);
    }
    
    public void GoInside()
    {
        insideEnvironment.SetActive(true);
        outsideEnvironment.SetActive(false);

        outsideCamera.SetActive(false);
        insideCamera.SetActive(true);
    }
}
