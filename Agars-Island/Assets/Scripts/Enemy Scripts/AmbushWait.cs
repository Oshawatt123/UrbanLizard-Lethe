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
    private float MinAmbushTime;
    private float MaxAmbushTime;

    public AmbushWait(Transform _self, float InMinAmbushTime, float InMaxAmbushTime)
    {
        Self = _self;
        localBB = Self.GetComponent<localTree>();
        agent = Self.GetComponent<NavMeshAgent>();
        TimeToEndAmbush = int.MaxValue;

        MinAmbushTime = InMinAmbushTime;
        MaxAmbushTime = InMaxAmbushTime;
    }

    public override NodeState tick()
    {
        //Check if first frame in ambush
        if(TimeToEndAmbush == int.MaxValue)
        {
            //Start Timer
            TimeToEndAmbush = Time.time + Random.Range(MinAmbushTime,MaxAmbushTime);
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
