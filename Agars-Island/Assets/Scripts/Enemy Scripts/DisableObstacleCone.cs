using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using UnityEngine.AI;

public class DisableObstacleCone : BT_Behaviour
{
    //Required variables
    private Transform Self;
    private GameObject Player;

    public DisableObstacleCone(Transform _Self)
    {
        //Set required variables
        Self = _Self;
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public override NodeState tick()
    {
        //Disable Player cone
        Player.transform.GetChild(1).gameObject.SetActive(false);
        return NodeState.NODE_SUCCESS;
    }
}
