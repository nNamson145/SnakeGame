//using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject Background;
    public GameObject menuPanel;
    public GameObject ingamePanel;
    public GameObject deadPanel;


    //Khởi tạo Instance
    private void OnEnable()
    {
        Instance = this;
    }

    private void OnDisable()
    {
        Instance = null;
    }
    public void SetScoreInGame(int Score)
    {
        ingamePanel.GetComponent<InGameUI>().SetTextBox(Score);
    }

    public void OnSetBackground(bool turnOn)
    {
        if (turnOn)
        {
            Background.SetActive(true);
        }
        else
        {
            Background?.SetActive(false);
        }
    }

    public void OnMainmenu()
    {
        menuPanel.SetActive(true);
    }

    public void OnGameOver()
    {
        deadPanel.SetActive(true);

        //Dừng trò chơi
        Time.timeScale = 0;
    }

    // Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
