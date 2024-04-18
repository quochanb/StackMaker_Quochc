using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab, currentMap;
    [SerializeField] private GameObject[] levelPrefab;
    [SerializeField] private Transform startPoint;

    private int currentMapIndex = 0;
    private int currentLevel = 1;

    // Start is called before the first frame update
    void Start()
    {
        //instantiate map
        currentMap = Instantiate(levelPrefab[currentMapIndex], Vector3.zero, Quaternion.identity);
        startPoint = currentMap.GetComponent<Maps>().playerPos;

        //instantiate player
        GameObject playerInstance = Instantiate(playerPrefab, startPoint.position, Quaternion.identity);
        Camera.main.GetComponent<CameraFollow>().target = playerInstance.transform;
    }

    private void OnEnable()
    {
        WinUI.retryGameEvent += OnRetryGame;
        WinUI.nextLevelEvent += OnNextLevel;
        SettingUI.retryGameEvent += OnRetryGame;
    }

    private void OnDisable()
    {
        WinUI.retryGameEvent -= OnRetryGame;
        WinUI.nextLevelEvent -= OnNextLevel;
        SettingUI.retryGameEvent -= OnRetryGame;
    }

    private void OnNextLevel()
    {
        currentLevel++;
        if (currentMapIndex <= levelPrefab.Length - 1)
        {
            currentMapIndex++;
            //tao level tiep theo bang prefab
            GameObject nextMap = Instantiate(levelPrefab[currentMapIndex], Vector3.zero, Quaternion.identity);
            //lay diem bat dau level moi
            startPoint = nextMap.GetComponent<Maps>().playerPos;

            currentMap.SetActive(false);
            nextMap = currentMap;
            Player.Instance.transform.position = startPoint.position;
        }

    }

    private void OnRetryGame()
    {
        string currentLevelName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentLevelName);
    }

}
