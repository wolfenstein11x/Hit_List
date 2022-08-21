using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolGuard : Enemy
{
    Animator animator;
    CapsuleCollider2D bodyCollider;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public override void TakeHit()
    {
        base.TakeHit();
        animator.SetTrigger("takeHit");
        isDead = true;
    }

}
