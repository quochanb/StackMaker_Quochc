using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public enum Direction
{
    None = 0, Left = 1, Right = 2, Forward = 3, Back = 4
}

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private LayerMask brickLayer, unBrickLayer;
    [SerializeField] private GameObject brickPrefab, brickHolder, playerSprite;
    private Direction direction;
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
        GetInput();
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

    //Moving
    private void Moving()
    {
        transform.position = Vector3.MoveTowards(transform.position, lastHitPoint, speed * Time.deltaTime);
    }

    //Xac dinh huong vuot cua nguoi choi
    private void GetInput()
    {
        //Check neu dang di chuyen
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
                    direction = swipe.x < 0 ? Direction.Left : Direction.Right;
                }
                else
                {
                    direction = swipe.y < 0 ? Direction.Back : Direction.Forward;
                }
                Vector3 direct = GetDirection(direction);
                lastHitPoint = GetLastPoint(direct);
            }
        }
    }

    //Chuyen enum Direction sang dang Vector3
    private Vector3 GetDirection(Direction dir)
    {
        switch (dir)
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

    //Lay ra diem va cham cuoi cung
    private Vector3 GetLastPoint(Vector3 dir)
    {
        RaycastHit hit;
        for (int i = 1; i < 50; i++)
        {
            Ray ray = new Ray(transform.position + dir * i, Vector3.down);
            Debug.DrawRay(transform.position + dir * i, Vector3.down, Color.red, 20f);
            if (Physics.Raycast(ray, out hit, 5f, brickLayer) || Physics.Raycast(ray, out hit, 5f, unBrickLayer))
            {
                lastHitPoint = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            }
            else
            {
                break;
            }
        }
        return lastHitPoint;
    }

    private void AddBrick()
    {
        //Lay ra vi tri vien gach cuoi cung
        Vector3 lastBrickPosition = brickHolder.transform.position;

        //Tao brick moi tai vi tri cua Brick Holder
        GameObject brick = Instantiate(brickPrefab, lastBrickPosition, Quaternion.Euler(-90, 0, -180));
        //Set brick moi tao la child cua Brick Holder
        brick.transform.SetParent(brickHolder.transform);
        //Set vi tri de cac brick xep chong len nhau
        brick.transform.localPosition = new Vector3(0, brickList.Count * 0.3f, 0);
        //add brick vua tao ra
        brickList.Add(brick);
        //set lai vi tri cua player = vi tri cua brick stack
        playerSprite.transform.localPosition = brick.transform.localPosition;
    }

    private void RemoveBrick()
    {
        //lay vien gach tren cung
        GameObject brick = brickList[brickList.Count - 1];
        //xoa vien gach ra khoi list
        brickList.Remove(brick);
        //destroy vien gach di
        Destroy(brick);
        //set position cua player 
        playerSprite.transform.localPosition = brickList[brickList.Count - 1].transform.localPosition;
    }

    private void ClearBrick()
    {
        //destroy toan bo vien gach trong list
        brickList.Clear();
        
        //set position cua player 

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Brick"))
        {
            AddBrick();
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("UnBrick"))
        {
            Debug.Log("Before: " + other.name + " " + other.transform.position);
            RemoveBrick();
        }

        if (other.gameObject.CompareTag("Finish"))
        {
            Debug.Log("Win game");
        }
    }
}
