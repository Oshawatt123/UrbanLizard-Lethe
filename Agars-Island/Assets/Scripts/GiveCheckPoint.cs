using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GiveCheckPoint : MonoBehaviour
{
    private bool triggered = false;
    [SerializeField] private UnityEvent triggerEvent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void CheckPoint()
    {
        if (triggered) return;
        
        triggered = true;
        triggerEvent.Invoke();
    }
}
