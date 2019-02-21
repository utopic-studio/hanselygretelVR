using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class MoveNavMeshAgents : MonoBehaviour
{


    public UnityEngine.AI.NavMeshAgent[] agents;
    public Transform[] destinations;
    [SerializeField] UnityEngine.Events.UnityEvent OnArriveAny;
    [SerializeField] UnityEngine.Events.UnityEvent OnArriveAll;
    [SerializeField] UnityEngine.Events.UnityEvent[] eventOnArrivePerCharacter;

    private List<bool> boolOnArrive;
    private bool any = false;
    private bool all = false;

    private void Start()
    {
        boolOnArrive = Enumerable.Repeat<bool>(false, agents.Length).ToList<bool>();
    }

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
        this.boolOnArrive[i] = true;
        if (boolOnArrive.Any<bool>(b => b == true))
        {
            if (!any)
                OnArriveAny.Invoke();
            any = true;
        }
        if (boolOnArrive.All<bool>(b => b == true))
        {
            if (!all)
                OnArriveAll.Invoke();
            all = true;
        }
        if (i < eventOnArrivePerCharacter.Length)
            eventOnArrivePerCharacter[i].Invoke();
    }
    
}
