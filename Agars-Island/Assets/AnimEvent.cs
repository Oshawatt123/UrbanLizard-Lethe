using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvent : MonoBehaviour
{
    public Animator anim;

    public String eventName = "";
    // Start is called before the first frame update
    void Start()
    {
        if (!anim)
            Debug.LogWarning(transform.name + "event trigger has no animator");
        if (eventName == "")
            Debug.LogWarning(transform.name + "event trigger has no event name");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetTrigger(eventName);
        }
    }
}
