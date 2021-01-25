using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using UnityEngine.AI;

public class CheckSanity : BT_Behaviour
{
    private Transform Self;
    private localTree LocalBB;
    private NavMeshAgent Agent;
    private GameObject Player;
    private PlayerSanity SanityScript;

    public float SanityLimit;

    public CheckSanity(Transform _self, float InLimit)
    {
        Self = _self;
        LocalBB = Self.GetComponent<localTree>();

        Agent = Self.GetComponent<NavMeshAgent>();

        Player = GameObject.FindGameObjectWithTag("Player");
        SanityLimit = InLimit;
        SanityScript = Player.GetComponent<PlayerSanity>();
    }

    public override NodeState tick()
    {
        float DistToPlayer = Vector3.Distance(Player.transform.position, Self.position);
        //Check if AI is forcing a direct charge
        if (LocalBB.ForceCharge || DistToPlayer < 25)
        {
            //Disable Cone
            Player.transform.GetChild(1).gameObject.SetActive(false);
            return NodeState.NODE_SUCCESS;
        }
        //Get random number
        float RandomNumb = Random.Range(SanityLimit - 10, SanityLimit + 10);

        //Otherwise compare sanity to threshold for this behaviour
        if(SanityScript.Sanity > SanityLimit || RandomNumb == SanityLimit)
        {
            return NodeState.NODE_FAILURE;
        }

        else
        {
            return NodeState.NODE_SUCCESS;
        }
    }
}
