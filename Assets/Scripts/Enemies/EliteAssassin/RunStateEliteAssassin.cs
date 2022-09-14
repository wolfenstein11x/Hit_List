using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunStateEliteAssassin : StateMachineBehaviour
{
    EliteAssassin eliteAssassin;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        eliteAssassin = animator.GetComponent<EliteAssassin>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (eliteAssassin.TargetDead())
        {
            animator.SetBool("targetDead", true);
            return;
        }

        eliteAssassin.Run();
        if (!eliteAssassin.TargetSpotted())
        {
            animator.SetBool("targetSpotted", false);
        }

        else if (eliteAssassin.TargetInSights())
        {
            animator.SetBool("targetAcquired", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("targetSpotted", false);
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
