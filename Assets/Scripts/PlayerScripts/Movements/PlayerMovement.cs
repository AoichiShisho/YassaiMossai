using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private float walkSpeed = 3.0f;
    private float runSpeed = 6.0f;
    private float rotationSpeed = 200.0f;
    private Animator animator;
    private float movementInput;
    private float rotationInput;
    private bool isRunning;

    private InputDevice currentDevice;

    void Start()
    {
        SetInputDevice(InputSystem.devices[0]);
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        GetInput();

        Move();
        Turn();

        UpdateAnimation();
    }

    public void SetInputDevice(InputDevice device) {
        currentDevice = device;
    }

    void GetInput()
    {
        if (currentDevice is Gamepad gamepad) {
            Vector2 stickInput = gamepad.leftStick.ReadValue();
            movementInput = stickInput.y;
            rotationInput = stickInput.x;
            isRunning = gamepad.leftTrigger.isPressed;
        }
        else if (currentDevice is Keyboard) {
            movementInput = Input.GetAxis("Vertical");
            rotationInput = Input.GetAxis("Horizontal");
            isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        }
        else {
            Debug.LogError("PlayerMovement: Unknown device type");
        }
    }

    void Move()
    {
        float currentSpeed = isRunning ? runSpeed : walkSpeed;
        transform.Translate(Vector3.forward * movementInput * currentSpeed * Time.deltaTime);
    }

    void Turn()
    {
        transform.Rotate(Vector3.up, rotationInput * rotationSpeed * Time.deltaTime);
    }

    void UpdateAnimation()
    {
        float speedFactor = isRunning ? 2.0f : 1.0f;
        animator.SetFloat("Speed", Mathf.Abs(movementInput) * speedFactor);
    }
}
