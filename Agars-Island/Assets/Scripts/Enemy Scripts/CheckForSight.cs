using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using UnityEngine.AI;

public class CheckForSight : BT_Behaviour
{
    //Required Variables
    private Transform Self;
    private localTree LocalBB;
    private GameObject Player;

    //Sight variables
    private float SightRange;
    private PlayerMovement PlayerMove;


    public CheckForSight(Transform _self, float InSightRange)
    {
        //Set required variables
        Self = _self;
        LocalBB = Self.GetComponent<localTree>();
        Player = GameObject.FindGameObjectWithTag("Player");

        //Set sight range and player movement script
        SightRange = InSightRange;
        PlayerMove = Player.GetComponent<PlayerMovement>();
    }

    public override NodeState tick()
    {
        Vector3 VectToPlayer = GetPlayerPosition() - Self.position;
        //Check if within range and Player not hiding
        if (VectToPlayer.magnitude <= SightRange && !PlayerMove.IsHiding)
        {
            //Check for line of sight
            RaycastHit HitObject;
            if (Physics.Raycast(Self.position, VectToPlayer, out HitObject))
            {
                //Debug.DrawRay(Self.position, VectToPlayer, Color.blue);
                //If Hit Player without interuption
                if (HitObject.transform.gameObject.tag == "Player")
                {
                    Debug.Log("Line of sight made");
                    LocalBB.setMoveToLocation(GetPlayerPosition());
                    //Line of sight established, return True
                    nodeState = NodeState.NODE_SUCCESS;
                    return NodeState.NODE_SUCCESS;
                }
            }
        }

        //Else Return False
        nodeState = NodeState.NODE_FAILURE;
        return NodeState.NODE_FAILURE;
    }


}
