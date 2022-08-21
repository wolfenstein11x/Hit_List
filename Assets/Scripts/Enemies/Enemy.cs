using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void TakeHit()
    {

    }

    public virtual float GetRange()
    {
        return 0;
    }

    public void ShootRaycasts(float range)
    {
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * range);
        Ray ray = new Ray(transform.position, Vector2.right);
        Debug.DrawRay(ray.origin, ray.direction * range, Color.blue);
    }

    public void RemoveFromPlay()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) { return; }

        if (collision.gameObject.tag == "Bullet")
        {
            TakeHit();
        }
    }


}
