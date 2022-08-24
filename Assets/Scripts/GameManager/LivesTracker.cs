using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesTracker : MonoBehaviour
{
    [SerializeField] GameObject[] lives;
    [SerializeField] int startingLives = 3;

    PlayerMovement player;
    int lifePointer;
    int maxLives;


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        InitializeLives();
        maxLives = startingLives;
    }

    private void InitializeLives()
    {
        lifePointer = startingLives - 1;

        for (int i = 0; i < lives.Length; i++)
        {
            if (i <= lifePointer)
            {
                lives[i].GetComponent<Animator>().SetTrigger("activate");
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

        for (int i = 0; i < maxLives; i++)
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

        lives[lifePointer].GetComponent<Animator>().ResetTrigger("activate");
        lives[lifePointer].GetComponent<Animator>().SetTrigger("deactivate");
        lifePointer--;
    }

    public void LoseAllLives()
    {
        foreach (GameObject life in lives)
        {
            life.GetComponent<Animator>().ResetTrigger("activate");
            life.GetComponent<Animator>().SetTrigger("deactivate");
        }
    }
}
