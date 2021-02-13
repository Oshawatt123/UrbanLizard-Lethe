using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CullLights : MonoBehaviour
{

    [SerializeField] private GameObject sun;
    [SerializeField] private List<GameObject> lightsToCull;
    
    void OnPreCull () {
        if (sun != null)
            sun.SetActive(false);
    }

    void OnPreRender() {
        if (sun != null)
            sun.SetActive(false);
    }
    void OnPostRender() {
        if (sun != null)
            sun.SetActive(true);
    }
}
