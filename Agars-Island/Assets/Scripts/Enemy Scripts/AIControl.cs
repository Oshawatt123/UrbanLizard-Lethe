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

    // Start is called before the first frame update
    void Start()
    {
        BT_Sequencer Seq1 = new BT_Sequencer();
        Seq1.AddNode(new CheckSanity(transform));
        Seq1.AddNode(new MoveToAmbush(transform, AmbushRouteDelay));
        Seq1.AddNode(new AmbushWait(transform, MinAmbushTime, MaxAmbushTime));

        BT_Sequencer Seq2 = new BT_Sequencer();
        Seq2.AddNode(new DirectCharge(transform));
        Seq2.AddNode(new GoToPoint(transform));

        BT_Selector Sel1 = new BT_Selector();
        Sel1.AddNode(Seq1);
        Sel1.AddNode(Seq2);

        BT_Sequencer Seq3 = new BT_Sequencer();
        Seq3.AddNode(new CheckForSight(transform, SightRange));
        Seq3.AddNode(Sel1);

        BT_Sequencer Seq4 = new BT_Sequencer();
        Seq4.AddNode(new RandomMovement(transform, MinDirTime, MaxDirTime, WanderDistance));
        Seq4.AddNode(new GoToPoint(transform));

        BT_Selector RootSel = new BT_Selector();
        RootSel.AddNode(Seq3);
        RootSel.AddNode(Seq4);

        tree.SetRoot(RootSel);
    }

    // Update is called once per frame
    void Update()
    {
        tree.Tick();
    }
}
