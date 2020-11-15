using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSanity : MonoBehaviour
{
    public float Sanity;
    private float maxSanity = 100f;
    [SerializeField] private Slider sanityBar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sanityBar.value = (Sanity / maxSanity) * 100f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Contact");
        }
    }
}
