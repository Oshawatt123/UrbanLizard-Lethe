using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using UnityEngine.AI;

public class CheckForSight : BT_Behaviour
{
    private Transform Self;
    private localTree LocalBB;

    private float SightRange;

    public CheckForSight(Transform _self, float InSightRange)
    {
        Self = _self;
        LocalBB = Self.GetComponent<localTree>();

        SightRange = InSightRange;
    }

    public override NodeState tick()
    {
        Vector3 VectToPlayer = GetPlayerPosition() - Self.position;
        //Check if within range
        if (VectToPlayer.magnitude <= SightRange)
        {
            //Check for line of sight
            RaycastHit HitObject;
            if (Physics.Raycast(Self.position, VectToPlayer, out HitObject))
            {
                Debug.DrawRay(Self.position, VectToPlayer, Color.blue);
                //If Hit Player without interuption
                if (HitObject.transform.gameObject.tag == "Player")
                {
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
