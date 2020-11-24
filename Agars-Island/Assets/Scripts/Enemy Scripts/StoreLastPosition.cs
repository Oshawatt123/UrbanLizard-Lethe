using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviourTree;

public class StoreLastPosition : BT_Behaviour
{
    private Transform Self;
    private localTree localBB;
    private NavMeshAgent agent;
    private GameObject Player;

    public StoreLastPosition(Transform _self)
    {
        Self = _self;
        localBB = Self.GetComponent<localTree>();
        agent = Self.GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public override NodeState tick()
    {
        Player.transform.GetChild(1).gameObject.SetActive(true);
        localBB.StoredPosition = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z);
        Debug.Log("Stored");
        return NodeState.NODE_SUCCESS;
    }

}
