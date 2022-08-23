using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkStateEnemy : StateMachineBehaviour
{
    Enemy enemy;
    Rigidbody2D body;
    float range;
    float walkSpeed;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
        body = animator.GetComponent<Rigidbody2D>();
        range = enemy.GetRange();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enemy.TargetDead())
        {
            animator.SetBool("targetDead", true);
            return;
        }

        walkSpeed = enemy.GetWalkSpeed();
        enemy.Walk(body, walkSpeed);
        if (enemy.TargetInSights())
        {
            animator.SetBool("targetAcquired", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
