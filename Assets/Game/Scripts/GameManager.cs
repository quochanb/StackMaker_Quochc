using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 0;
    }

    private void OnEnable()
    {
        UIManager.startGameEvent += OnStartGame;
        UIManager.pauseGameEvent += OnPauseGame;
        SettingUI.continueGameEvent += OnContinueGame;
    }

    private void OnDisable()
    {
        UIManager.startGameEvent -= OnStartGame;
        UIManager.pauseGameEvent -= OnPauseGame;
        SettingUI.continueGameEvent -= OnContinueGame;
    }

    private void OnStartGame()
    {
        Time.timeScale = 1;
    }

    private void OnPauseGame()
    {
        Time.timeScale = 0;
    }

    private void OnContinueGame()
    {
        Time.timeScale = 1;
    }
}
