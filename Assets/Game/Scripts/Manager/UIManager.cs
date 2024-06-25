using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public delegate void StartGameDelegate();
    public static StartGameDelegate startGameEvent;
    public delegate void PauseGameDelegate();
    public static StartGameDelegate pauseGameEvent;
    [SerializeField] private GameObject menuUI, gameUI, settingUI, winUI;
    [SerializeField] private Button startBtn, settingBtn, continueBtn;
    
    private void Start()
    {
        startBtn.onClick.AddListener(OnStartGame);
        settingBtn.onClick.AddListener(OnPauseGame);
        continueBtn.onClick.AddListener(OnContinue);
        GameManager.instance.ChangeGameState(GameState.Pause);
    }

    private void OnEnable()
    {
        Player.winGameEvent += OnWinGame;
        SettingUI.mainMenuEvent += OnMainMenu;
    }

    private void OnDisable()
    {
        Player.winGameEvent -= OnWinGame;
        SettingUI.mainMenuEvent -= OnMainMenu;
    }

    private void OnStartGame()
    {
        menuUI.SetActive(false);
        gameUI.SetActive(true);
        startGameEvent?.Invoke();
        StartCoroutine(DelayCallChangeState());
    }

    private void OnContinue()
    {
        int levelIndex = LevelManager.Instance.LoadLevel();
        LevelManager.Instance.SpawnLevel(levelIndex);
        menuUI.SetActive(false);
        gameUI.SetActive(true);
        //StartCoroutine(DelayCallChangeState());
        GameManager.instance.OnContinue();
    }

    private void OnPauseGame()
    {
        settingUI.SetActive(true);
        pauseGameEvent?.Invoke();
        GameManager.instance.ChangeGameState(GameState.Pause);
    }

    private void OnWinGame()
    {
        StartCoroutine(DelayTime());
    }

    private void OnMainMenu()
    {
        gameUI.SetActive(false);
        menuUI.SetActive(true);
    }

    IEnumerator DelayTime()
    {
        yield return new WaitForSeconds(2f);
        winUI.SetActive(true);
        GameManager.instance.ChangeGameState(GameState.Pause);
    }
    IEnumerator DelayCallChangeState()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.instance.ChangeGameState(GameState.Play);
    }
}
