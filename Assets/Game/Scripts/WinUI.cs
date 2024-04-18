using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinUI : MonoBehaviour
{
    public delegate void NextLevelDelegate();
    public static NextLevelDelegate nextLevelEvent;
    public delegate void RetryDelegate();
    public static NextLevelDelegate retryGameEvent;

    [SerializeField] private Button retryBtn, nextBtn;
    // Start is called before the first frame update
    void Start()
    {
        retryBtn.onClick.AddListener(OnRetryGame);
        nextBtn.onClick.AddListener(OnNextLevel);
    }

    private void OnNextLevel()
    {
        gameObject.SetActive(false);
        nextLevelEvent?.Invoke();
    }

    private void OnRetryGame()
    {
        gameObject.SetActive(false);
        retryGameEvent?.Invoke();
    }
}
