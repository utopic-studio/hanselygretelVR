using UnityEngine;

public class MoveNavMeshAgents : MonoBehaviour
{


    public UnityEngine.AI.NavMeshAgent[] agents;
    public Transform[] destinations;
    [SerializeField] UnityEngine.Events.UnityEvent[] eventOnArrive;

    public void MoveAgents()
    {
        if (agents.Length == destinations.Length)
        {
            for (int i=0; i<agents.Length; i++)
            {
                agents[i].SetDestination(destinations[i].position);
                //agents[i].GetComponent<AnimatorBoolController>().SetAnim(AnimatorBoolController.AnimationType.Walk);
            }
        }
    }
    public void CallEventOnArrive(int i)
    {
        eventOnArrive[i].Invoke();
    }
    public void AnimateAgents()
    {
        /*
        for (int i = 0; i < agents.Length; i++)
        {
            agents[i].GetComponent<AnimatorBoolController>().SetAnim(AnimatorBoolController.AnimationType.Walk);
        }
        */
    }

    /*
    private bool HasReachedDestintation(UnityEngine.AI.NavMeshAgent mNavMeshAgent)
    {
        if (!mNavMeshAgent.pathPending)
        {
            if (mNavMeshAgent.remainingDistance <= mNavMeshAgent.stoppingDistance)
            {
                if (!mNavMeshAgent.hasPath || mNavMeshAgent.velocity.sqrMagnitude < 0.6f)
                {
                    return true;
                }
            }
        }
        return false;
    }
    */
}
