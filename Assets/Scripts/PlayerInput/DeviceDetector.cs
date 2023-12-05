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
    }

    private void UpdateDeviceDropdown()
    {
        deviceDropdown.ClearOptions();
        List<string> options = new List<string>();
        
        foreach (var device in InputSystem.devices) {
            if (device is Gamepad || device is Keyboard) {
                devices.Add(device);
                string displayName = $"{device.displayName} (ID: {device.deviceId})";
                options.Add(displayName);
            }
        }

        deviceDropdown.AddOptions(options);
    }

    public void OnDeviceSelected(int index)
    {
        InputDevice selectedDevice = devices[index];
        Debug.Log($"Selected device: {selectedDevice.displayName} (ID: {selectedDevice.deviceId})");
        playerMovement.SetInputDevice(selectedDevice);
    }
}
