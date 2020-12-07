using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Camera Camera;
    [HideInInspector] public Rigidbody PlayerBody;

    //Rotation
    public float RotationSpeed;
    public float CamMinY;
    public float CamMaxY;
    public float RotSmoothSpeed;

    //Movement
    public float MoveSpeed;
    //Sprinting
    private bool IsSprinting;
    public int Stamina;
    public int MaxStamina;
    //Stamina Drain
    public int SprintDrainDelay;
    private float NextDrainTime;
    //Stamina Recovery
    public int SprintRecoverDelay;
    private float NextRecTime;
    //Other Movement Related
    float Gravity = -13;
    float VelocityY = 0.0f;

    //Rotations
    private float BodyRoationX;
    private float CamRotationY;

    //private CharacterController Controller;

    [SerializeField] private Slider staminaBar;

    public bool IsHiding;

    // Start is called before the first frame update
    void Start()
    {
        Camera = this.GetComponentInChildren<Camera>();
        PlayerBody = this.GetComponent<Rigidbody>();

        //Lock Cursor to centre
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //Controller = this.GetComponent<CharacterController>();
        Stamina = MaxStamina;
        NextDrainTime = Time.time + SprintDrainDelay;
        IsHiding = false;
    }

    private void Update()
    {
        //Check for sprint toggle
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            SprintToggle();
        }

        //Drain Sprint after delay
        if (IsSprinting && Time.time >= NextDrainTime)
        {
            DrainSprint();
        }

        //As long as not sprinting, recover stamina
        else if (!IsSprinting && Time.time >= NextRecTime)
        {
            RecoverSprint();
        }
        
        staminaBar.value = (Stamina / (float)MaxStamina)*100.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        LookRotation();
        Movement();
    }

    void LookRotation()
    {
        //Add any new mouse movement
        BodyRoationX += Input.GetAxis("Mouse X") * RotationSpeed;
        CamRotationY += Input.GetAxis("Mouse Y") * RotationSpeed;

        //Clamp Camera Roation Values
        CamRotationY = Mathf.Clamp(CamRotationY, CamMinY, CamMaxY);

        //Create Rotation Targets
        Quaternion CamTargetRotation = Quaternion.Euler(-CamRotationY, 0, 0);
        Quaternion BodyRotationTarget = Quaternion.Euler(0, BodyRoationX, 0);

        //Handle Rotations
        transform.rotation = Quaternion.Lerp(transform.rotation, BodyRotationTarget, Time.deltaTime * RotSmoothSpeed);
        Camera.transform.localRotation = Quaternion.Lerp(Camera.transform.localRotation, CamTargetRotation, Time.deltaTime * RotSmoothSpeed);
    }
    void Movement()
    {
        float MoveH = Input.GetAxisRaw("Horizontal");
        float MoveV = Input.GetAxisRaw("Vertical");

        Vector3 MoveDir = new Vector3(MoveH, 0, MoveV) * MoveSpeed;

        Vector3 NewPos = PlayerBody.position + PlayerBody.transform.TransformDirection(MoveDir);

        PlayerBody.MovePosition(NewPos);

        if(MoveDir == Vector3.zero && IsSprinting)
        {
            SprintToggle();
        }
    }
    void SprintToggle()
    {
        if (IsSprinting)
        {
            IsSprinting = false;
            MoveSpeed /= 2;
            NextRecTime = Time.time + SprintRecoverDelay;
        }

        else if (Stamina > 0)
        {
            IsSprinting = true;
            MoveSpeed *= 2;
        }
    }
    void DrainSprint()
    {
        Stamina -= 5;
        NextDrainTime = Time.time + SprintDrainDelay;
        //Check if stamina is depleted
        if (Stamina <= 0)
        {
            IsSprinting = false;
            MoveSpeed /= 2;
            NextRecTime = Time.time + SprintRecoverDelay;
        }
    }
    void RecoverSprint()
    {
        if (Stamina < MaxStamina)
        {
            Stamina += 5;
            NextRecTime = Time.time + SprintRecoverDelay;
        }
    }
}
