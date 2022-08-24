using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GrenadesTracker : MonoBehaviour
{
    [SerializeField] GameObject[] grenades;
    [SerializeField] int startingGrenades = 1;
    
    int grenadePointer;


    // Start is called before the first frame update
    void Start()
    {
        InitializeGrenades();
    }

    
    private void InitializeGrenades()
    {
        grenadePointer = startingGrenades - 1;

        for (int i = 0; i < grenades.Length; i++)
        {
            if (i > grenadePointer)
            {
                grenades[i].GetComponentInChildren<Image>().enabled = false;
            }
        }
    }

    public void AddGrenade()
    {
        // do nothing if grenade slots are full
        if (grenadePointer >= grenades.Length - 1) { return; }

        grenadePointer++;
        grenades[grenadePointer].GetComponent<Image>().enabled = true;

    }

    public void LoseGrenade()
    {
        // do nothing if grenade slots are empty
        if (grenadePointer < 0) { return; }

        grenades[grenadePointer].GetComponent<Image>().enabled = false;
        grenadePointer--;
    }

    public bool HasGrenade()
    {
        return grenadePointer >= 0;
    }

    public bool GrenadesMaxed()
    {
        return grenadePointer >= grenades.Length - 1;
    }
}
