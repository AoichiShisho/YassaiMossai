using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeviceDetector : MonoBehaviour
{
    public List<Dropdown> playerDeviceDropdowns;
    public List<Image> playerDeviceImage;
    private List<InputDevice> devices;
    private Dictionary<int, int> playerDeviceSelections = new Dictionary<int, int>();
    public List<Text> playerDeviceTexts;
    private int initialPlayerCount;

    public Sprite gamepadSprite;
    public Sprite keyboardSprite;

    void Start() {
        DontDestroyOnLoad(gameObject);

        // プレイヤー数における初期値を2に設定
        initialPlayerCount = 2;

        devices = new List<InputDevice>();
        foreach (var device in InputSystem.devices) {
            if (device is Gamepad || device is Keyboard) {
                devices.Add(device);
            }
        }
        UpdateDeviceDropdowns();
        InputSystem.onDeviceChange += OnDeviceChanged;

        for (int i = 0; i < initialPlayerCount; i++) {
            if (devices.Count > i) {
                UpdateDeviceImage(i, devices[i]);
            }
        }
    }


    void Update() 
    {
        UpdateDeviceTexts();
    }

    private void UpdateDeviceTexts()
    {
        for (int i = 0; i < playerDeviceTexts.Count; i++) {
            if (devices.Count > i) {
                playerDeviceTexts[i].text = $"Player {i + 1}: {devices[i].displayName}";
            } else {
                playerDeviceTexts[i].text = $"Player {i + 1}: No Device";
            }
        }
    }

    public Dictionary<int, int> GetPlayerDeviceSelections() {
        return playerDeviceSelections;
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
            InitializeDeviceSelections();
            UpdateDeviceDropdowns();
        }
    }

    // デバイスの選択状態を初期化する
    private void InitializeDeviceSelections() 
    {
        playerDeviceSelections.Clear();
        for (int i = 0; i < Math.Min(devices.Count, initialPlayerCount); i++) {
            playerDeviceSelections.Add(i, devices[i].deviceId);
        }
    }

    private void UpdateDeviceDropdowns()
    {
        for (int i = 0; i < initialPlayerCount; i++) {
            playerDeviceDropdowns[i].onValueChanged.RemoveAllListeners();

            int localIndex = i;
            // どれかのDropdownでデバイスで違う選択肢に変更されたら
            playerDeviceDropdowns[i].onValueChanged.AddListener((int deviceIndex) => {
                OnDeviceSelected(localIndex, deviceIndex);
            });

            UpdateDropdownOptions(playerDeviceDropdowns[i], i);

            // 利用可能なデバイスの中から最初のものをデフォルトとして設定
            if (devices.Count > i) {
                InputDevice defaultDevice = devices[i];
                playerDeviceDropdowns[i].value = i;
                playerDeviceSelections[i] = defaultDevice.deviceId;
            } else {
                playerDeviceDropdowns[i].value = 0;
                playerDeviceSelections[i] = -1; // 未選択の状態を示す
            }

            playerDeviceDropdowns[i].RefreshShownValue();
        }
    }

    // Dropdownでデバイスが選択されたら
    public void OnDeviceSelected(int playerIndex, int deviceIndex)
    {
        InputDevice selectedDevice = devices[deviceIndex];
        int previousDeviceId = playerDeviceSelections.ContainsKey(playerIndex) ? playerDeviceSelections[playerIndex] : -1;

        // 他のプレイヤーが同じデバイスを選んでいないか確認
        bool isAlreadySelected = false;
        foreach (var kvp in playerDeviceSelections)
        {
            if (kvp.Key != playerIndex && kvp.Value == selectedDevice.deviceId) {
                Debug.LogError($"Device {selectedDevice.displayName} is already selected by another player.");
                isAlreadySelected = true;
                break;
            }
        }

        if (isAlreadySelected) {
            // Dropdownの選択を以前のデバイスに戻す
            if (previousDeviceId != -1)
            {
                int previousDeviceIndex = devices.FindIndex(d => d.deviceId == previousDeviceId);
                playerDeviceDropdowns[playerIndex].value = previousDeviceIndex >= 0 ? previousDeviceIndex : 0;
            }
            else {
                playerDeviceDropdowns[playerIndex].value = 0; // デフォルトの選択（例えば「No Device」）に戻す
            }
            playerDeviceDropdowns[playerIndex].RefreshShownValue();
        }
        else
        {
            // デバイスの選択を更新
            playerDeviceSelections[playerIndex] = selectedDevice.deviceId;
            UpdateDeviceImage(playerIndex, selectedDevice);
            PlayerPrefs.SetInt($"PlayerDeviceID_{playerIndex}", selectedDevice.deviceId);
            PlayerPrefs.Save();
            Debug.Log($"Player {playerIndex + 1} selected device: {selectedDevice.displayName}");
        }
    }

    private void UpdateDeviceImage(int playerIndex, InputDevice selectedDevice)
    {
        if (selectedDevice is Gamepad) {
            playerDeviceImage[playerIndex].sprite = gamepadSprite;
        } else if (selectedDevice is Keyboard) {
            playerDeviceImage[playerIndex].sprite = keyboardSprite;
        } else {
            // ログで画像が選択できなかった旨を出力
            Debug.LogError($"Device {selectedDevice.displayName} is not supported.");
        }
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

    // SampleScene2へ遷移する
    public void OnClickToMain()
    {
        // すべてのプレイヤーがデバイスを選択しているか確認する
        bool allPlayersHaveDevices = true;
        for (int i = 0; i < initialPlayerCount; i++) {
            if (!playerDeviceSelections.ContainsKey(i) || playerDeviceSelections[i] == -1) {
                // プレイヤーがデバイスを選択していない場合
                allPlayersHaveDevices = false;
                break;
            }
        }

        if (allPlayersHaveDevices) {
            // すべてのプレイヤーがデバイスを選択している場合、次のシーンに遷移
            SceneManager.LoadScene("Main");
        } else {
            // 一人でもデバイスを選択していないプレイヤーがいる場合、エラーメッセージを出力
            Debug.LogError("Cannot perform transition to Main: One or more players have not selected a device.");
        }
    }

    // // デバイスの選択がユニークかどうかを確認する
    // private bool AreDeviceSelectionsUnique()
    // {
    //     HashSet<int> uniqueSelections = new HashSet<int>();
    //     foreach (var selection in playerDeviceSelections)
    //     {
    //         // 選択が重複しているか、または未選択(-1)かをチェック
    //         if (!uniqueSelections.Add(selection.Value) || selection.Value == -1) {
    //             return false;
    //         }
    //     }
    //     return true;
    // }
}
