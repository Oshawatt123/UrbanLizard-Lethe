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

    public float SanityLimit;

    public CheckSanity(Transform _self, float InLimit)
    {
        Self = _self;
        LocalBB = Self.GetComponent<localTree>();

        Agent = Self.GetComponent<NavMeshAgent>();

        Player = GameObject.FindGameObjectWithTag("Player");
        SanityLimit = InLimit;
    }

    public override NodeState tick()
    {
        float DistToPlayer = Vector3.Distance(Player.transform.position, Self.position);
        //Check if AI is forcing a direct charge
        if (LocalBB.ForceCharge || DistToPlayer < 22)
        {
            return NodeState.NODE_SUCCESS;
        }

        //Otherwise compare sanity to threshold for this behaviour
        if(Player.GetComponent<PlayerSanity>().Sanity > SanityLimit)
        {
            return NodeState.NODE_FAILURE;
        }

        else
        {
            return NodeState.NODE_SUCCESS;
        }
    }
}
