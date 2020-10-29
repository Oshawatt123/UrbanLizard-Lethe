using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AITest : MonoBehaviour
{
    private GameObject Player;
    private NavMeshAgent Agent;

    public float SightRange;
    public float WanderDistance;

    public float MinDirTime;
    public float MaxDirTime;
    private float NextDirTime;

    // Start is called before the first frame update
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player");
        NextDirTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        //Check distance to player and line of sight
        if (CheckForSight())
        {
            //Set Destination to be Player's location
            SetDestination(Player.transform.position);
        }

        //If time has passed to change direction
        else if(Time.time >= NextDirTime)
        {
            SetDestination(RandomMovement());
        }
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
}
