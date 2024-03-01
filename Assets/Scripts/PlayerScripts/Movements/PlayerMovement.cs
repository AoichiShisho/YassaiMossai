using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public int playerIndex;
    private float walkSpeed = 3.0f;
    private float runSpeed = 6.0f;
    private float rotationSpeed = 400.0f;
    private Animator animator;
    private Vector2 movementInput;
    private bool isRunning;

    private InputDevice currentDevice;

    public ParticleSystem dustEffect;

    void Start()
    {
        SetDeviceFromSavedPreferences();
        animator = GetComponent<Animator>();
        SetDustEffectPosition();
    }

    void Update()
    {
        SetDeviceFromSavedPreferences();
        GetInput();
        UpdateAnimation();
        UpdateDustEffect();
    }

    private void SetDeviceFromSavedPreferences()
    {
        int deviceInt = PlayerPrefs.GetInt($"PlayerDeviceID_{playerIndex}", -1);
        if (deviceInt == -1) Debug.LogError($"PlayerDeviceID_{playerIndex} is not set.");
        else currentDevice = InputSystem.GetDeviceById(deviceInt);
    }

    void GetInput()
    {
        if (currentDevice != null) {
            if (currentDevice is Gamepad) {
                GamepadMovement();
            }
            else if (currentDevice is Keyboard) {
                KeyboardMovement();
            }
            else {
                Debug.LogError("PlayerMovement: Unknown device type");
            }
        }
    }

    void GamepadMovement()
    {
        Gamepad gamepad = currentDevice as Gamepad;
        movementInput = gamepad.leftStick.ReadValue();
        if (gamepad.leftStickButton.wasPressedThisFrame) {
            isRunning = !isRunning;
        }

        // Move and Rotate
        ApplyMovementAndRotation(movementInput);
    }

    void KeyboardMovement()
    {
        movementInput.x = Input.GetAxis("Horizontal");
        movementInput.y = Input.GetAxis("Vertical");
        isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        // Move and Rotate
        ApplyMovementAndRotation(movementInput);
    }

    void ApplyMovementAndRotation(Vector2 movementInput)
    {
        float currentSpeed = isRunning ? runSpeed : walkSpeed;
        Vector3 movement = new Vector3(movementInput.x, 0, movementInput.y) * currentSpeed * Time.deltaTime;
        transform.Translate(movement, Space.World);

        if (movement != Vector3.zero) {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void UpdateAnimation()
    {
        float speedFactor = isRunning ? 2.0f : 1.0f;
        float speed = movementInput.magnitude * speedFactor;
        animator.SetFloat("Speed", speed);
    }

    void UpdateDustEffect()
    {
        var emission = dustEffect.emission;
        var main = dustEffect.main;
        var shape = dustEffect.shape;

        if (movementInput.magnitude > 0) {
            if (!dustEffect.isPlaying) {
                dustEffect.Play();
            }
            if (isRunning) {
                emission.rateOverTime = 20;
                main.startLifetime = 0.5f;
                shape.radius = 0.3f;
            } else {
                emission.rateOverTime = 10;
                main.startLifetime = 1.0f;
                shape.radius = 0.1f;
            }
        } else {
            if (dustEffect.isPlaying) {
                dustEffect.Stop();
            }
        }
    }

    private void SetDustEffectPosition()
    {
        var main = dustEffect.main;
        main.simulationSpace = ParticleSystemSimulationSpace.World;
    }
}
