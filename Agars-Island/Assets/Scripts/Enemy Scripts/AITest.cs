using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AITest : MonoBehaviour
{
    public GameObject Player;
    private NavMeshAgent Agent;

    public float SightRange;

    // Start is called before the first frame update
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckForSight())
        {
            ChasePlayer();
        }
    }

    public void ChasePlayer()
    {
        Agent.destination = Player.transform.position;
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
                if(HitObject.transform.gameObject.tag == "Player")
                {
                    return true;
                }
            }
        }

        //Else Return False
        return false;
    }
}
