using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] float velocityX = 5f;
    [SerializeField] float velocityY = 5f;
    [SerializeField] float explosionDelayMin = 4f;
    [SerializeField] float explosionDelayMax = 5f;
    [SerializeField] float explosionDuration = 0.5f;
    [SerializeField] GameObject explosion;
    [SerializeField] float explosionOffsetX = 0.42f;
    [SerializeField] float explosionOffsetY = 2.04f;

    Rigidbody2D rigidbody;
    float orientation;
    AudioSource explosionSound;

    // Start is called before the first frame update
    void Start()
    {
        orientation = GetComponentInParent<OrientationTracker>().GetOrientation();
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = new Vector2(velocityX * orientation, velocityY);
        explosionSound = GetComponent<AudioSource>();

        // de-child grenade from thrower so it does not move with thrower
        transform.parent = null;

        float explosionDelay = Random.Range(explosionDelayMin, explosionDelayMax);
        StartCoroutine(ExplodeCoroutine(explosionDelay));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ExplodeCoroutine(float explosionDelay)
    {
        yield return new WaitForSeconds(explosionDelay);

        // make grenade disappear
        GetComponent<SpriteRenderer>().enabled = false;

        // show explosion in place of grenade and play sound
        explosionSound.Play();
        Vector3 explosionPoint = new Vector3(transform.position.x + explosionOffsetX, transform.position.y + explosionOffsetY, transform.position.z);
        GameObject grenadeExplosion = Instantiate(explosion, explosionPoint, explosion.transform.rotation);
        
        // remove grenade and explosion
        Destroy(grenadeExplosion, explosionDuration);
        Destroy(gameObject, explosionDuration);

    }

}