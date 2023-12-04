using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DeviceDetector : MonoBehaviour
{
    public Dropdown deviceDropdown;
    private List<InputDevice> devices;

    // Start is called before the first frame update
    void Start()
    {
        devices = new List<InputDevice>();
        UpdateDeviceDropdown();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateDeviceDropdown()
    {
        deviceDropdown.ClearOptions();
        List<string> options = new List<string>();
        
        foreach (var device in InputSystem.devices) {
            if (device is Gamepad || device is Keyboard) {
                devices.Add(device);
                options.Add(device.displayName);
            }
        }

        deviceDropdown.AddOptions(options);
    }
}
