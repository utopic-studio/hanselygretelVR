using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNavMeshAgentHere : MonoBehaviour {

	public UnityEngine.AI.NavMeshAgent agent;
	[Tooltip("If set to None, nav mesh agents walks to this object")]
	public Transform destination;


	public void MoveNavMeshAgent() {

		if (destination)
			agent.destination = destination.position;
		else
			agent.destination = transform.position;
	}

}
