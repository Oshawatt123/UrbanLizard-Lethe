using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AITest : MonoBehaviour
{
    private GameObject Player;
    private NavMeshAgent Agent;

    public float SightRange;

    //---------------------------------- Wander Variables ----------------------------
    public float WanderDistance;
    public float MinDirTime;
    public float MaxDirTime;
    private float NextDirTime;

    //----------------------------------- Ambush variables ---------------------------
    public GameObject[] AmbushPositions;
    private bool InAmbush;
    private bool MovingToAmbush;
    public float AmbushWaitTime;
    private float TimeToEndAmbush;

    // Start is called before the first frame update
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player");
        NextDirTime = Time.time;

        AmbushPositions = GameObject.FindGameObjectsWithTag("AmbushPos");
        InAmbush = false;
        MovingToAmbush = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*//Check distance to player and line of sight
        if (CheckForSight())
        {
            //Set Destination to be Player's location
            SetDestination(Player.transform.position);
        }

        //If time has passed to change direction
        else if(Time.time >= NextDirTime)
        {
            SetDestination(RandomMovement());
        }*/

        AmbushBehaviour();
    }

    public void SetDestination(Vector3 TargetPos)
    {
        Agent.destination = TargetPos;
    }

    public bool CheckForSight()
    {
        Vector3 VectToPlayer = Player.transform.position - this.transform.position;
        //Check if within range
        if(VectToPlayer.magnitude <= SightRange)
        {
            //Check for line of sight
            RaycastHit HitObject;
            if(Physics.Raycast(this.transform.position, VectToPlayer, out HitObject))
            {
                Debug.DrawRay(this.transform.position, VectToPlayer, Color.blue);
                //If Hit Player without interuption
                if(HitObject.transform.gameObject.tag == "Player")
                {
                    //Line of sight established, return True
                    return true;
                }
            }
        }

        //Else Return False
        return false;
    }

    public Vector3 RandomMovement()
    {
        NextDirTime = Time.time + Random.Range(MinDirTime, MaxDirTime);

        Vector3 RandomDirection = Random.insideUnitSphere * WanderDistance;

        RandomDirection += this.transform.position;

        NavMeshHit NavHit;
        NavMesh.SamplePosition(RandomDirection, out NavHit, WanderDistance, -1);

        return NavHit.position;
    }

    public void AmbushBehaviour()
    {
        //If waiting to Ambush
        if(InAmbush == true)
        {
            //Check if time has passed to leave ambush
            if(Time.time >= TimeToEndAmbush)
            {
                InAmbush = false;
            }
        }

        //If already moving to ambush position
        else if(MovingToAmbush == true)
        {
            //Check if arrived at ambush position
            if (this.transform.position.x == Agent.destination.x && this.transform.position.z == Agent.destination.z)
            {
                //Set InAmbush to true and MovingToAmbush to false
                InAmbush = true;
                MovingToAmbush = false;
                TimeToEndAmbush = Time.time + AmbushWaitTime;
            }

        }

        //Else Find Closest Ambush point and begin move to
        else
        {
            GameObject ClosestPoint = null;
            float ClosestDistance = float.MaxValue;

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
            SetDestination(ClosestPoint.transform.position);
            MovingToAmbush = true;
        }
    }

    private float CalculatePathDistance(GameObject Point)
    {
        NavMeshPath Path = new NavMeshPath();
        //Calculate path to point
        if (NavMesh.CalculatePath(this.transform.position, Point.transform.position, NavMesh.AllAreas, Path))
        {
            float PathLength = 0;
            //If Path is valid
            if (Path.status != NavMeshPathStatus.PathInvalid)
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
