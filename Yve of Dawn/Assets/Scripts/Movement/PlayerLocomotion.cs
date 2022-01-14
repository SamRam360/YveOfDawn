using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    InputManager inputManager;

    Vector3 moveDirection;
    Transform cameraObject;
    Rigidbody playerRigidbody;

    public bool isSprinting;

    [Header("Movement Speeds")]
    public float walkingSpeed = 1.5f;
    public float runningSpeed = 5;
    public float sprintingSpeed = 8;
    public float roationSpeed = 8;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerRigidbody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
    }

    public void HandleAllMovement()
    {
        HandleMovement();
        HandleRotation();
    }
    private void HandleMovement()
    {
        moveDirection = cameraObject.forward * inputManager.verticalInput; //Movement Input
        moveDirection = moveDirection + cameraObject.right * inputManager.horizonalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;

        //If we are sprinting, select sprinting speed

        if (isSprinting)
        {
            moveDirection = moveDirection * sprintingSpeed;
        }
        else
        {
            //If we are running, select running speed
            if (inputManager.moveAmount >= 0.5f)
            {
                moveDirection = moveDirection * runningSpeed;
            }
            else
            {
                moveDirection = moveDirection * walkingSpeed;
            }
        }

      


        // if we are Walking, select walking speed
        

        Vector3 movementVelocity = moveDirection;
        playerRigidbody.velocity = movementVelocity;

    }   

    private void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObject.forward * inputManager.verticalInput;
        targetDirection = targetDirection + cameraObject.right * inputManager.horizonalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
            targetDirection = transform.forward;


        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRoation = Quaternion.Slerp(transform.rotation, targetRotation,roationSpeed * Time.deltaTime);

        transform.rotation = playerRoation;
    }
}
