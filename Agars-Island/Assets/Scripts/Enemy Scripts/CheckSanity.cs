﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using UnityEngine.AI;

public class CheckSanity : BT_Behaviour
{
    //Required Variables
    private Transform Self;
    private localTree LocalBB;
    private NavMeshAgent Agent;
    private GameObject Player;
    private PlayerSanity SanityScript;

    public float SanityLimit;

    public CheckSanity(Transform _self, float InLimit)
    {
        Self = _self;
        LocalBB = Self.GetComponent<localTree>();

        Agent = Self.GetComponent<NavMeshAgent>();

        Player = GameObject.FindGameObjectWithTag("Player");
        SanityLimit = InLimit;
        SanityScript = Player.GetComponent<PlayerSanity>();
    }

    public override NodeState tick()
    {
        float DistToPlayer = Vector3.Distance(Player.transform.position, Self.position);
        //Get random number
        float RandomNumb = Random.Range(SanityLimit - 10, SanityLimit + 10);

        //Check if AI is forcing a direct charge or random chance has been activated
        if ((LocalBB.ForceCharge || DistToPlayer < 25 || RandomNumb == SanityLimit) && LocalBB.InAmbush == false )
        {
            //Disable Cone
            Player.transform.GetChild(1).gameObject.SetActive(false);
            return NodeState.NODE_SUCCESS;
        }

        //Otherwise compare sanity to threshold for this behaviour
        if(SanityScript.Sanity > SanityLimit)
        {
            return NodeState.NODE_FAILURE;
        }

        else
        {
            return NodeState.NODE_SUCCESS;
        }
    }
}
