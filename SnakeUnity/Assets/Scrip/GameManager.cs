using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject SnakeHead;
    private SnakeToroidal Snakescrip;

    private int Score = 0;


    //Khởi tạo Instance
    private void OnEnable()
    {
        Instance = this;
    }

    private void OnDisable()
    {
        Instance = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        SnakeHead.GetComponent<SnakeToroidal>();
        Snakescrip = SnakeHead.GetComponent<SnakeToroidal>();
        //Dừng trò chơi
        Time.timeScale = 0;
    }


    public void OnGameStart()
    {
        if (Snakescrip != null)
        {
            Snakescrip.ResetSnake();
            Snakescrip.enabled = true;
        }
        Time.timeScale = 1;
    }
    public void OnGameExit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;  // Dừng game khi chạy trong Unity Editor
        #else
        Application.Quit();  // Thoát game khi chạy trong bản build
        #endif
    }
    public void OnGameOver()
    {
        UIManager.Instance.OnSetBackground(true);
        UIManager.Instance.OnGameOver();
    }
    
    public void AddScore()
    {
        Score++;

        UIManager.Instance.SetScoreInGame(Score);
    }

}
