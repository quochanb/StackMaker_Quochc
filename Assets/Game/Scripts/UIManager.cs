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
    [SerializeField] private Button startBtn, settingBtn;
    // Start is called before the first frame update
    void Start()
    {
        startBtn.onClick.AddListener(OnStartGame);
        settingBtn.onClick.AddListener(OnPauseGame);
    }

    private void OnEnable()
    {
        Player.winGameEvent += OnWinGame;
    }

    private void OnDisable()
    {
        Player.winGameEvent -= OnWinGame;
    }

    private void OnStartGame()
    {
        menuUI.SetActive(false);
        gameUI.SetActive(true);
        startGameEvent?.Invoke();
        //Time.timeScale = 1;
    }

    private void OnPauseGame()
    {
        settingUI.SetActive(true);
        pauseGameEvent?.Invoke();
        //Time.timeScale = 0;
    }

    private void OnWinGame()
    {
        winUI.SetActive(true);
    }
}
