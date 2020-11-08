using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using UnityEngine.AI;

public class MoveToAmbush : BT_Behaviour
{
    private Transform Self;
    private localTree localBB;
    private NavMeshAgent agent;

    private GameObject Player;

    //----------------------------------- Ambush variables ---------------------------
    public GameObject[] AmbushPositions;
    private bool InAmbush;
    public float AmbushWaitTime;
    private float TimeToEndAmbush;

    //---------------------------------- Ambush Movement Variables ------------------
    private float NextAmbRouteCheck;
    public float AmbushRouteDelay;

    public MoveToAmbush(Transform _self)
    {
        Self = _self;
        localBB = Self.GetComponent<localTree>();
        agent = Self.GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player");

        NextAmbRouteCheck = Time.time;
        AmbushPositions = GameObject.FindGameObjectsWithTag("AmbushPos");
    }

    public override NodeState tick()
    {
        //If small delay has past, recalculate routes to ensure still moving to closest
        if (Time.time >= NextAmbRouteCheck)
        {
            GameObject ClosestPoint = null;
            float ClosestDistance = float.MaxValue;

            //Enable Obstacle on player to prevent selecting ambush point in sight
            Player.transform.GetChild(1).gameObject.SetActive(true);

            //Scan each ambush point for closest
            foreach (GameObject Point in AmbushPositions)
            {
                float PathDistance = CalculatePathDistance(Point);

                if (PathDistance < ClosestDistance)
                {
                    ClosestDistance = PathDistance;
                    ClosestPoint = Point;
                }
            }

            //Set Destination
            localBB.setMoveToLocation(ClosestPoint.transform.position);
            NextAmbRouteCheck = Time.time + AmbushRouteDelay;
        }

        float dist = agent.remainingDistance;
        //Check if arrived at ambush position
        if (dist != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0)
        {
            //Set InAmbush to true and MovingToAmbush to false
            InAmbush = true;
            TimeToEndAmbush = Time.time + AmbushWaitTime;

            Self.transform.GetChild(0).gameObject.SetActive(true);
            agent.enabled = false;
            Self.transform.position += Self.transform.up * 5;

            //Disable Cone
            Player.transform.GetChild(1).gameObject.SetActive(false);

            return NodeState.NODE_SUCCESS;
        }

        return NodeState.NODE_RUNNING;
    }

    private float CalculatePathDistance(GameObject Point)
    {
        NavMeshPath Path = new NavMeshPath();
        //Calculate path to point
        if (NavMesh.CalculatePath(Self.transform.position, Point.transform.position, NavMesh.AllAreas, Path))
        {
            float PathLength = 0;
            //If Path is valid
            if (Path.status == NavMeshPathStatus.PathComplete)
            {
                for (int i = 1; i < Path.corners.Length; ++i)
                {
                    PathLength += Vector3.Distance(Path.corners[i - 1], Path.corners[i]);

                }
                return PathLength;
            }
        }
        //If Calculation Fails or Path is incomplete
        Debug.Log("Incomplete Path");
        return float.MaxValue;

    }
}
