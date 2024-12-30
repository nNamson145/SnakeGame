using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    // Start is called before the first frame update
    /*void Start()
    { 
    }*/


    public void OnClickPlay()
    {
        //print("Ta bam nut roi ne");
        gameObject.SetActive(false);
        UIManager.Instance.OnSetBackground(false);
        GameManager.Instance.OnGameStart();
    }

    public void OnExitGame()
    {
        GameManager.Instance.OnGameExit();
    }




}
