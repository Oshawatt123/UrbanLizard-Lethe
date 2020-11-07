using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

/* This class is used as a base class to all BT_Tree's created.
 * It provides a space for a local blackboard for storing and
 * accessing variables needed throughout the tree
 * 
 * Already included is some functions for getting and setting a
 * 'global' tree variable "moveToLocation"
*/
public class localTree : MonoBehaviour
{

    public BT_Tree tree = new BT_Tree();

    public Vector3 moveToLocation;

    public void setMoveToLocation(Vector3 location)
    {
        moveToLocation = location;
    }

    public Vector3 getMoveToLocation()
    {
        return moveToLocation;
    }

}