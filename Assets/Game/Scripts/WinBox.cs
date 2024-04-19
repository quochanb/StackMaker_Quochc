using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinBox : MonoBehaviour
{
    [SerializeField] private GameObject winBox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        Player.winGameEvent += OpenBox;
    }

    private void OnDisable()
    {
        Player.winGameEvent -= OpenBox;
    }

    private void OpenBox()
    {
        winBox.SetActive(true);
    }
}
