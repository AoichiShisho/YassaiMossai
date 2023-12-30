using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text timerText;
    private float startTime;
    private float leftTime;
    //後々ポーズ画面を作る時もこのboolean値で管理したい
    private bool isCounting = true;

    public float LeftTime
    {
        get { return leftTime; }
    }

    void Start()
    {
        // 一旦90秒
        startTime = 90;
    }

    void Update()
    {
        if (isCounting)
        {
            leftTime = startTime - Time.timeSinceLevelLoad;

            if (leftTime <= 0)
            {
                isCounting = false;
                leftTime = 0;
                LoadResultScene();
            }

            int t = Mathf.FloorToInt(leftTime);

            string minutes = ((int)t / 60).ToString("00");
            string seconds = (t % 60).ToString("00");

            timerText.text = minutes + ":" + seconds;
        }
    }

    void LoadResultScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Result");
    }
}
