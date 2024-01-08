using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button resumeButtton;

    private void Start()
    {
        pausePanel.SetActive(false);
        resumeButtton.onClick.AddListener(ResumeGame);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && Time.timeScale == 1)
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
}
