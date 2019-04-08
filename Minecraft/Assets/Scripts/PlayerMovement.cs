using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    #region Variables
    [SerializeField] private int mouseSensitivity = 3;
    [SerializeField] private int maxXRotationDeg = 60;
    [SerializeField] private float forwardSpeed = 0.08f;
    [SerializeField] private float leftRightSpeed = 0.06f;
    [SerializeField] private float sprintSpeedCounter = 1.4f;
    [SerializeField] private float jumpForce = 5;

    private Transform mainCamera;
    private float cameraRotationX = 0.0f, playerRotationY = 0.0f;
    private Rigidbody rb;
    private float distToGround;
    #endregion

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = transform.GetChild(0);
        distToGround = 0.75f; //Half of collider y length
        cameraRotationX = 0.0f;
    }

    private void FixedUpdate()
    {
        cameraRotation();
        playerMove();
        jumping();
    }

    private void cameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity; //Mouse X axis - rotate Y player axis
        float mouseY = -Input.GetAxis("Mouse Y") * mouseSensitivity; //Mouse Y axis - rotate X camera axis

        cameraRotationX = mainCamera.transform.rotation.eulerAngles.x + mouseY;
        playerRotationY = transform.rotation.eulerAngles.y + mouseX;

        if(cameraRotationX < 360.0f - maxXRotationDeg && cameraRotationX >= 140) //Clamping of X axis camera rotating
            cameraRotationX = 360.0f - maxXRotationDeg;
        else if(cameraRotationX > maxXRotationDeg && cameraRotationX < 140)
            cameraRotationX = maxXRotationDeg;

        transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, playerRotationY, transform.rotation.eulerAngles.z);
        mainCamera.transform.eulerAngles = new Vector3(cameraRotationX, mainCamera.transform.rotation.eulerAngles.y, mainCamera.transform.rotation.eulerAngles.z);
    }

    private void playerMove()
    {
        float sprintMove = Input.GetAxis("Sprint"); //Sprinting
        if(sprintMove == 0) sprintMove = 1;
        else sprintMove = sprintSpeedCounter;

        float forwardMove = Input.GetAxis("Vertical") * forwardSpeed * sprintMove;
        float leftRightMove = Input.GetAxis("Horizontal") * leftRightSpeed * sprintMove;
        
        transform.Translate(leftRightMove, 0, forwardMove, Space.Self);
    }

    private void jumping()
    {
        float jump = Input.GetAxis("Jump") * jumpForce;
        if(jump != 0)
        {
            if(isGrounded())
                rb.AddForce(Vector3.up * jump, ForceMode.Impulse);
        }
    }

    private bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.01f);
    }


    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Cube")
        {
            rb.velocity = Vector3.zero;
            //rb.angularVelocity = Vector3.zero;
        }
    }
}