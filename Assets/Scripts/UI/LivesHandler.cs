using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesHandler : MonoBehaviour
{
    [SerializeField] GameObject lifeIcon;
    [SerializeField] int startingLives;
    [SerializeField] float spacing = 20;
    [SerializeField] float lifeAnimationTime = 1.4f;

    List<GameObject> currentLives = new List<GameObject>();
    int totalLives = 0;

    // Start is called before the first frame update
    void Start()
    {
        InitializeLives();
        totalLives = startingLives;
        LoseLife();
    }

    private void InitializeLives()
    {
        for (int i=0; i < startingLives; i++)
        {
            AddLife();
        }
    }

    public void AddLife()
    {
        float xPos = transform.position.x + currentLives.Count * spacing;
        Vector3 pos = new Vector3(xPos, transform.position.y, transform.position.z);

        GameObject life = Instantiate(lifeIcon, pos, transform.rotation);
        
        // make sprite child of canvas, or won't be seen
        life.transform.SetParent(transform);

        // fix scale of sprite due to issue with parent/child/world scaling
        life.transform.localScale = new Vector3(1f, 1f, 1f);

        // track life in list
        currentLives.Add(life);
    }

    public void LoseLife()
    {
        GameObject currentLife = currentLives[currentLives.Count - 1];

        currentLife.GetComponent<Animator>().SetTrigger("lifeLost");

        currentLives.RemoveAt(currentLives.Count - 1);

        Destroy(currentLife, lifeAnimationTime);
    }

    public void LoseAllLives()
    {
        foreach(GameObject life in currentLives)
        {
            life.GetComponent<Animator>().SetTrigger("lifeLost");
            Destroy(life, lifeAnimationTime);
        }

        currentLives.Clear();
    }

    public void RestoreLife()
    {
        // if all lives are full, do nothing
        if (currentLives.Count == totalLives) { return; }

        AddLife();
    }

    public void RestoreAllLives()
    {
        int livesToAdd = totalLives - currentLives.Count;
        for (int i=0; i < livesToAdd; i++)
        {
            RestoreLife();
        }
    }

    public void AddExtraLife()
    {
        RestoreAllLives();
        AddLife();
        totalLives++;
    }
}
