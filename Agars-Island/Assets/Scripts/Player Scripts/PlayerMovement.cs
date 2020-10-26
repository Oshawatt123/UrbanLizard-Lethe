using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //Private
    float BodyRoationX;
    float CamRotationY;
    CharacterController Controller;


    // Start is called before the first frame update
    void Start()
    {
        Camera = this.GetComponentInChildren<Camera>();
        PlayerBody = this.GetComponent<Rigidbody>();

        //Lock Cursor to centre
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Controller = this.GetComponent<CharacterController>();
        Stamina = MaxStamina;
        NextDrainTime = Time.time + SprintDrainDelay;
    }

    private void Update()
    {
        //Check for sprint toggle
        SprintToggle();

        //Drain Sprint after delay
        if (IsSprinting && Time.time >= NextDrainTime)
        {
            Stamina -= 5;
            NextDrainTime = Time.time + SprintDrainDelay;
            //Check if stamina is depleted
            if(Stamina <= 0)
            {
                IsSprinting = false;
                MoveSpeed /= 2;
                NextRecTime = Time.time + SprintRecoverDelay;
            }
        }

        //As long as not sprinting, recover stamina
        else if (!IsSprinting && Time.time >= NextRecTime)
        {
            if(Stamina < MaxStamina)
            {
                Stamina += 5;
                NextRecTime = Time.time + SprintRecoverDelay;
            }
        }
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
        Vector2 inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        inputDir.Normalize();
        
        VelocityY += Gravity * Time.deltaTime;

        if (Controller.isGrounded)
        {
            VelocityY = 0;
        }

        

        Vector3 Velocity = (this.transform.forward * inputDir.y + this.transform.right * inputDir.x) * MoveSpeed + Vector3.up * VelocityY;

        Controller.Move(Velocity * Time.deltaTime);
    }

    void SprintToggle()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (IsSprinting)
            {
                IsSprinting = false;
                MoveSpeed /= 2;
                NextRecTime = Time.time + SprintRecoverDelay;
            }

            else if(Stamina > 0)
            {
                IsSprinting = true;
                MoveSpeed *= 2;
            }
        }
    }
}
