using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortWiresPuzzleManager : MonoBehaviour
{
    private int numberOfScrews;
    
    private int screwsComplete;

    [SerializeField] private Animator coverAnim;
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
}
