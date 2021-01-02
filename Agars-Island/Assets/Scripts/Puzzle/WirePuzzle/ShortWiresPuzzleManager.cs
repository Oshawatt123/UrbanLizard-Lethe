using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShortWiresPuzzleManager : MonoBehaviour
{
    private int numberOfScrews;
    
    private int screwsComplete;

    private int[] wires = new int[4];

    [SerializeField] private Animator coverAnim;

    public UnityEvent finishEvent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (screwsComplete >= numberOfScrews)
        {
            coverAnim.SetTrigger("Fall");
        }
    }

    public void DeclareScrew()
    {
        numberOfScrews++;
    }

    public void ScrewComplete()
    {
        screwsComplete++;
    }

    public void CompleteWire(int wire)
    {
        wires[wire] = 1;

        CheckComplete();
    }

    public void FailWire(int wire)
    {
        wires[wire] = 0;
    }

    private void CheckComplete()
    {
        for (int i = 0; i < 4; i++)
        {
            if (wires[i] != 1)
                break;
        }
        finishEvent.Invoke();
    }
    
}