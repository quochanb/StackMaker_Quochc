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

    public GameState currentState;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentState = GameState.Pause;
    }

    private void OnEnable()
    {
        UIManager.startGameEvent += OnPlayGame;
        UIManager.pauseGameEvent += OnPauseGame;
        SettingUI.resumeGameEvent += OnPlayGame;
    }

    private void OnDisable()
    {
        UIManager.startGameEvent -= OnPlayGame;
        UIManager.pauseGameEvent -= OnPauseGame;
        SettingUI.resumeGameEvent -= OnPlayGame;
    }

    public void ChangeGameState(GameState newState)
    {
        this.currentState = newState;
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

    public void OnContinue()
    {
        ChangeGameState(GameState.Play);
        StartCoroutine(DelayCallStartGame());
    }   
    
    IEnumerator DelayCallStartGame()
    {
        yield return new WaitForSeconds(1f);
        
    }

}
