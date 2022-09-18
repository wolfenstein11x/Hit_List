using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowStateAssassin : StateMachineBehaviour
{
    Assassin assassin;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        assassin = animator.GetComponent<Assassin>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bool targetSpotted = assassin.TargetSpotted();
        bool targetSpottedBehind = assassin.TargetSpotted(-1);

        if (targetSpotted)
        {
            if (assassin.TargetInSights())
            {
                // shoot
                animator.SetBool("targetAcquired", true);
            }

            else
            {
                animator.SetBool("targetSpotted", true);
            }
        }

        else if (targetSpottedBehind)
        {
            assassin.FlipSprite();
            assassin.ReverseDirection();

            if (assassin.TargetInSights())
            {
                // shoot
                animator.SetBool("targetAcquired", true);
            }

            else
            {
                animator.SetBool("targetSpotted", true);
            }
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
