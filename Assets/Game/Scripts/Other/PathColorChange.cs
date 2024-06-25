
using UnityEngine;

public class PathColorChange : MonoBehaviour
{
    [SerializeField] private Material pathColor;
    [SerializeField] private MeshRenderer pathRenderer;

    private void Start()
    {
        pathRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pathRenderer.material = pathColor;
        }
    }
}

