using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviourTree;

public class RandomMovement : BT_Behaviour
{
    private Transform Self;
    private localTree localBB;
    private NavMeshAgent agent;

    public float WanderDistance;
    public float MinDirTime;
    public float MaxDirTime;
    private float NextDirTime;

    public RandomMovement(Transform _self)
    {
        Self = _self;
        localBB = Self.GetComponent<localTree>();
        agent = Self.GetComponent<NavMeshAgent>();

        NextDirTime = Time.time;
    }

    public override NodeState tick()
    {
        if (Time.time >= NextDirTime)
        {
            NextDirTime = Time.time + Random.Range(MinDirTime, MaxDirTime);

            Vector3 RandomDirection = Random.insideUnitSphere * WanderDistance;

            RandomDirection += Self.position;

            NavMeshHit NavHit;
            NavMesh.SamplePosition(RandomDirection, out NavHit, WanderDistance, -1);

            localBB.setMoveToLocation(NavHit.position);

            Debug.Log("Point");
            return NodeState.NODE_SUCCESS;
        }

        return NodeState.NODE_FAILURE;
    }
}
