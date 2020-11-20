using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using UnityEngine.AI;

public class DirectCharge : BT_Behaviour
{
    private Transform Self;
    private localTree localBB;
    private NavMeshAgent Agent;

    private GameObject Player;

    public DirectCharge(Transform _self)
    {
        Self = _self;
        localBB = Self.GetComponent<localTree>();

        Agent = Self.GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public override NodeState tick()
    {
        Debug.Log("Charging!");
        //Disable Player Obstacle Cone
        Player.transform.GetChild(1).gameObject.SetActive(false);

        //Set move to location
        localBB.setMoveToLocation(localBB.StoredPosition);
        localBB.FixedMoveLocation = true;

        return NodeState.NODE_SUCCESS;
    }
}
