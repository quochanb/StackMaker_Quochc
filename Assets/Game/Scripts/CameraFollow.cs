using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 camPos = target.position + offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, camPos, smoothTime);
        transform.position = smoothPos;
        transform.LookAt(target);
    }
}
