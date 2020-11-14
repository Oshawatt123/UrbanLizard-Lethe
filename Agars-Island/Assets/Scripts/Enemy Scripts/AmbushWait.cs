using System.Collections;
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
    private float AmbushTime;

    public AmbushWait(Transform _self, float InAmbushTime)
    {
        Self = _self;
        localBB = Self.GetComponent<localTree>();
        agent = Self.GetComponent<NavMeshAgent>();
        TimeToEndAmbush = int.MaxValue;

        AmbushTime = InAmbushTime;
    }

    public override NodeState tick()
    {
        //Check if first frame in ambush
        if(TimeToEndAmbush == int.MaxValue)
        {
            //Start Timer
            TimeToEndAmbush = Time.time + AmbushTime;
        }
        
        //Check if time has passed to leave ambush
        else if (Time.time >= TimeToEndAmbush)
        {
            Self.transform.GetChild(0).gameObject.SetActive(false);
            agent.enabled = true;
            Self.transform.position -= Self.transform.up * 5;
            Debug.Log("Ambush Timer Complete");

            //Reset timer to trigger first pass
            TimeToEndAmbush = int.MaxValue;
            return NodeState.NODE_SUCCESS;
        }

        return NodeState.NODE_RUNNING;
    }
}
