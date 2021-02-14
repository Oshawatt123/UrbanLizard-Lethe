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

    bool DestSetFrame;

    //----------------------------------- Ambush positions ---------------------------
    private GameObject[] AmbushPositions;

    //---------------------------------- Ambush Movement Variables ------------------
    private float NextAmbRouteCheck;
    private float AmbushRouteDelay;

    public MoveToAmbush(Transform _self, float InRouteDelay)
    {
        Self = _self;
        localBB = Self.GetComponent<localTree>();
        agent = Self.GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player");

        NextAmbRouteCheck = Time.time;
        AmbushPositions = GameObject.FindGameObjectsWithTag("AmbushPos");

        AmbushRouteDelay = InRouteDelay;

        DestSetFrame = false;
        Self.transform.GetChild(0).gameObject.SetActive(false);

    }

    public override NodeState tick()
    {
        Player.transform.GetChild(1).gameObject.SetActive(true);
        //If small delay has past, recalculate routes to ensure still moving to closest
        if (Time.time >= NextAmbRouteCheck)
        {
            DestSetFrame = false;
            GameObject ClosestPoint = null;
            float ClosestDistance = float.MaxValue;
            NextAmbRouteCheck = Time.time + AmbushRouteDelay;
            //Enable Obstacle on player to prevent selecting ambush point in sight
            Player.transform.GetChild(1).gameObject.SetActive(true);

            //Scan each ambush point for closest
            foreach (GameObject Point in AmbushPositions)
            {
                float PathDistance = CalculatePathDistance(Point);
                float PlayerY = Mathf.RoundToInt(Player.transform.position.y);
                float Pointy = Mathf.RoundToInt(Point.transform.position.y);
                if (PathDistance < ClosestDistance /*&& Pointy == PlayerY*/)
                {
                    ClosestDistance = PathDistance;
                    ClosestPoint = Point;
                    //Set Destination
                    localBB.setMoveToLocation(ClosestPoint.transform.position);
                    agent.SetDestination(ClosestPoint.transform.position);
                    DestSetFrame = true;
                }
            }

        }

        float dist = agent.remainingDistance;
        Debug.Log(dist);
        //Check if arrived at ambush position
        if (dist <= 0 && !DestSetFrame)
        {            
            
            //Disable Cone
            Player.transform.GetChild(1).gameObject.SetActive(false);

            Self.transform.GetChild(0).gameObject.SetActive(true);
            agent.enabled = false;
            Self.GetComponent<MeshRenderer>().enabled = false;
            Self.GetComponent<CapsuleCollider>().enabled = false;
            localBB.InAmbush = true;
            return NodeState.NODE_SUCCESS;
        }

        if (DestSetFrame)
        {
            DestSetFrame = false;
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
        return float.MaxValue;

    }
}
