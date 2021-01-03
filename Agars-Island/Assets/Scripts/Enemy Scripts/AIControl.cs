using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class AIControl : localTree
{
    //Move To Ambush Variables
    public float AmbushRouteDelay;

    //Check for sight variables
    public float SightRange;

    //Ambushing variables
    public float MinAmbushTime;
    public float MaxAmbushTime;

    //Random Movement variables
    public float WanderDistance;
    public float MinDirTime;
    public float MaxDirTime;

    public bool MovingToMarkedLocation;
    public Vector3 MarkedLocation;

    public float ChargeSanity;
    public float AltRouteSanity;

    // Start is called before the first frame update
    void Start()
    {
        BT_Sequencer Seq1 = new BT_Sequencer();
        Seq1.AddNode(new CheckSanity(transform, AltRouteSanity));
        Seq1.AddNode(new MoveToAmbush(transform, AmbushRouteDelay));
        Seq1.AddNode(new AmbushWait(transform, MinAmbushTime, MaxAmbushTime));
        Seq1.AddNode(new DisableObstacleCone(transform));

        BT_Sequencer Seq2 = new BT_Sequencer();
        Seq2.AddNode(new CheckSanity(transform, ChargeSanity));
        Seq2.AddNode(new DirectCharge(transform));
        Seq2.AddNode(new GoToPoint(transform));
        Seq2.AddNode(new DisableObstacleCone(transform));

        BT_Sequencer Seq5 = new BT_Sequencer();
        Seq5.AddNode(new StoreLastPosition(transform));
        Seq5.AddNode(new MoveToAmbush(transform, AmbushRouteDelay));
        Seq5.AddNode(new AmbushBehind(transform));
        Seq5.AddNode(new SetToMarkedPos(transform));
        Seq5.AddNode(new GoToPoint(transform));
        Seq5.AddNode(new DisableObstacleCone(transform));

        BT_Selector Sel1 = new BT_Selector();
        Sel1.AddNode(Seq2);
        Sel1.AddNode(Seq1);
        Sel1.AddNode(Seq5);

        BT_Sequencer Seq3 = new BT_Sequencer();
        Seq3.AddNode(new CheckForSight(transform, SightRange));
        Seq3.AddNode(new StoreLastPosition(transform));
        Seq3.AddNode(Sel1);

        BT_Sequencer Seq4 = new BT_Sequencer();
        Seq4.AddNode(new RandomMovement(transform, MinDirTime, MaxDirTime, WanderDistance));
        Seq4.AddNode(new GoToPoint(transform));

        BT_Sequencer Seq6 = new BT_Sequencer();
        Seq6.AddNode(new MovingToMarkedPos(transform));
        Seq6.AddNode(new ArrivedAtMarkedPoint(transform));

        BT_Selector RootSel = new BT_Selector();
        RootSel.AddNode(Seq3);
        RootSel.AddNode(Seq6);
        RootSel.AddNode(Seq4);

        tree.SetRoot(RootSel);
    }

    // Update is called once per frame
    void Update()
    {
        tree.Tick();
    }
}
