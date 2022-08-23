using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] GameObject[] lives;
    [SerializeField] GameObject[] grenades;
    [SerializeField] int startingLives = 3;
    [SerializeField] int startingGrenades = 1;

    int lifePointer;
    int grenadePointer;
    int maxLives;

    PlayerMovement player;

    private void Awake()
    {
        // make singleton
        int numGameSessions = FindObjectsOfType<GameSession>().Length;

        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }

        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        InitializeLives();
        InitializeGrenades();
        maxLives = startingLives;
        player = FindObjectOfType<PlayerMovement>();
    }

    private void InitializeLives()
    {
        lifePointer = startingLives - 1;

        for (int i=0; i < lives.Length; i++)
        {
            if (i <= lifePointer)
            {
                lives[i].GetComponent<Animator>().SetTrigger("activate");
            }
        }
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

    public void RestoreLife()
    {
        if (lifePointer == maxLives - 1) { return; }

        lifePointer++;
        lives[lifePointer].GetComponent<Animator>().SetTrigger("activate");
    }

    public void RestoreAllLives()
    {
        if (lifePointer == maxLives - 1) { return; }

        for (int i=0; i < maxLives; i++)
        {
            lives[i].GetComponent<Animator>().SetTrigger("activate");
        }

        lifePointer = maxLives - 1;
    }

    public void AddLife()
    {
        RestoreAllLives();

        if (maxLives == lives.Length) { return; }

        maxLives++;
        lifePointer = maxLives - 1;
        lives[lifePointer].GetComponent<Animator>().SetTrigger("activate");
    }

    public void LoseLife()
    {
        if (lifePointer < 0) { return; }

        if (lifePointer == 0) 
        {
            player.Die();
        }

        lives[lifePointer].GetComponent<Animator>().SetTrigger("deactivate");
        lifePointer--;
    }

    public void LoseAllLives()
    {
        foreach(GameObject life in lives)
        {
            life.GetComponent<Animator>().ResetTrigger("activate");
            life.GetComponent<Animator>().SetTrigger("deactivate");
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
