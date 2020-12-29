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

    private float WanderDistance;
    private float MinDirTime;
    private float MaxDirTime;
    private float NextDirTime;

    private GameObject Player;

    public RandomMovement(Transform _self, float InMinDirTime, float InMaxDirTime, float InWanderRange)
    {
        Self = _self;
        localBB = Self.GetComponent<localTree>();
        agent = Self.GetComponent<NavMeshAgent>();

        NextDirTime = Time.time;

        WanderDistance = InWanderRange;
        MinDirTime = InMinDirTime;
        MaxDirTime = InMaxDirTime;

        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public override NodeState tick()
    {
        bool SuitablePoint = false;
        if (localBB.FixedMoveLocation)
        {
            localBB.FixedMoveLocation = false;
        }

        if (Time.time >= NextDirTime || agent.remainingDistance == 0)
        {
            NavMeshHit NavHit;
            NextDirTime = Time.time + Random.Range(MinDirTime, MaxDirTime);

            while(SuitablePoint == false)
            {
                Vector3 RandomDirection = Random.insideUnitSphere * Random.Range(10, WanderDistance);

                RandomDirection += Player.transform.position;

                NavMesh.SamplePosition(RandomDirection, out NavHit, WanderDistance, -1);

                NavMeshPath Path = new NavMeshPath();
                agent.CalculatePath(NavHit.position, Path);
                if (Path.status == NavMeshPathStatus.PathComplete)
                {
                    localBB.setMoveToLocation(NavHit.position);
                    SuitablePoint = true;
                    Debug.Log("Suitable Path");
                }
            }

            return NodeState.NODE_SUCCESS;
        }

        return NodeState.NODE_FAILURE;
    }
}
