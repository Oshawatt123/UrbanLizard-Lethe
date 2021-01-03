using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using UnityEngine.AI;

public class GoToPoint : BT_Behaviour
{
    private Transform Self;
    private localTree LocalBB;
    private NavMeshAgent Agent;
    private GameObject Player;

    public GoToPoint(Transform _self)
    {
        Self = _self;
        LocalBB = Self.GetComponent<localTree>();

        Agent = Self.GetComponent<NavMeshAgent>();

        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public override NodeState tick()
    {
        Agent.destination = LocalBB.getMoveToLocation();

        //Debug.Log("GoToPoint : " + agent.destination);
        var path = Agent.path;

        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.blue);
        }

        /*if (!Agent.pathPending && Agent.remainingDistance < 1.0f)
        {
            nodeState = NodeState.NODE_SUCCESS;
            return NodeState.NODE_SUCCESS;
        }*/

        Player.transform.GetChild(1).gameObject.SetActive(false);

        Debug.Log("Moving To Point");

        nodeState = NodeState.NODE_SUCCESS;
        return NodeState.NODE_SUCCESS;

        //nodeState = NodeState.NODE_RUNNING;
        //return NodeState.NODE_RUNNING;
    }
}
