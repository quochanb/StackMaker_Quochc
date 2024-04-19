using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathColorChange : MonoBehaviour
{
    private Color pathColor = Color.yellow;
    private Renderer pathRenderer;

    private void Start()
    {
        pathRenderer = GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collision");
            pathRenderer.material.color = pathColor;
        }
    }
}

