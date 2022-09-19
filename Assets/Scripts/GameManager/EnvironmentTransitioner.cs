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
        // Don't want to hear left over grenade exploding outside, ruin the indoor music
        Grenade[] grenades = FindObjectsOfType<Grenade>();
        foreach(Grenade grenade in grenades)
        {
            Destroy(grenade);
        }

        insideEnvironment.SetActive(true);
        outsideEnvironment.SetActive(false);

        outsideCamera.SetActive(false);
        insideCamera.SetActive(true);
    }
}
