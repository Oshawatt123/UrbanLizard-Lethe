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
    private InventoryTracker Inventory;
    public LayerMask Mask;

    // Start is called before the first frame update
    void Start()
    {
        Enemy = GameObject.FindGameObjectWithTag("Enemy");
        Inventory = this.GetComponent<InventoryTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        float DistToEnemy = Vector3.Distance(this.transform.position, Enemy.transform.position);
        //If enemy is close enough to drain sanity
        if(DistToEnemy <= DrainDistance)
        {
            Vector3 VectToEnemy = Enemy.transform.position - this.transform.position;
            RaycastHit Hit;

            Vector3 MyPosition = Camera.main.WorldToViewportPoint(Enemy.transform.position);
            if ((MyPosition.x >= 0.0f && MyPosition.x <= 1.0f) && (MyPosition.y >= 0.0f && MyPosition.y <= 1.0f) && MyPosition.z >= 0)
            {
                Debug.DrawRay(this.transform.position + (this.transform.forward * 2), VectToEnemy, Color.red);
                if (Physics.Raycast(this.transform.position, VectToEnemy, out Hit, 75f, ~Mask))
                {
                    Debug.Log("Here");
                    if (Hit.transform.CompareTag("Enemy"))
                    {
                        //Drain Sanity
                        Sanity -= DrainSpeed * Time.deltaTime;
                    }
                }
            }
        }
        sanityBar.value = (Sanity / maxSanity) * 100f;

        if(Input.GetKeyDown(KeyCode.M) && Inventory.meds > 0)
        {
            Inventory.RemoveMeds(1);
            Sanity += 25f;
            Mathf.Clamp(Sanity, 0, 100);
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
