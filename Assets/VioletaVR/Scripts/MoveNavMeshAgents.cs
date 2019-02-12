using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNavMeshAgents : MonoBehaviour
{


    public UnityEngine.AI.NavMeshAgent[] agents;
    public Transform[] destinations;

    public void MoveAndAnimateAgents()
    {
        MoveAgents();
        AnimateAgents();
    }

    public void MoveAgents()
    {
        if (agents.Length == destinations.Length)
        {
            for (int i=0; i<agents.Length; i++)
            {
                agents[i].SetDestination(destinations[i].position);
                agents[i].GetComponent<AnimatorBoolController>().SetAnim(AnimatorBoolController.AnimationType.Walk);
            }
        }
    }
    public void AnimateAgents()
    {
        for (int i = 0; i < agents.Length; i++)
        {
            agents[i].GetComponent<AnimatorBoolController>().SetAnim(AnimatorBoolController.AnimationType.Walk);
            //agents[i].GetComponent<Animator>().SetBool= AnimatorBoolController.AnimationType.Walk;
        }
    }
    private void Update()
    {
        for (int i = 0; i < agents.Length; i++)
        {
            //if (this.HasReachedDestintation(agents[i]))
                //agents[i].GetComponent<AnimatorBoolController>().anim = AnimatorBoolController.AnimationType.Idle;
        }
    }
    private bool HasReachedDestintation(UnityEngine.AI.NavMeshAgent mNavMeshAgent)
    {
        if (!mNavMeshAgent.pathPending)
        {
            if (mNavMeshAgent.remainingDistance <= mNavMeshAgent.stoppingDistance)
            {
                if (!mNavMeshAgent.hasPath || mNavMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
