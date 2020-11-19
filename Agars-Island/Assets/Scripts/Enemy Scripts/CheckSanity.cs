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
        if(DistToPlayer < 20 || Player.GetComponent<PlayerSanity>().Sanity > SanityLimit)
        {
            Player.transform.GetChild(1).gameObject.SetActive(false);
            return NodeState.NODE_FAILURE;
        }

        else
        {
            Player.transform.GetChild(1).gameObject.SetActive(true);
            return NodeState.NODE_SUCCESS;
        }
    }
}
