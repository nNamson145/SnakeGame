using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnClickReturnMenu()
    {
        //print("Ta bam nut roi ne");
        UIManager.Instance.OnMainmenu();
        gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    /*void Update()
    {
        
    }*/
}
