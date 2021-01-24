using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class TutorialManager : MonoBehaviour
{
    private bool firstLoad = true; // change to flag from checkpoint manager
    
    private bool inventoryUsed = false;
    private bool inventoryClosed = false;

    [SerializeField] private Animator TABAnim;
    [SerializeField] private Animator TABCloseAnim;
    [SerializeField] private Animator EAnim;
    
    // Start is called before the first frame update
    void Start()
    {
        TABCloseAnim.Play("InventHidden");
        
        EAnim.Play("InventHidden");

        if (firstLoad)
        {
            Thread.Sleep(1000);
            TABAnim.SetTrigger("FadeIn");
            firstLoad = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!inventoryUsed)
            {
                TABAnim.SetTrigger("FadeOut");
                
                TABCloseAnim.SetTrigger("FadeIn");
                inventoryUsed = true;
                return;
            }

            if (!inventoryClosed)
            {
                TABCloseAnim.SetTrigger("FadeOut");
                
                EAnim.SetTrigger("FadeIn");
                inventoryClosed = true;
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if(inventoryClosed)
                EAnim.SetTrigger("FadeOut");
        }
    }
}
