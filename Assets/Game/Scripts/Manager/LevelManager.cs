using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private GameObject playerPrefab, currentMap;
    [SerializeField] private GameObject[] levelPrefab;
    [SerializeField] private Transform startPoint;

    GameObject playerInstance;

    private int currentMapIndex = 0;
    private int currentLevelIndex = 0;

    private void Start()
    {
        //instantiate map
        currentMap = Instantiate(levelPrefab[currentMapIndex], Vector3.zero, Quaternion.identity);
        startPoint = currentMap.GetComponent<Maps>().playerPos;
        //instantiate player
        playerInstance = Instantiate(playerPrefab, startPoint.position, Quaternion.identity);
        Camera.main.GetComponent<CameraFollow>().target = playerInstance.transform;
    }

    private void OnEnable()
    {
        WinUI.retryGameEvent += OnRetryGame;
        WinUI.nextLevelEvent += OnNextLevel;
        SettingUI.mainMenuEvent += OnMainMenu;
    }

    private void OnDisable()
    {
        WinUI.retryGameEvent -= OnRetryGame;
        WinUI.nextLevelEvent -= OnNextLevel;
        SettingUI.mainMenuEvent -= OnMainMenu;
    }

    private void OnNextLevel()
    {
        if (currentMapIndex < levelPrefab.Length - 1)
        {
            currentMapIndex++;
        }
        else
        {
            //Debug.Log("No more level to load");
            currentMapIndex = 0;
        }
        SaveLevel(currentMapIndex);
        SpawnLevel(currentMapIndex);
    }

    private void OnRetryGame()
    {
        SpawnLevel(LoadLevel());
    }

    private void OnMainMenu()
    {
        SpawnLevel(LoadLevel());
        Player.instance.ClearBrick();
    }

    public void SaveLevel(int levelIndex)
    {
        if (levelIndex > 0)
        {
            PlayerPrefs.SetInt("CurrentLevel", levelIndex);
            PlayerPrefs.Save();
        }
    }

    public int LoadLevel()
    {
        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            currentLevelIndex = PlayerPrefs.GetInt("CurrentLevel");
        }
        return currentLevelIndex;
    }

    public void SpawnLevel(int levelIndex)
    {
        //tao level tiep theo bang prefab
        GameObject nextMap = Instantiate(levelPrefab[levelIndex], Vector3.zero, Quaternion.identity);
        //lay diem bat dau level moi
        startPoint = nextMap.GetComponent<Maps>().playerPos;
        //xoa map cu di
        Destroy(currentMap.gameObject);
        //gan map hien tai thanh map moi
        currentMap = nextMap;
        //set lai vi tri player thanh vi tri start
        Player.instance.transform.position = startPoint.position;
        //reset lai last hit point
        playerInstance.GetComponent<Player>().SetLastHitPoint(startPoint.position);
    }
}
