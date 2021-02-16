﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviourTree;

public class AmbushWait : BT_Behaviour
{
    private Transform Self;
    private localTree localBB;
    private NavMeshAgent agent;

    private float TimeToEndAmbush;
    private float MinAmbushTime;
    private float MaxAmbushTime;

    private GameObject[] AmbushPositions;
    private GameObject Player;

    public AmbushWait(Transform _self, float InMinAmbushTime, float InMaxAmbushTime)
    {
        Self = _self;
        localBB = Self.GetComponent<localTree>();
        agent = Self.GetComponent<NavMeshAgent>();
        TimeToEndAmbush = int.MaxValue;

        MinAmbushTime = InMinAmbushTime;
        MaxAmbushTime = InMaxAmbushTime;

        AmbushPositions = GameObject.FindGameObjectsWithTag("AmbushPos");
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public override NodeState tick()
    {
        //Check if first frame in ambush
        if(TimeToEndAmbush == int.MaxValue)
        {
            if (localBB.FixedMoveLocation)
            {
                localBB.FixedMoveLocation = false;
            }
            localBB.InAmbush = true;
            //Start Timer
            TimeToEndAmbush = Time.time + Random.Range(MinAmbushTime,MaxAmbushTime);
        }
        
        //Check if time has passed to leave ambush
        else if (Time.time >= TimeToEndAmbush)
        {
            //Set to no longer in ambush
            localBB.InAmbush = false;
            Self.transform.GetChild(0).gameObject.SetActive(false);
            Self.GetComponent<MeshRenderer>().enabled = true;
            Self.GetComponent<CapsuleCollider>().enabled = true;
            agent.enabled = true;
            Debug.Log("Ambush Timer Complete");

            //Do something to prevent AI from looping Ambushing
            localBB.CanAmbush = false;
            //StartCoroutine(AmbushActiveDelay());

            //Reset timer to trigger first pass
            TimeToEndAmbush = int.MaxValue;
            return NodeState.NODE_SUCCESS;
        }

        return NodeState.NODE_RUNNING;
    }

    private IEnumerator AmbushActiveDelay()
    {
        yield return new WaitForSeconds(30f);
        localBB.CanAmbush = true;
    }
}
