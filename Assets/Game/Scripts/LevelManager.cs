using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject playerPrefab, playerInstance;
    [SerializeField] private GameObject[] levelPrefab;
    [SerializeField] private Transform startPoint;

    private int currentMapIndex = 0;
    private int currentLevel = 1;
    private string levelPrefix = "Level_";

    // Start is called before the first frame update
    void Start()
    {
        GameObject level_1 = Instantiate(levelPrefab[currentMapIndex], Vector3.zero, Quaternion.identity);
        startPoint = level_1.GetComponent<Maps>().playerStartPoint;
        SpawnPlayer();
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

    private void SpawnPlayer()
    {
        playerInstance = Instantiate(playerPrefab, startPoint.transform.position, Quaternion.identity);
        Camera.main.GetComponent<CameraFollow>().target = playerInstance.transform;
    }

    private void OnNextLevel()
    {
        currentMapIndex++;
        currentLevel++;
        if(currentLevel <= levelPrefab.Length)
        {
            //tao level tiep theo bang prefab
            GameObject nextLevel = Instantiate(levelPrefab[currentMapIndex], Vector3.zero, Quaternion.identity);
            //lay diem bat dau level moi
            startPoint = nextLevel.GetComponent<Maps>().playerStartPoint;
            //lay ten level
            string levelName = levelPrefix + currentLevel.ToString();
            //load level theo ten
            SceneManager.LoadScene(levelName);
        }
        
    }

    private void OnRetryGame()
    {
        string currentLevelName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentLevelName);
    }

}
