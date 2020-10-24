using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Camera Camera;
    public Rigidbody PlayerBody;

    //Rotation
    public float RotationSpeed;
    public float CamMinY;
    public float CamMaxY;
    public float RotSmoothSpeed;

    //Movement
    public float MoveSpeed;

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
    }

    // Update is called once per frame
    void Update()
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

        Vector3 Velocity = (this.transform.forward * inputDir.y + this.transform.right * inputDir.x) * MoveSpeed;

        Controller.Move(Velocity * Time.deltaTime);
    }
}
