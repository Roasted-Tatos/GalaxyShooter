using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_PhaseC : StateMachineBehaviour
{
    public float time;
    public float minTime;
    public float maxTime;

    private int random;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        time = Random.Range(minTime, maxTime);
        random = Random.Range(0, 3);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (time <= 0)
        {
            if(random == 0)
            {
                animator.SetTrigger("PhaseD");
            }
            else if (random == 1)
            {
                animator.SetTrigger("Ramming");
            }
            else
            {
                animator.SetTrigger("PhaseE");
            }
            
        }
        else
        {
            time -= Time.deltaTime;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
