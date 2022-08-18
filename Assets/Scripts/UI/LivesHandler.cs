using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesHandler : MonoBehaviour
{
    [SerializeField] GameObject lifeIcon;
    [SerializeField] int startingLives;
    [SerializeField] int maxLives;
    [SerializeField] float lostLifeAlpha = 60f;
    [SerializeField] float spacing = 20;
    [SerializeField] float lifeAnimationTime = 1.4f;

    List<GameObject> currentLives = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        InitializeLives();
    }

    private void InitializeLives()
    {
        for (int i=0; i < startingLives; i++)
        {
            Vector3 lifePos = new Vector3(transform.position.x + spacing * i, transform.position.y, transform.position.z);
            AddLife(lifePos);
        }
    }

    private void AddLife(Vector3 pos)
    {
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
}
