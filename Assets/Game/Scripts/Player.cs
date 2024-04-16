using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;

public enum Direction
{
    None = 0, Left = 1, Right = 2, Forward = 3, Back = 4
}

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private LayerMask brickLayer;
    [SerializeField] private GameObject brickPrefab;
    [SerializeField] private Transform brickHolderTransform;
    private Direction currentDirection;
    private Vector2 startPosition, endPosition;
    private Vector3 lastHitPoint;
    private List<GameObject> brickList = new List<GameObject>();


    private bool isMoving = false;

    void Start()
    {
        lastHitPoint = transform.position;
    }


    void Update()
    {
        DetectSwipe();
        if (Vector3.Distance(transform.position, lastHitPoint) < 0.1f)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
        Moving();
    }

    //Di chuyển Player
    private void Moving()
    {
        transform.position = Vector3.MoveTowards(transform.position, lastHitPoint, speed * Time.deltaTime);
    }

    //Determine direction swipe
    private void DetectSwipe()
    {
        if (!isMoving)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startPosition = Input.mousePosition;
            }
            if (Input.GetMouseButtonUp(0))
            {
                endPosition = Input.mousePosition;

                Vector2 swipe = endPosition - startPosition;
                if (Mathf.Abs(swipe.x) > Mathf.Abs(swipe.y))
                {
                    currentDirection = swipe.x < 0 ? Direction.Left : Direction.Right;
                }
                else
                {
                    currentDirection = swipe.y < 0 ? Direction.Back : Direction.Forward;
                }
                Vector3 direct = GetDirection(currentDirection);
                lastHitPoint = GetLastHitPoint(direct);
            }
        }
        else
        {
            currentDirection = Direction.None;
        }
    }

    //Convert type Direction to Vector3
    private Vector3 GetDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.Left:
                return Vector3.left;
            case Direction.Right:
                return Vector3.right;
            case Direction.Forward:
                return Vector3.forward;
            case Direction.Back:
                return Vector3.back;
            default:
                return Vector3.zero;
        }
    }

    //Get last hit point
    private Vector3 GetLastHitPoint(Vector3 direct)
    {
        for (int i = 0; i < 50; i++)
        {
            RaycastHit hit;
            //Debug.DrawRay(transform.position + direct*i, Vector3.down, Color.red, 5f);
            if (Physics.Raycast(transform.position + direct * i, Vector3.down, out hit, 5f, brickLayer))
            {
                lastHitPoint = hit.point;
            }
            else
            {
                break;
            }
        }
        return new Vector3(lastHitPoint.x, transform.position.y, lastHitPoint.z);

    }

    private void AddBrick()
    {
        Vector3 lastBrickPosition = brickHolderTransform.position;
        if (brickList.Count > 0)
        {
            lastBrickPosition = brickList[brickList.Count - 1].transform.position;
        }
        GameObject brick = Instantiate(brickPrefab, lastBrickPosition + new Vector3(0, 0.3f, 0), Quaternion.Euler(-90, 0, -180));
        //brick.name = "Brick" + brickList.Count;
        brickList.Add(brick);
        brick.transform.SetParent(transform);
    }

    private void RemoveBrick(GameObject brick)
    {
        brickList.Remove(brick);
    }

    private void ClearBrick()
    {
        brickList.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Brick"))
        {
            brickList.Add(other.gameObject);
            AddBrick();
            other.gameObject.SetActive(false);
        }
    }
}
