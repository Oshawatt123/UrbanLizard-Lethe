using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using UnityEngine.AI;

public class DirectCharge : BT_Behaviour
{
    private Transform Self;
    private localTree LocalBB;
    private NavMeshAgent Agent;

    private GameObject Player;

    public DirectCharge(Transform _self)
    {
        Self = _self;
        LocalBB = Self.GetComponent<localTree>();

        Agent = Self.GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public override NodeState tick()
    {
        //Disable Player Obstacle Cone
        Player.transform.GetChild(1).gameObject.SetActive(false);

        //Set move to location
        LocalBB.setMoveToLocation(Player.transform.position);

        return NodeState.NODE_SUCCESS;
    }
}
