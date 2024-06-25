using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    [SerializeField] private GameObject closeBox, openBox;

    public void HitPlayer()
    {
        closeBox.SetActive(false);
        openBox.SetActive(true);
    }
}
