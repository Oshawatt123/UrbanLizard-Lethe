using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using UnityEngine.AI;

public class DisableObstacleCone : BT_Behaviour
{
    private Transform Self;
    private localTree LocalBB;
    private NavMeshAgent Agent;
    private GameObject Player;

    public DisableObstacleCone(Transform _Self)
    {
        Self = _Self;
        LocalBB = Self.GetComponent<localTree>();

        Agent = Self.GetComponent<NavMeshAgent>();

        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public override NodeState tick()
    {
        Player.transform.GetChild(1).gameObject.SetActive(false);
        return NodeState.NODE_SUCCESS;
    }
}
