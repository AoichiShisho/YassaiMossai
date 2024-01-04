using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementTemp : MonoBehaviour
{
    public int playerIndex;
    private float walkSpeed = 3.0f;
    private float runSpeed = 6.0f;
    private float rotationSpeed = 400.0f;
    private Animator animator;
    private Vector2 movementInput;
    private bool isRunning;

    private InputDevice currentDevice;

    void Start()
    {
        currentDevice = InputSystem.devices[0];
        // SetDeviceFromSavedPreferences();
        // SetInputDevice(InputSystem.devices[0]);
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
        Debug.Log($"[Player {playerIndex}] Input device set: {device.displayName}");
    }

    private void SetDeviceFromSavedPreferences()
    {
        int deviceId = PlayerPrefs.GetInt($"PlayerDeviceID_{playerIndex}", -1);
        if (deviceId != -1) {
            var device = InputSystem.GetDeviceById(deviceId);
            if (device != null) {
                SetInputDevice(device);
                Debug.Log($"[Player {playerIndex}] Device set to: {device.displayName}");
            } else {
                Debug.LogError($"[Player {playerIndex}] Saved device not found. Device ID: {deviceId}");
            }
        } else {
            Debug.LogWarning($"[Player {playerIndex}] No device ID saved in PlayerPrefs.");
        }
    }

    void GetInput()
    {
        if (currentDevice != null) {
            if (currentDevice is Gamepad gamepad) {
                movementInput = gamepad.leftStick.ReadValue();
                if (gamepad.leftStickButton.wasPressedThisFrame) {
                    isRunning = !isRunning;
                }
            }
            else if (currentDevice is Keyboard) {
                movementInput.x = Input.GetAxis("Horizontal");
                movementInput.y = Input.GetAxis("Vertical");
                isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
            }
            else {
                Debug.LogError("PlayerMovement: Unknown device type");
                return;
            }
        }
    }

    void Move()
    {
        float currentSpeed = isRunning ? runSpeed : walkSpeed;
        Vector3 movement = new Vector3(movementInput.x, 0, movementInput.y);
        transform.Translate(movement * currentSpeed * Time.deltaTime, Space.World);

        if (movement != Vector3.zero) {
            if (currentDevice is Gamepad) {
                // Gamepadを使用している場合、左スティックの方向にキャラクターを向ける
                Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            } else if (currentDevice is Keyboard) {
                // キーボードを使用している場合、移動方向にキャラクターを向ける
                RotateTowardsMovementDirection(movement);
            }
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
