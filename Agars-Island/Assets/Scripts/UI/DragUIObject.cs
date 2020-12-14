using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class DragUIObject : MonoBehaviour, IDragHandler
{

    public bool lockHorizontal;
    public bool lockVertical;
    
    private RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += new Vector2(eventData.delta.x * (lockHorizontal == true ? 0 : 1), eventData.delta.y * (lockVertical == true ? 0 : 1));
    }
}