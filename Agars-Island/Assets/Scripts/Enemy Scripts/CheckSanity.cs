using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using UnityEngine.AI;

public class CheckSanity : BT_Behaviour
{
    private Transform Self;
    private localTree LocalBB;
    private NavMeshAgent Agent;

    private GameObject Player;

    public CheckSanity(Transform _self)
    {
        Self = _self;
        LocalBB = Self.GetComponent<localTree>();

        Agent = Self.GetComponent<NavMeshAgent>();

        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public override NodeState tick()
    {
        if(Player.GetComponent<PlayerSanity>().Sanity < 50)
        {
            Player.transform.GetChild(1).gameObject.SetActive(true);
        }

        return NodeState.NODE_FAILURE;
    }
}
