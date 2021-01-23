using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ToggleFloor))]
public class ToggleFloorInspector : Editor
{
    public override void OnInspectorGUI()
    {
        ToggleFloor toggleFloor = (ToggleFloor) target;
        
        if (GUILayout.Button("Toggle floor"))
        {
            
            foreach (GameObject gameObject in toggleFloor.FloorObjects)
            {
                gameObject.SetActive(toggleFloor.showing);
            }

            toggleFloor.showing = !toggleFloor.showing;
        }
        
        base.OnInspectorGUI();
    }
}
