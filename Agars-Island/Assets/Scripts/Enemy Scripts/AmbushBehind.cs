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

        foreach(GameObject Point in AmbushPositions)
        {
            float AngleToPoint = Vector3.Angle(Player.transform.forward, Point.transform.position);
            Debug.Log(AngleToPoint);
            //Check if angle to point is equal to behind player
            if ((AngleToPoint > 90 || AngleToPoint < -90) && Point.transform.position.y == Player.transform.position.y)
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
                float PlayerY = Mathf.RoundToInt(Player.transform.position.y);
                //Check if point is closest to player
                if (Vector3.Distance(Player.transform.position, Point.transform.position) < ClosestDistance && 
                    Point.transform.position.y == PlayerY)
                {
                    ClosestPoint = Point;
                    ClosestDistance = Vector3.Distance(Player.transform.position, Point.transform.position);
                }
            }
        }

        localBB.ForceCharge = true;
        Self.transform.position = ClosestPoint.transform.position;
        agent.enabled = true;

        return NodeState.NODE_SUCCESS;
    }
}
