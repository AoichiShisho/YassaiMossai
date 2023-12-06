using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private float walkSpeed = 3.0f;
    private float runSpeed = 6.0f;
    private float rotationSpeed = 400.0f;
    private Animator animator;
    private Vector2 movementInput;
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
        UpdateAnimation();
    }

    public void SetInputDevice(InputDevice device) {
        currentDevice = device;
    }

    void GetInput()
    {
        if (currentDevice is Gamepad gamepad) {
            movementInput = gamepad.leftStick.ReadValue();
            isRunning = gamepad.leftTrigger.isPressed;

            RotateTowardsMovementDirection(movementInput);
        }
        else if (currentDevice is Keyboard) {
            movementInput.x = Input.GetAxis("Horizontal");
            movementInput.y = Input.GetAxis("Vertical");
            isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        }
        else {
            Debug.LogError("PlayerMovement: Unknown device type");
        }
    }

    void Move()
    {
        float currentSpeed = isRunning ? runSpeed : walkSpeed;
        Vector3 movement = new Vector3(movementInput.x, 0, movementInput.y);
        transform.Translate(movement * currentSpeed * Time.deltaTime, Space.World);

        if (!(currentDevice is Gamepad) && movement != Vector3.zero) {
            RotateTowardsMovementDirection(movement);
        }
    }

    void RotateTowardsMovementDirection(Vector3 direction)
    {
        if (direction != Vector3.zero) {
            if (currentDevice is Keyboard) {
                Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
                Quaternion yRotation = Quaternion.Euler(0, toRotation.eulerAngles.y, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, yRotation, rotationSpeed * Time.deltaTime);
            } else if (currentDevice is Gamepad) {
                var rotationDirection = new Vector3(movementInput.x, 0, movementInput.y);
                transform.rotation = Quaternion.LookRotation(rotationDirection);
            }
        }
    }

    void UpdateAnimation()
    {
        float speedFactor = isRunning ? 2.0f : 1.0f;
        float speed = movementInput.magnitude * speedFactor;
        animator.SetFloat("Speed", speed);
    }
}
