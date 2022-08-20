using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] float velocityX = 5f;
    [SerializeField] float velocityY = 5f;

    Rigidbody2D rigidbody;
    float orientation;

    // Start is called before the first frame update
    void Start()
    {
        orientation = GetComponentInParent<OrientationTracker>().GetOrientation();
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = new Vector2(velocityX * orientation, velocityY);

        // de-child grenade from thrower so it does not move with thrower
        transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
