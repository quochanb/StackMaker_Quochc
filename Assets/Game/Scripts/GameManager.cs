using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum GameState
{
    Play, Pause
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameState state;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        ChangeGameState(GameState.Pause);
    }

    private void OnEnable()
    {
        UIManager.startGameEvent += OnPlayGame;
        UIManager.pauseGameEvent += OnPauseGame;
        SettingUI.continueGameEvent += OnPlayGame;
    }

    private void OnDisable()
    {
        UIManager.startGameEvent -= OnPlayGame;
        UIManager.pauseGameEvent -= OnPauseGame;
        SettingUI.continueGameEvent -= OnPlayGame;
    }

    public void ChangeGameState(GameState newState)
    {
        this.state = newState;
        switch (newState)
        {
            case GameState.Play:
                OnPlayGame();
                break;
            case GameState.Pause:
                OnPauseGame();
                break;
            default:
                break;
        }

    }

    private void OnPlayGame()
    {
        Time.timeScale = 1;
    }

    private void OnPauseGame()
    {
        Time.timeScale = 0;
    }
}
