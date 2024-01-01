using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public DeviceDetector deviceDetector; 

    public void StartGame() {
        if (!deviceDetector.HasDeviceSelectionDuplicates()) {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        } else {
            Debug.Log("同じデバイスが複数選択されています。違うデバイスを選択してください。");
        }
    }
}