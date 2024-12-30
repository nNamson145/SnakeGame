using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
using UnityEngine.UIElements;

public class SnakeToroidal : MonoBehaviour
{

    [SerializeField]
    public GameObject SnakePrefab;
    public int beginSnakeSize = 3;
    private float moveStep = 1f;
    
    public float stepTime = 0.1f;
    private float timer;
    private List<Transform> snakeBody = new List<Transform>();
    private Vector2 direction = Vector2.up;

    int mapWidth = 30;
    int mapHeight = 16;


    private Animator animator;
    public Sprite horizontalSprite;
    public Sprite verticalSprite;
    public Sprite DownRightSprite;
    public Sprite UpRightSprite;
    public Sprite DownLeftSprite;
    public Sprite UpLeftSprite;

    void Start()
    {
        animator = GetComponent<Animator>();
        //ResetSnake();
        timer = stepTime;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && direction != Vector2.down)
        {
            direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S) && direction != Vector2.up)
        { 
            direction = Vector2.down;
        }
        else if(Input.GetKeyDown(KeyCode.D) && direction != Vector2.left)
        {
            direction = Vector2.right;
        }
        else if(Input.GetKeyDown(KeyCode.A) && direction != Vector2.right)
        {
            direction = Vector2.left;
        }


        // Kiểm tra thời gian để di chuyển rắn
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            MoveSnake();
            timer = stepTime; // Đặt lại bộ đếm
            UpdateBodyDirection();
        }

        if (this && direction != Vector2.zero)
        {
            animator.SetFloat("DirectionX", direction.x);
            animator.SetFloat("DirectionY", direction.y);
        }

    }



    void UpdateBodyDirection()
    {
        for (int i = 1; i < snakeBody.Count; i++)
        {
            Vector3 current = snakeBody[i].position;          // Vị trí hiện tại
            Vector3 previous = snakeBody[i - 1].position;     // Vị trí của đoạn phía trước

            // Tính hướng giữa phần hiện tại và phần trước đó
            Vector2 direction = new Vector2(
                Mathf.RoundToInt(previous.x - current.x),
                Mathf.RoundToInt(previous.y - current.y)
            );

            SpriteRenderer sr = snakeBody[i].GetComponent<SpriteRenderer>();

            // Xử lý sprite dựa trên hướng ngang/dọc
            if (direction.x != 0) // Đi ngang
            {
                sr.sprite = horizontalSprite;
            }
            else if (direction.y != 0) // Đi dọc
            {
                sr.sprite = verticalSprite;
            }

            SpriteRenderer srn = snakeBody[i - 1].GetComponent<SpriteRenderer>();

            // Xử lý góc rẽ (cần đủ dữ liệu từ cả hướng trước và sau)
            if (i > 1) // Chỉ xử lý từ đoạn thứ 2 trở đi
            {
                Vector3 next = snakeBody[i - 2].position; // Vị trí của đoạn phía trước hơn

                // Tính hướng trước đó
                Vector2 prevDirection = new Vector2(
                    Mathf.RoundToInt(previous.x - next.x),
                    Mathf.RoundToInt(previous.y - next.y)
                );

                // So sánh hướng trước và sau để xác định góc rẽ
                if (prevDirection.x == 1 && direction.y == -1) // Rẽ phải dưới
                {
                    srn.sprite = DownRightSprite;
                }
                else if (prevDirection.x == 1 && direction.y == 1) // Rẽ phải trên
                {
                    srn.sprite = UpRightSprite;
                }
                else if (prevDirection.x == -1 && direction.y == -1) // Rẽ trái dưới
                {
                    srn.sprite = DownLeftSprite;
                }
                else if (prevDirection.x == -1 && direction.y == 1) // Rẽ trái trên
                {
                    srn.sprite = UpLeftSprite;
                }
                else if (prevDirection.y == 1 && direction.x == -1) // Rẽ trái trên (từ dọc xuống ngang)
                {
                    srn.sprite = UpLeftSprite;
                }
                else if (prevDirection.y == 1 && direction.x == 1) // Rẽ phải trên (từ dọc xuống ngang)
                {
                    srn.sprite = UpRightSprite;
                }
                else if (prevDirection.y == -1 && direction.x == -1) // Rẽ trái dưới (từ ngang lên dọc)
                {
                    srn.sprite = DownLeftSprite;
                }
                else if (prevDirection.y == -1 && direction.x == 1) // Rẽ phải dưới (từ ngang lên dọc)
                {
                    srn.sprite = DownRightSprite;
                }
            }
        }
    }

    private void MoveSnake()
    {
        // Di chuyển các đoạn thân theo đầu rắn
        for (int i = snakeBody.Count - 1; i > 0; i--)
        {
            snakeBody[i].position = snakeBody[i - 1].position;
        }

        // Di chuyển phần đầu
        Vector3 newPosition = transform.position + new Vector3(direction.x, direction.y, 0) * moveStep;



        // Xử lý bản đồ lặp (teleport khi vượt khỏi biên bản đồ)
        if (newPosition.x > mapWidth / 2 - 1)
        {
            //newPosition.x = -mapWidth / 2;
            GameManager.Instance.OnGameOver();
        }
        else if (newPosition.x < -mapWidth / 2)
        {
            //newPosition.x = mapWidth / 2 - 1;
            GameManager.Instance.OnGameOver();
        }



        if (newPosition.y > mapHeight / 2 - 1)
        {
            //newPosition.y = -mapHeight / 2;
            GameManager.Instance.OnGameOver();
        }
        else if (newPosition.y < -mapHeight / 2)
        {
            //newPosition.y = mapHeight / 2 - 1;
            GameManager.Instance.OnGameOver();
        }

        // Set vị trí mới cho đầu rắn
        transform.position = newPosition;
    }

    public void ResetSnake()
    {
        
        for (int i = 1; i < snakeBody.Count; i++)
        {
            Destroy(snakeBody[i].gameObject);
        }
        snakeBody.Clear();
        snakeBody.Add(transform);
        transform.position = Vector3.zero;

        //Tạo body răn ban đầu
        for (int i = 1; i < beginSnakeSize; i++)
        {
            Grow();
        }
    }

    public void Grow()
    {
        //spawn phần thân rắn
        GameObject newBody = Instantiate(SnakePrefab);

        //lưu vị trí vào list
        newBody.transform.position = snakeBody[snakeBody.Count - 1].position - new Vector3(direction.x ,direction.y, 0 );
        
        snakeBody.Add(newBody.transform);
         
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        int layer = other.gameObject.layer;

        if (layer == LayerMask.NameToLayer("SnakeBody"))
        {
            Debug.Log("game over!");
            GameManager.Instance.OnGameOver();
        }
    }


}
