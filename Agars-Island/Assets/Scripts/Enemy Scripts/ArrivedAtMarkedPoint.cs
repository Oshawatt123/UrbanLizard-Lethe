using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using UnityEngine.AI;

public class ArrivedAtMarkedPoint : BT_Behaviour
{
    private Transform Self;
    private localTree localBB;
    private NavMeshAgent agent;

    public ArrivedAtMarkedPoint(Transform _self)
    {
        Self = _self;
        localBB = Self.GetComponent<localTree>();
        agent = Self.GetComponent<NavMeshAgent>();
    }

    public override NodeState tick()
    {
        //Check Distance on route to point
        if (agent.remainingDistance == 0)
        {
            //Set moving to preset point to false
            localBB.FixedMoveLocation = false;
        }
        return NodeState.NODE_SUCCESS;
    }
}
