using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathColorChange : MonoBehaviour
{
    [SerializeField] private Color pathColor;
    private MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            meshRenderer.material.color = pathColor;
        }
    }
}

