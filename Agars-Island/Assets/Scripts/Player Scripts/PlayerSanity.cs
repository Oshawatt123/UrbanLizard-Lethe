using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSanity : MonoBehaviour
{
    public float Sanity;
    public float DrainDistance;
    public float DrainSpeed;

    private float maxSanity = 100f;
    [SerializeField] private Slider sanityBar;

    private GameObject Enemy;

    // Start is called before the first frame update
    void Start()
    {
        Enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        sanityBar.value = (Sanity / maxSanity) * 100f;

        float DistToEnemy = Vector3.Distance(this.transform.position, Enemy.transform.position);
        //If enemy is close enough to drain sanity
        if(DistToEnemy <= DrainDistance)
        {
            Vector3 VectToEnemy = Enemy.transform.position - this.transform.position;
            RaycastHit Hit;

            //Check if in Viewport
            if (Enemy.GetComponent<MeshRenderer>().isVisible)
            {
                if (Physics.Raycast(this.transform.position, VectToEnemy, out Hit))
                {
                    if (Hit.transform.CompareTag("Enemy"))
                    {
                        //Drain Sanity
                        Sanity -= DrainSpeed * Time.deltaTime;
                    }
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Contact");
            Time.timeScale = 0;
        }
    }
}
