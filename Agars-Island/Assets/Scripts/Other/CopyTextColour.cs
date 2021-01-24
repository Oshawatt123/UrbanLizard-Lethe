using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CopyTextColour : MonoBehaviour
{
    [SerializeField] private Text textToCopy;

    private Light thisLight;
    // Start is called before the first frame update
    void Start()
    {
        thisLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        thisLight.color = textToCopy.color;
    }
}
