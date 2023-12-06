using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DeviceDetector : MonoBehaviour
{
    public Dropdown deviceDropdown;
    private List<InputDevice> devices;    
    public PlayerMovement playerMovement;

    void Start()
    {
        devices = new List<InputDevice>();
        UpdateDeviceDropdown();

        InputSystem.onDeviceChange += OnDeviceChanged;
    }

    void OnDestroy()
    {
        InputSystem.onDeviceChange -= OnDeviceChanged;
    }

    private void OnDeviceChanged(InputDevice device, InputDeviceChange change)
    {
        if (change == InputDeviceChange.Added || change == InputDeviceChange.Removed)
        {
            UpdateDeviceDropdown();
        }
    }

    private void UpdateDeviceDropdown()
    {
        deviceDropdown.ClearOptions();
        devices.Clear(); 
        List<string> options = new List<string>();

        foreach (var device in InputSystem.devices) {
            if (device is Gamepad || device is Keyboard) {
                devices.Add(device);
                string displayName = $"{device.displayName}";
                options.Add(displayName);
            }
        }

        deviceDropdown.AddOptions(options);
    }

    public void OnDeviceSelected(int index)
    {
        InputDevice selectedDevice = devices[index];
        Debug.Log($"Selected device: {selectedDevice.displayName}");
        playerMovement.SetInputDevice(selectedDevice);
    }
}
