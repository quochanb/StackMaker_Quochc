using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathColorChange : MonoBehaviour
{
    private Color pathColor = new Color(255, 219, 76, 255);
    private Renderer pathRenderer;

    private void Start()
    {
        pathRenderer = GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pathRenderer.material.SetColor("_Color", new Color(255, 219, 76, 255));
        }
    }
}

