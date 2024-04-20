using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab, currentMap;
    [SerializeField] private GameObject[] levelPrefab;
    [SerializeField] private Transform startPoint;

    GameObject playerInstance;

    private int currentMapIndex = 0;

    // Start is called before the first frame update
    void Start()
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
        if (currentMapIndex < levelPrefab.Length)
        {
            currentMapIndex++;
            //tao level tiep theo bang prefab
            GameObject nextMap = Instantiate(levelPrefab[currentMapIndex], Vector3.zero, Quaternion.identity);
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
        else
        {
            Debug.Log("No more level to load");
        }
    }

    private void OnRetryGame()
    {
        string currentLevelName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentLevelName);
    }
}
