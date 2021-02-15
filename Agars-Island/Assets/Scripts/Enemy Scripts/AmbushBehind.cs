using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviourTree;

public class AmbushBehind : BT_Behaviour
{
    private Transform Self;
    private localTree localBB;
    private NavMeshAgent agent;
    private GameObject Player;

    private GameObject[] AmbushPositions;

    public AmbushBehind(Transform _self)
    {
        Self = _self;
        localBB = Self.GetComponent<localTree>();
        agent = Self.GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player");
        AmbushPositions = GameObject.FindGameObjectsWithTag("AmbushPos");
    }

    public override NodeState tick()
    {
        GameObject ClosestPoint = null;
        float ClosestDistance = int.MaxValue;
        float ThisY = Mathf.RoundToInt(Self.transform.position.y);
        float PointY = 0;
        foreach (GameObject Point in AmbushPositions)
        {
            float AngleToPoint = Vector3.Angle(Player.transform.forward, Point.transform.position);
            PointY = Mathf.RoundToInt(Point.transform.position.y);
            //Check if angle to point is equal to behind player and point is on same level
            if ((AngleToPoint > 90 || AngleToPoint < -90) && ((Self.transform.position.y < 0 && PointY < 0) ||
                (Self.transform.position.y > 0 && PointY > 0)))
            {
                Debug.Log("Point Behind Found");
                //Check if point is closest to player
                if (Vector3.Distance(Player.transform.position, Point.transform.position) < ClosestDistance)
                {
                    ClosestPoint = Point;
                    ClosestDistance = Vector3.Distance(Player.transform.position, Point.transform.position);
                }
            }
        }

        //If Player is somehow facing all ambush points
        if(ClosestDistance == int.MaxValue)
        {
            Debug.Log("No Point Found");
            //Rescan list and just find closest point
            foreach (GameObject Point in AmbushPositions)
            {
                PointY = Mathf.RoundToInt(Point.transform.position.y);
                //Check if point is closest to player
                if (Vector3.Distance(Player.transform.position, Point.transform.position) < ClosestDistance &&
                    PointY == ThisY)
                {
                    ClosestPoint = Point;
                    ClosestDistance = Vector3.Distance(Player.transform.position, Point.transform.position);
                }
            }
        }

        localBB.ForceCharge = true;
        Self.GetComponent<MeshRenderer>().enabled = true;
        Self.GetComponent<CapsuleCollider>().enabled = true;
        Self.transform.position = ClosestPoint.transform.position;
        agent.enabled = true;

        return NodeState.NODE_SUCCESS;
    }
}
