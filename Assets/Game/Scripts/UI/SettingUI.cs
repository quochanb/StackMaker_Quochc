using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    public delegate void RetryGameDelegate();
    public static RetryGameDelegate retryGameEvent;
    public delegate void ContinueGameDelegate();
    public static ContinueGameDelegate resumeGameEvent;

    [SerializeField] private Button retryBtn, resumeBtn;

    private void Start()
    {
        retryBtn.onClick.AddListener(OnRetryGame);
        resumeBtn.onClick.AddListener(OnContinueGame);
    }

    private void OnRetryGame()
    {
        gameObject.SetActive(false);
        retryGameEvent?.Invoke();
        GameManager.instance.ChangeGameState(GameState.Play);
    }

    private void OnContinueGame()
    {
        gameObject.SetActive(false);
        resumeGameEvent?.Invoke();
        GameManager.instance.ChangeGameState(GameState.Play);
    }
}
