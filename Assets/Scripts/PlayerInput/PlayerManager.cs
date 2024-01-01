using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    private const string PLAYER_COUNT_KEY = "PlayerCount";

    [SerializeField]
    private List<Dropdown> playerDeviceDropdowns;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt(PLAYER_COUNT_KEY, 2);
        PlayerPrefs.Save();
    }

    public void SetPlayerCount(int count) {
        PlayerPrefs.SetInt(PLAYER_COUNT_KEY, count);
        PlayerPrefs.Save();
    }

    public void IncreasePlayerCount() {
        int count = PlayerPrefs.GetInt(PLAYER_COUNT_KEY);
        if (count < 4) {
            SetPlayerCount(count + 1);
            Debug.Log("IncreasePlayerCount: " + PlayerPrefs.GetInt(PLAYER_COUNT_KEY));
            UpdateDeviceDropdownsActiveState();
        } else {
            // エラーメッセージを表示
            Debug.Log("Can't increase player count");
        }
    }

    public void DecreasePlayerCount() {
        int count = PlayerPrefs.GetInt(PLAYER_COUNT_KEY);
        if (count > 2) {
            SetPlayerCount(count - 1);
            Debug.Log("DecreasePlayerCount: " + PlayerPrefs.GetInt(PLAYER_COUNT_KEY));
            UpdateDeviceDropdownsActiveState();
        } else {
            // エラーメッセージを表示
            Debug.Log("Can't decrease player count");
        }
    }

    public void UpdateDeviceDropdownsActiveState() {
        int playerCount = PlayerPrefs.GetInt(PLAYER_COUNT_KEY);
        for (int i = 0; i < playerDeviceDropdowns.Count; i++) {
            playerDeviceDropdowns[i].gameObject.SetActive(i < playerCount);
        }
    }
}
