using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DeviceDetector : MonoBehaviour
{
    public List<Dropdown> playerDeviceDropdowns;
    private List<InputDevice> devices;
    public List<PlayerMovement> playerMovements;

    // <int, int>はそれぞれplayerIndexと入力デバイスのIDが入る
    private Dictionary<int, int> playerDeviceSelections = new Dictionary<int, int>();

    void Start()
    {
        devices = new List<InputDevice>();
        foreach (var device in InputSystem.devices) {
            if (device is Gamepad || device is Keyboard) {
                devices.Add(device);
            }
        }
        InitializeDeviceSelections();
        UpdateDeviceDropdowns();
        InputSystem.onDeviceChange += OnDeviceChanged;
    }

    private void InitializeDeviceSelections()
    {
        for (int i = 0; i < playerDeviceDropdowns.Count; i++) {
            playerDeviceSelections[i] = -1;
        }
    }

    void OnDestroy()
    {
        InputSystem.onDeviceChange -= OnDeviceChanged;
    }

    // デバイスが追加または削除されるたびに呼ばれる
    private void OnDeviceChanged(InputDevice device, InputDeviceChange change)
    {
        if (change == InputDeviceChange.Added || change == InputDeviceChange.Removed) {
            devices.Clear();
            foreach (var d in InputSystem.devices) {
                if (d is Gamepad || d is Keyboard) {
                    devices.Add(d);
                }
            }
            UpdateDeviceDropdowns();
        }
    }

    private void UpdateDeviceDropdowns()
    {
        for (int i = 0; i < playerDeviceDropdowns.Count; i++) {
            playerDeviceDropdowns[i].onValueChanged.RemoveAllListeners();

            // playerDeviceDropdownが変更されたらOnDeviceSelectedを呼ぶ
            int localIndex = i;
            playerDeviceDropdowns[i].onValueChanged.AddListener((int deviceIndex) => {
                OnDeviceSelected(localIndex, deviceIndex);
            });

            UpdateDropdownOptions(playerDeviceDropdowns[i], i);
        }
    }

    public void OnDeviceSelected(int playerIndex, int deviceIndex)
    {
        InputDevice selectedDevice = devices[deviceIndex];
        playerDeviceSelections[playerIndex] = selectedDevice.deviceId;

        if (playerIndex >= 0 && playerIndex < playerMovements.Count) {
            playerMovements[playerIndex].SetInputDevice(selectedDevice);
        }

        Debug.Log($"Player {playerIndex} selected device: {selectedDevice.displayName}");

        // Dropdownの値と見た目を更新
        playerDeviceDropdowns[playerIndex].value = deviceIndex;
        playerDeviceDropdowns[playerIndex].RefreshShownValue();
    }

    private void UpdateDropdownOptions(Dropdown dropdown, int playerIndex)
    {
        dropdown.ClearOptions();
        List<string> options = new List<string>();
        List<int> optionDeviceIds = new List<int>();

        foreach (var device in devices) {
            if (device is Gamepad || device is Keyboard) {
                options.Add(device.displayName);
                optionDeviceIds.Add(device.deviceId);
            }
        }

        dropdown.AddOptions(options);
    }
}
