using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    // DeviceDetectorへの参照を追加
    // public DeviceDetector deviceDetector;

    // ゲームを開始する
    public void StartGame() {
        // // DeviceDetectorからplayerDeviceSelectionsを取得
        // var playerDeviceSelections = deviceDetector.GetPlayerDeviceSelections();

        // if (IsDeviceDuplicate(playerDeviceSelections)) {
        //     // 重複がある場合、エラーメッセージを表示
        //     Debug.LogError("デバイスの重複があります。各プレイヤーは異なるデバイスを選択してください。");
        //     return;
        // }

        // // 重複がない場合、Mainシーンに遷移
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }

    // // デバイスの重複をチェックする
    // private bool IsDeviceDuplicate(Dictionary<int, int> playerDeviceSelections) {
    //     HashSet<int> selectedDevices = new HashSet<int>();

    //     foreach (var device in playerDeviceSelections.Values) {
    //         if (selectedDevices.Contains(device)) {
    //             // 重複を見つけた場合
    //             return true;
    //         }
    //         selectedDevices.Add(device);
    //     }

    //     // 重複がない場合
    //     return false;
    // }
}
