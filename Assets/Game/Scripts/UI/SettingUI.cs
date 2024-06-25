using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    public delegate void MainMenuDelegate();
    public static MainMenuDelegate mainMenuEvent;
    public delegate void ContinueGameDelegate();
    public static ContinueGameDelegate resumeGameEvent;

    [SerializeField] private Button mainMenuBtn, resumeBtn;

    private void Start()
    {
        mainMenuBtn.onClick.AddListener(OnMainMenu);
        resumeBtn.onClick.AddListener(OnContinueGame);
    }

    private void OnMainMenu()
    {
        gameObject.SetActive(false);
        mainMenuEvent?.Invoke();
        GameManager.instance.ChangeGameState(GameState.Pause);
    }

    private void OnContinueGame()
    {
        gameObject.SetActive(false);
        resumeGameEvent?.Invoke();
        GameManager.instance.ChangeGameState(GameState.Play);
    }
}
