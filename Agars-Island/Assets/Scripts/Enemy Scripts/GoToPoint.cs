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

    public GoToPoint(Transform _self)
    {
        Self = _self;
        LocalBB = Self.GetComponent<localTree>();

        Agent = Self.GetComponent<UnityEngine.AI.NavMeshAgent>();
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

        if (!Agent.pathPending && Agent.remainingDistance < 1.0f)
        {
            nodeState = NodeState.NODE_SUCCESS;
            return NodeState.NODE_SUCCESS;
        }
        nodeState = NodeState.NODE_RUNNING;
        return NodeState.NODE_RUNNING;
    }
}
