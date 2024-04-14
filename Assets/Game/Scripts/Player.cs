using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public enum Direction
{
    None = 0, Left = 1, Right = 2, Forward = 3, Back = 4
}
public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private LayerMask brickLayer;
    private Direction swipeDirection;
    private Vector2 startPosition, endPosition;
    private Vector3 direct, lastHitPoint;

    void Start()
    {

    }


    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        
    }

    private Direction DetectSwipe()
    {

        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            endPosition = Input.mousePosition;

            float xSwipe = endPosition.x - startPosition.x;
            float ySwipe = endPosition.y - startPosition.y;
            if (Mathf.Abs(xSwipe) > Mathf.Abs(ySwipe))
            {
                swipeDirection = xSwipe < 0 ? Direction.Left : Direction.Right;
            }
            else
            {
                swipeDirection = ySwipe < 0 ? Direction.Back : Direction.Forward;
            }
        }

        return swipeDirection;
    }

    private Vector3 GetDirection()
    {
        Direction direction = DetectSwipe();

        switch (direction)
        {
            case Direction.Left:
                direct = Vector3.left;
                break;
            case Direction.Right:
                direct = Vector3.right;
                break;
            case Direction.Forward:
                direct = Vector3.forward;
                break;
            case Direction.Back:
                direct = Vector3.back;
                break;
            default:
                direct = Vector3.zero;
                break;
        }
        return direct;
    }

    private void GetLastHitPoint()
    {
        
    }

    private void AddBrick()
    {

    }

    private void RemoveBrick()
    {

    }

    private void ClearBrick()
    {

    }
}
