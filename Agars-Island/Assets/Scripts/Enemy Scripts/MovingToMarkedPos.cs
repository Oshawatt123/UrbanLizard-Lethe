using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using UnityEngine.AI;

public class MovingToMarkedPos : BT_Behaviour
{
    private Transform Self;
    private localTree localBB;
    private NavMeshAgent agent;

    public MovingToMarkedPos(Transform _self)
    {
        Self = _self;
        localBB = Self.GetComponent<localTree>();
        agent = Self.GetComponent<NavMeshAgent>();

    }

    public override NodeState tick()
    {
        //If agent is moving to a preset location (players last known position from alternate route or sight lost)
        if (localBB.FixedMoveLocation)
        {
            //Return success to continue movement to that point
            return NodeState.NODE_SUCCESS;
        }

        //Else return fail to allow for random movement
        return NodeState.NODE_FAILURE;
    }
}
