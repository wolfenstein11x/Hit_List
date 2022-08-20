using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] GameObject[] lives;
    [SerializeField] GameObject[] grenades;
    [SerializeField] int startingLives = 3;
    [SerializeField] int startingGrenades = 1;
    [SerializeField] float loseLifeAnimationTime = 1f;
    int lifePointer;
    int grenadePointer;
    int maxLives;

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
    }

    private void InitializeLives()
    {
        lifePointer = startingLives - 1;

        for (int i=0; i < lives.Length; i++)
        {
            if (i > lifePointer)
            {
                lives[i].GetComponent<Animator>().SetTrigger("deactivated");
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
                grenades[i].SetActive(false);
            }
        }
    }

    public void RestoreLife()
    {
        if (lifePointer == maxLives - 1) { return; }

        lifePointer++;
        lives[lifePointer].GetComponent<Animator>().SetTrigger("lifeRestored");
    }

    public void RestoreAllLives()
    {
        if (lifePointer == maxLives - 1) { return; }

        for (int i=0; i < maxLives; i++)
        {
            lives[i].GetComponent<Animator>().SetTrigger("lifeRestored");
        }

        lifePointer = maxLives - 1;
    }

    public void AddLife()
    {
        RestoreAllLives();

        if (maxLives == lives.Length) { return; }

        maxLives++;
        lifePointer = maxLives - 1;
        lives[lifePointer].GetComponent<Animator>().SetTrigger("lifeRestored");
    }

    public void LoseLife()
    {
        if (lifePointer == 0) { return; }

        lives[lifePointer].GetComponent<Animator>().SetTrigger("lifeLost");
        lifePointer--;
    }

    public void LoseAllLives()
    {
        foreach(GameObject life in lives)
        {
            life.GetComponent<Animator>().SetTrigger("lifeLost");
        }
    }

    
    
}
