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
    public static ContinueGameDelegate continueGameEvent;

    [SerializeField] private Button retryBtn, continueBtn;

    // Start is called before the first frame update
    void Start()
    {
        retryBtn.onClick.AddListener(OnRetryGame);
        continueBtn.onClick.AddListener(OnContinueGame);
    }

    private void OnRetryGame()
    {
        gameObject.SetActive(false);
        retryGameEvent?.Invoke();
    }

    private void OnContinueGame()
    {
        gameObject.SetActive(false);
        continueGameEvent?.Invoke();
    }
}
