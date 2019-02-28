using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class MoveNavMeshAgents : MonoBehaviour
{

    [Header("En el arreglo AGENTS, usa sólo los que vas a mover")]
    [SerializeField] float rotationSpeedOnArrive = 10;
    [SerializeField] UnityEngine.AI.NavMeshAgent[] agents;
    [Header("No modificar el arreglo DESTINATIONS")]
    [SerializeField] Transform[] destinations;
    [SerializeField] UnityEngine.Events.UnityEvent OnArriveAny;
    [SerializeField] UnityEngine.Events.UnityEvent OnArriveAll;
    [SerializeField] UnityEngine.Events.UnityEvent[] eventOnArrivePerCharacter;

    private List<bool> boolOnArrive;
    private bool any = false;
    private bool all = false;
    private bool moveAgentsCalled = false;

    private void Start()
    {
        boolOnArrive = Enumerable.Repeat<bool>(false, agents.Length).ToList<bool>();
    }
    public void Update()
    {
        if (moveAgentsCalled)
        {
            for (int i = 0; i < agents.Length; i++)
            {
                if (agents[i] && agents[i].enabled && agents[i].gameObject.activeInHierarchy && !agents[i].pathPending)
                {
                    if (agents[i].remainingDistance <= agents[i].stoppingDistance)
                    {
                        if (!agents[i].hasPath || agents[i].velocity.sqrMagnitude <= 0.2f)
                        {
                            CallEventOnArrive(i);
                        }
                    }
                }
            }
        }
    }
    public void MoveAgents()
    {
        moveAgentsCalled = true;
        if (agents.Length <= destinations.Length)
        {
            for (int i=0; i<agents.Length; i++)
            {
                if (agents[i].gameObject.activeInHierarchy && agents[i].isActiveAndEnabled)
                    agents[i].SetDestination(destinations[i].position);
            }
        }
    }
    public void MoveAgentsInstantly()
    {
        moveAgentsCalled = true;
        if (agents.Length <= destinations.Length)
        {
            for (int i = 0; i < agents.Length; i++)
            {
                if (agents[i].gameObject.activeInHierarchy && agents[i].isActiveAndEnabled)
                {
                    agents[i].ResetPath();
                    agents[i].Warp(destinations[i].position);
                }
            }
        }
    }
    public void CallEventOnArrive(int i)
    {

        RotateAgents(i);
        
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

    private void RotateAgents(int i)
    {
        if (i < destinations.Length)
        {
            Vector3 lookPos = destinations[i].forward;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            J.J.instance.followCurve(x => agents[i].transform.rotation = Quaternion.Slerp(agents[i].transform.rotation, rotation, x), 10 * rotationSpeedOnArrive / agents[i].angularSpeed);
        }
        

    }
}
