﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlashlightAttack : MonoBehaviour
{
    public float AttackRange;
    public float DrainAmount;
    public float ActivationCooldown;
    public bool EnemyInCone;

    private ToggleFlashlight ThisToggle;
    public GameObject Enemy;
    private Flashlight_PRO LightScript;

    private GameObject[] AmbushPositions;
    public LayerMask Mask;

    // Start is called before the first frame update
    void Start()
    {
        ThisToggle = this.GetComponent<ToggleFlashlight>();
        EnemyInCone = false;
        AmbushPositions = GameObject.FindGameObjectsWithTag("AmbushPos");
        LightScript = this.GetComponentInChildren<Flashlight_PRO>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            //Check if enough battery to complete attack and that enemy isn't ambushing
            if(ThisToggle.Battery >= DrainAmount && Enemy.GetComponent<localTree>().InAmbush == false)
            {
                //Increase Brightness for set time
                LightScript.Change_Intensivity(100);
                StartCoroutine(LightResetDelay());

                //Drain set amount of battery
                ThisToggle.Battery -= DrainAmount;

                //Check for Enemy In Cone
                if (EnemyInCone)
                {
                    RaycastHit HitObject;
                    Vector3 VectToEnemy = Enemy.transform.position - this.transform.position;
                    //Check for line of sight
                    Debug.DrawRay(this.transform.position + (this.transform.forward * 2), VectToEnemy, Color.red);
                    if (Physics.Raycast(this.transform.position, VectToEnemy, out HitObject, 100f, ~Mask))
                    {
                        Debug.Log(HitObject.transform.name);
                        if(HitObject.transform.gameObject == Enemy)
                        {
                            //Play Dispersal Animation
                            Debug.Log("Dispersing Enemy");

                            //Disable Enemy Behaviour Tree
                            Enemy.transform.GetComponent<AIControl>().enabled = false;
                            Enemy.transform.GetComponent<NavMeshAgent>().enabled = false;

                            //Move Enemy to random ambush position out of range
                            Enemy.transform.position = FindFurthestPosition();

                            //Wait for Delay
                            StartCoroutine(ActivationDelay());
                        }
                    }
                }
            }

            else
            {
                //Play Fizzle sound

            }
        }
    }

    private Vector3 FindFurthestPosition()
    {
        float FurthestDistance = 0;
        Vector3 FurthestPoint = new Vector3();
        foreach (GameObject Point in AmbushPositions)
        {
            //Check if point is closest to player
            if (Vector3.Distance(this.transform.position, Point.transform.position) > FurthestDistance)
            {
                FurthestPoint = Point.transform.position;
                FurthestDistance = Vector3.Distance(this.transform.position, Point.transform.position);
            }
        }
        return FurthestPoint;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") )
        {
            EnemyInCone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyInCone = false;
        }
    }

    private IEnumerator ActivationDelay()
    {
        yield return new WaitForSeconds(ActivationCooldown);

        //Re-activate enemy behaviour tree
        Enemy.GetComponent<AIControl>().enabled = true;
        Enemy.GetComponent<AIControl>().MovingToMarkedLocation = false;
        Enemy.GetComponent<NavMeshAgent>().enabled = true;
    }

    private IEnumerator LightResetDelay()
    {
        yield return new WaitForSeconds(2f);
        //Reset brightness;
        LightScript.Change_Intensivity(50);
    }
}
